using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using PLSQLImportFull.Data;

namespace PLSQLImportFull.Business
{
    public class ImportStatistics
    {
        public int TotalScripts { get; set; }
        public int TotalInsertsExpected { get; set; }
        public int TotalInsertsSuccess { get; set; }
        public Dictionary<string, int> ErrorCountByTable { get; set; } = new Dictionary<string, int>();
    }

    public class ImportManager
    {
        private readonly OracleQueryExecutor _queryExecutor;

        public ImportManager(OracleQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
        }

        public ImportStatistics ExecuteFiles(List<string> filePaths, Action<string> logger)
        {
            var stats = new ImportStatistics();
            stats.TotalScripts = filePaths.Count;

            foreach (string file in filePaths)
            {
                string ext = Path.GetExtension(file).ToLower();

                if (ext == ".7z") ProcessZipFile(file, stats, logger);
                else if (ext == ".sql" || ext == ".pdc") ProcessSqlFile(file, stats, logger);
                else logger($"[AVISO] Ignorado: {Path.GetFileName(file)}");
            }
            return stats;
        }

        private void ProcessZipFile(string zipPath, ImportStatistics stats, Action<string> logger)
        {
            // Lógica do 7zip (Mantida igual ao seu original)
            string sevenZipPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "7za.exe");
            if (!File.Exists(sevenZipPath)) sevenZipPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "7za.exe");

            if (!File.Exists(sevenZipPath))
            {
                logger($"[ERRO] 7za.exe não encontrado.");
                return;
            }

            string tempDir = Path.Combine(Path.GetTempPath(), "ImportFull_" + Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);

            try
            {
                logger($"Extraindo: {Path.GetFileName(zipPath)}...");
                var p = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = sevenZipPath,
                    Arguments = $"e \"{zipPath}\" -o\"{tempDir}\" -y",
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    UseShellExecute = false
                };
                using (var process = System.Diagnostics.Process.Start(p)) process.WaitForExit();

                var scripts = Directory.GetFiles(tempDir, "*.*", SearchOption.AllDirectories)
                                       .Where(s => s.EndsWith(".sql", StringComparison.OrdinalIgnoreCase) ||
                                                   s.EndsWith(".pdc", StringComparison.OrdinalIgnoreCase))
                                       .OrderBy(s => s).ToList();

