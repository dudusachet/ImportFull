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
        private OracleQueryExecutor _queryExecutor;

        public ImportManager(OracleQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor ?? throw new ArgumentNullException("queryExecutor");
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

        private void ProcessSqlFile(string file, ImportStatistics stats, Action<string> logger)
        {
            try
            {
                string fileName = Path.GetFileName(file);
                logger($"--------------------------------------------------");
                logger($"Arquivo: {fileName}");

                var tableStats = new Dictionary<string, int[]>(); // [Total, Sucesso]
                int fileInserts = 0;

                // Lê o arquivo comando por comando (Streaming)
                foreach (var rawSql in ReadAndParseSqlCommands(file))
                {
                    if (string.IsNullOrWhiteSpace(rawSql)) continue;

                    // --- CORREÇÃO PRINCIPAL ---
                    // Em vez de descartar o bloco se tiver PROMPT, nós limpamos ele.
                    // Isso preserva o INSERT que vem logo depois do cabeçalho.
                    string sql = SanitizeSql(rawSql);

                    if (string.IsNullOrWhiteSpace(sql)) continue;

                    bool isInsert = sql.TrimStart().StartsWith("INSERT", StringComparison.OrdinalIgnoreCase);
                    string tableName = isInsert ? ExtractTableName(sql) : "OUTROS_COMANDOS";

                    if (!tableStats.ContainsKey(tableName)) tableStats[tableName] = new int[] { 0, 0 };

                    if (isInsert)
                    {
                        fileInserts++;
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
                        if (!stats.ErrorCountByTable.ContainsKey(tableName)) stats.ErrorCountByTable[tableName] = 0;

                        if (ex.Message.Contains("ORA-00001"))
                        {
                            // Ignora duplicado (não conta como sucesso, nem como erro crítico)
                        }
                        else
                        {
                            stats.ErrorCountByTable[tableName]++;
                            logger($"[ERRO] {tableName}: {ex.Message}");
                        }
                    }
                }

                // Resumo
                if (tableStats.Count > 0)
                {
                    foreach (var kvp in tableStats)
                    {
                        if (kvp.Key == "OUTROS_COMANDOS" || kvp.Key == "DESCONHECIDA") continue;
                        logger($"OK: {kvp.Key} (Inseridos: {kvp.Value[1]}/{kvp.Value[0]})");
                    }
                }

                if (fileInserts == 0) logger($"Info: Arquivo processado (Sem INSERTs).");
            }
            catch (Exception ex)
            {
                logger($"[FATAL] Erro ao abrir arquivo: {ex.Message}");
            }
        }

        // Remove apenas linhas de configuração do SQL*Plus, preservando o SQL real
        private string SanitizeSql(string sql)
        {
            // Otimização: se não tem comandos de script, retorna rápido
            string upper = sql.TrimStart().ToUpper();
            if (!upper.StartsWith("SET") && !upper.StartsWith("PROMPT") &&
                !upper.StartsWith("EXIT") && !upper.StartsWith("QUIT"))
            {
                return sql;
            }

            StringBuilder sb = new StringBuilder();
            using (StringReader sr = new StringReader(sql))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string trimmed = line.Trim().ToUpper();

                    // Remove linhas que causam erro no driver Oracle Managed
                    if (trimmed.StartsWith("SET ") ||
                        trimmed.StartsWith("PROMPT") ||
                        trimmed.StartsWith("SPOOL ") ||
                        trimmed.StartsWith("WHENEVER ") ||
                        trimmed.StartsWith("EXIT") ||
                        trimmed.StartsWith("QUIT"))
                    {
                        continue;
                    }

                    sb.AppendLine(line);
                }
            }
            return sb.ToString().Trim();
        }

        // =========================================================================
        // PARSER DE STREAM SEGURO (CORREÇÃO DE BORDAS DE BUFFER)
        // =========================================================================
        private IEnumerable<string> ReadAndParseSqlCommands(string filePath)
        {
            StringBuilder sb = new StringBuilder();
            bool inString = false;
            bool inLineComment = false;

            using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
            {
                int nextChar;
                while ((nextChar = sr.Read()) != -1)
                {
                    char c = (char)nextChar;

                    // 1. Ignora conteúdo de comentários de linha
                    if (inLineComment)
                    {
                        if (c == '\n') inLineComment = false;
                        continue;
                    }

                    // 2. Verifica início de comentário (--)
                    if (!inString && c == '-' && sr.Peek() == '-')
                    {
                        inLineComment = true;
                        sr.Read(); // Consome o segundo traço
                        continue;
                    }

                    // 3. Lógica de Aspas (Strings)
                    if (c == '\'')
                    {
                        if (inString)
                        {
                            // Verifica aspa escapada ('') olhando o próximo char
                            if (sr.Peek() == '\'')
                            {
                                sb.Append(c);
                                sb.Append((char)sr.Read()); // Consome e adiciona a segunda aspa
                                continue;
                            }
                            inString = false; // Fecha a string
                        }
                        else
                        {
                            inString = true; // Abre a string
                        }
                    }

                    // 4. Ponto e vírgula (Fim de comando)
                    if (c == ';' && !inString)
                    {
                        string cmd = sb.ToString().Trim();
                        if (cmd.Length > 0) yield return cmd;
                        sb.Clear();
                        continue;
                    }

                    // 5. Barra / (Fim de bloco PL/SQL)
                    if (c == '/' && !inString)
                    {
                        string currentStr = sb.ToString().Trim();
                        if (currentStr.Length == 0) // Barra isolada no inicio
                        {
                            sb.Clear();
                            continue;
                        }
                        // Verifica se a barra está isolada no final de um bloco
                        if (sb.Length > 0 && (sb[sb.Length - 1] == '\n' || sb[sb.Length - 1] == '\r'))
                        {
                            string cmd = sb.ToString().Trim();
                            if (cmd.Length > 0) yield return cmd;
                            sb.Clear();
                            continue;
                        }
                    }

                    sb.Append(c);
                }
            }

            if (sb.Length > 0)
            {
                string cmd = sb.ToString().Trim();
                if (cmd.Length > 0) yield return cmd;
            }
        }

        private string ExtractTableName(string sql)
        {
            try
            {
                var match = Regex.Match(sql, @"INSERT\s+INTO\s+([a-zA-Z0-9_$#]+)", RegexOptions.IgnoreCase);
                if (match.Success) return match.Groups[1].Value.ToUpper();
                return "DESCONHECIDA";
            }
            catch { return "DESCONHECIDA"; }
        }
    }
}