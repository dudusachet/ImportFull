using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Diagnostics;
using PLSQLImportFull.Data;

namespace PLSQLImportFull.Business
{
    /// <summary>
    /// Classe para transportar os resultados da importação para o Formulário.
    /// </summary>
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

        /// <summary>
        /// Processa a lista de arquivos (SQL, PDC ou 7Z) e retorna estatísticas.
        /// </summary>
        public ImportStatistics ExecuteFiles(List<string> filePaths, Action<string> logger)
        {
            var stats = new ImportStatistics();
            stats.TotalScripts = filePaths.Count;

            foreach (string file in filePaths)
            {
                string ext = Path.GetExtension(file).ToLower();

                if (ext == ".7z")
                {
                    ProcessZipFile(file, stats, logger);
                }
                else if (ext == ".sql" || ext == ".pdc")
                {
                    ProcessSqlFile(file, stats, logger);
                }
                else
                {
                    logger($"[AVISO] Formato não suportado ignorado: {Path.GetFileName(file)}");
                }
            }

            return stats;
        }

        // --- Lógica para descompactar e processar ZIP/7Z ---
        private void ProcessZipFile(string zipPath, ImportStatistics stats, Action<string> logger)
        {
            // Procura dentro da pasta Resources
            string sevenZipPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "7za.exe");

            if (!File.Exists(sevenZipPath))
            {
                logger($"[ERRO] 7za.exe não encontrado. Não foi possível abrir: {Path.GetFileName(zipPath)}");
                return;
            }

            // Cria diretório temporário
            string tempDir = Path.Combine(Path.GetTempPath(), "ImportFull_" + Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);

            try
            {
                logger($"Extraindo: {Path.GetFileName(zipPath)}...");

                var p = new ProcessStartInfo
                {
                    FileName = sevenZipPath,
                    Arguments = $"e \"{zipPath}\" -o\"{tempDir}\" -y",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    UseShellExecute = false
                };

                using (var process = Process.Start(p))
                {
                    process.WaitForExit();
                }

                // Busca arquivos extraídos
                var scripts = Directory.GetFiles(tempDir, "*.*", SearchOption.AllDirectories)
                                       .Where(s => s.EndsWith(".sql", StringComparison.OrdinalIgnoreCase) ||
                                                   s.EndsWith(".pdc", StringComparison.OrdinalIgnoreCase))
                                       .OrderBy(s => s) // Ordem alfabética para consistência
                                       .ToList();

                if (scripts.Count == 0)
                {
                    logger($"[AVISO] O arquivo compactado estava vazio ou sem scripts SQL.");
                }
                else
                {
                    foreach (var script in scripts)
                    {
                        ProcessSqlFile(script, stats, logger);
                    }
                }
            }
            catch (Exception ex)
            {
                logger($"[ERRO] Falha ao extrair zip: {ex.Message}");
            }
            finally
            {
                // Limpeza
                try { Directory.Delete(tempDir, true); } catch { }
            }
        }

        // --- Lógica principal de processamento de SQL ---
        private void ProcessSqlFile(string file, ImportStatistics stats, Action<string> logger)
        {
            try
            {
                string fileName = Path.GetFileName(file);
                logger($"Lendo: {fileName}...");

                string scriptContent = File.ReadAllText(file, Encoding.UTF8);

                // 1. Remove comandos de formatação do SQL*Plus
                scriptContent = RemoveSqlPlusCommands(scriptContent);

                // 2. Quebra o script em comandos individuais
                List<string> commands = SplitSqlStatements(scriptContent);

                int fileInserts = 0;
                int fileSuccess = 0;

                foreach (var sql in commands)
                {
                    if (string.IsNullOrWhiteSpace(sql)) continue;

                    // Identifica se é INSERT para as estatísticas
                    bool isInsert = sql.Trim().StartsWith("INSERT", StringComparison.OrdinalIgnoreCase);
                    string tableName = isInsert ? ExtractTableName(sql) : "OUTROS";

                    if (isInsert)
                    {
                        fileInserts++;
                        stats.TotalInsertsExpected++;
                    }

                    try
                    {
                        _queryExecutor.ExecuteNonQuery(sql);

                        if (isInsert)
                        {
                            fileSuccess++;
                            stats.TotalInsertsSuccess++;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Registra erro na tabela específica
                        if (!stats.ErrorCountByTable.ContainsKey(tableName))
                            stats.ErrorCountByTable[tableName] = 0;

                        stats.ErrorCountByTable[tableName]++;

                        // Log detalhado do erro
                        logger($"[ERRO] {tableName}: {ex.Message}");
                    }
                }

                logger($"OK: {fileName} (Inseridos: {fileSuccess}/{fileInserts})");
            }
            catch (Exception ex)
            {
                logger($"[FATAL] Erro ao abrir arquivo {Path.GetFileName(file)}: {ex.Message}");
            }
        }

        // Remove comandos que o Oracle.ManagedDataAccess não entende (SET, PROMPT, etc)
        private string RemoveSqlPlusCommands(string script)
        {
            StringBuilder sb = new StringBuilder();
            using (StringReader sr = new StringReader(script))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string trimmed = line.Trim().ToUpper();

                    if (trimmed.StartsWith("SET ") ||
                        trimmed.StartsWith("PROMPT") ||
                        trimmed.StartsWith("EXIT") ||
                        trimmed.StartsWith("QUIT") ||
                        trimmed.StartsWith("--") ||
                        trimmed.StartsWith("COMMIT")) // O ADO.NET gerencia transação se quiser, ou deixa passar
                    {
                        continue;
                    }
                    sb.AppendLine(line);
                }
            }
            return sb.ToString();
        }

        // Divide o script gigante em comandos menores
        private List<string> SplitSqlStatements(string script)
        {
            List<string> commands = new List<string>();

            // Divide primeiro por "/" (padrão de blocos PL/SQL)
            string[] rawParts = script.Split(new[] { "\r\n/", "\n/", "\r/" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in rawParts)
            {
                string trimmedPart = part.Trim();
                if (string.IsNullOrWhiteSpace(trimmedPart)) continue;

                // Se for bloco PL/SQL, adiciona inteiro
                if (trimmedPart.StartsWith("DECLARE", StringComparison.OrdinalIgnoreCase) ||
                    trimmedPart.StartsWith("BEGIN", StringComparison.OrdinalIgnoreCase) ||
                    trimmedPart.StartsWith("CREATE OR REPLACE", StringComparison.OrdinalIgnoreCase))
                {
                    commands.Add(trimmedPart);
                }
                else
                {
                    // Se for SQL comum (INSERTs), divide por ponto e vírgula
                    string[] statements = trimmedPart.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var stmt in statements)
                    {
                        if (!string.IsNullOrWhiteSpace(stmt))
                            commands.Add(stmt.Trim());
                    }
                }
            }
            return commands;
        }

        // Extrai nome da tabela para o relatório de erros
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