                foreach (var script in scripts) ProcessSqlFile(script, stats, logger);
            }
            catch (Exception ex) { logger($"[ERRO] Zip: {ex.Message}"); }
            finally { try { Directory.Delete(tempDir, true); } catch { } }
        }

        // =================================================================================
        //  NOVO PROCESSADOR ROBUSTO (Híbrido: Linha a Linha + Máquina de Estado)
        // =================================================================================
        private void ProcessSqlFile(string file, ImportStatistics stats, Action<string> logger)
        {
            try
            {
                string fileName = Path.GetFileName(file);
                logger($"--------------------------------------------------");
                logger($"Arquivo: {fileName}");

                var tableStats = new Dictionary<string, int[]>();
                StringBuilder buffer = new StringBuilder();

                // Estados da leitura
                bool inPlSqlBlock = false;    // Estamos dentro de BEGIN...END?
                bool inBlockComment = false;  // Estamos dentro de /* ... */?

                // Usa Encoding.Default (ANSI) para corrigir acentos e problemas de aspas quebradas
                using (StreamReader sr = new StreamReader(file, Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string trimmed = line.Trim();

                        // 1. TRATAMENTO DE COMENTÁRIOS DE BLOCO (/* ... */)
                        // Resolve o erro PLS-00103 quando o Oracle tenta ler o cabeçalho decorativo
                        if (inBlockComment)
                        {
                            if (line.Contains("*/")) inBlockComment = false;
                            continue; // Pula linha
                        }
                        if (trimmed.StartsWith("/*"))
                        {
                            if (!trimmed.EndsWith("*/")) inBlockComment = true;
                            continue;
                        }

                        // 2. FILTRO DE SUJEIRA DO SQL*PLUS (Só se o buffer estiver vazio)
                        // Resolve o ORA-00900 (Comando inválido)
                        if (buffer.Length == 0)
                        {
                            if (string.IsNullOrWhiteSpace(trimmed)) continue;
                            if (trimmed.StartsWith("--")) continue; // Comentário simples de linha

                            string upper = trimmed.ToUpper();
                            if (upper.StartsWith("SET ") || upper.StartsWith("PROMPT") ||
                                upper.StartsWith("SPOOL") || upper.StartsWith("EXIT") ||
                                upper.StartsWith("QUIT") || upper.StartsWith("ACCEPT") ||
                                (upper.StartsWith("SELECT '") && upper.Contains("DUAL"))) // Logs de data
                            {
                                continue; // Ignora essa linha e vai para a próxima
                            }

                            // Detecta início de PL/SQL (Constraints, Triggers, Sequences reset)
                            if (upper.StartsWith("DECLARE") || upper.StartsWith("BEGIN"))
                            {
                                inPlSqlBlock = true;
                            }
                        }

                        // 3. ACUMULA A LINHA
                        if (buffer.Length > 0) buffer.AppendLine();
                        buffer.Append(line);

                        // 4. VERIFICA SE O COMANDO TERMINOU
                        if (IsCommandComplete(buffer.ToString(), inPlSqlBlock))
                        {
                            string sqlFinal = buffer.ToString().Trim();

                            // Remove terminadores que o C# não gosta
                            if (inPlSqlBlock)
                            {
                                // Remove a barra "/" e espaços finais
                                sqlFinal = sqlFinal.TrimEnd().TrimEnd('/').TrimEnd();
                            }
                            else if (sqlFinal.EndsWith(";"))
                            {
                                // Remove o ";" final
                                sqlFinal = sqlFinal.Substring(0, sqlFinal.Length - 1);
                            }

                            // Executa
                            string tableName = ExtractTableName(sqlFinal);
                            ExecutarSQL(sqlFinal, tableName, stats, tableStats, logger);

                            // Limpa para o próximo comando
                            buffer.Clear();
                            inPlSqlBlock = false;
                        }
                    }
                }

                // Log final do arquivo
                if (tableStats.Count > 0)
                {
                    foreach (var kvp in tableStats)
                    {
                        if (kvp.Key == "OUTROS" || kvp.Key == "PL/SQL") continue;
                        logger($"OK: {kvp.Key} (Inseridos: {kvp.Value[1]}/{kvp.Value[0]})");
                    }
                }
            }
            catch (Exception ex)
            {
                logger($"[FATAL] Erro ao ler arquivo: {ex.Message}");
            }
        }

        // Valida se o comando realmente acabou (conta aspas e verifica terminadores)
        private bool IsCommandComplete(string sql, bool isPlSql)
        {
            if (string.IsNullOrWhiteSpace(sql)) return false;
            string trimmed = sql.TrimEnd();

            // Lógica para PL/SQL (Constraints/Triggers)
            if (isPlSql)
            {
                // Só acaba se a última linha for uma barra "/" isolada
                return trimmed.EndsWith("/") && (trimmed.EndsWith("\n/") || trimmed.EndsWith("\r/") || trimmed == "/");
            }

            // Lógica para SQL Normal (INSERT)
            if (!trimmed.EndsWith(";")) return false;

            // VERIFICAÇÃO DE ASPAS (Resolve ORA-01756)
            // Se tem um ";" no final, mas estamos dentro de uma string (ex: 'Texto com ;'), não acabou.
            bool inString = false;
            for (int i = 0; i < sql.Length; i++)
            {
                if (sql[i] == '\'')
                {
                    // Verifica se é escape (duas aspas '')
                    if (inString && i + 1 < sql.Length && sql[i + 1] == '\'')
                        i++; // Pula a próxima
                    else
                        inString = !inString; // Abre ou fecha
                }
            }

            // Se inString for true, significa que tem aspas abertas, então o comando não acabou
            return !inString;
        }

        private void ExecutarSQL(string sql, string tableName, ImportStatistics stats, Dictionary<string, int[]> tableStats, Action<string> logger)
        {
            if (string.IsNullOrWhiteSpace(sql)) return;

            bool isInsert = tableName != "OUTROS" && tableName != "PL/SQL";

            if (!tableStats.ContainsKey(tableName)) tableStats[tableName] = new int[] { 0, 0 };

            if (isInsert)
            {
                stats.TotalInsertsExpected++;
                tableStats[tableName][0]++;
            }

            try
            {
                _queryExecutor.ExecuteNonQuery(sql);

                if (isInsert)
                {
                    stats.TotalInsertsSuccess++;
                    tableStats[tableName][1]++;
                }
            }
            catch (Exception ex)
            {
                // Ignora erro de constraint unique (duplicado) sem parar o processo
                if (ex.Message.Contains("ORA-00001")) return;

                if (!stats.ErrorCountByTable.ContainsKey(tableName)) stats.ErrorCountByTable[tableName] = 0;
                stats.ErrorCountByTable[tableName]++;

                // Não loga erros falsos gerados por lixo do SQL*Plus se algum passar
                if (!ex.Message.Contains("ORA-00900"))
                {
                    logger($"[ERRO] {tableName}: {ex.Message}");
                }
            }
        }

        private string ExtractTableName(string sql)
        {
            try
            {
                if (sql.Length > 200) sql = sql.Substring(0, 200); // Otimização

                // Verifica PL/SQL
                if (sql.TrimStart().ToUpper().StartsWith("DECLARE") || sql.TrimStart().ToUpper().StartsWith("BEGIN"))
                    return "PL/SQL";

                // Verifica INSERT
                var match = Regex.Match(sql, @"INSERT\s+INTO\s+([a-zA-Z0-9_$#]+)", RegexOptions.IgnoreCase);
                if (match.Success) return match.Groups[1].Value.ToUpper();

                return "OUTROS";
            }
            catch { return "OUTROS"; }
        }
    }
}