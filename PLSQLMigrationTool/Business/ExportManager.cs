using PLSQLImportFull.Data; 
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using PLSQLImportFull.Models;

namespace PLSQLImportFull.Business
{
    public class ImportManager
    {
        private OracleQueryExecutor _queryExecutor;

        public ImportManager(OracleQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor ?? throw new ArgumentNullException("queryExecutor");
        }

        /// <summary>
        /// Executa uma lista de arquivos SQL no banco
        /// </summary>
        public void ExecuteFiles(List<string> filePaths, Action<string> logger)
        {
            foreach (string file in filePaths)
            {
                try
                {
                    logger($"Lendo arquivo: {Path.GetFileName(file)}...");
                    string scriptContent = File.ReadAllText(file, Encoding.UTF8);

                    // 1. Limpar comandos SQL*Plus (SET, PROMPT, EXIT, QUIT)
                    // O driver ADO.NET não entende esses comandos e dá erro se tentar executar
                    scriptContent = RemoveSqlPlusCommands(scriptContent);

                    // 2. Quebrar o script em comandos individuais
                    // Oracle não executa scripts inteiros de uma vez no ADO.NET, precisa ser comando a comando
                    List<string> commands = SplitSqlStatements(scriptContent);

                    // 3. Executar comando a comando
                    int successCount = 0;
                    foreach (var sql in commands)
                    {
                        if (string.IsNullOrWhiteSpace(sql)) continue;

                        try
                        {
                            _queryExecutor.ExecuteNonQuery(sql); // Usa o método que já existe no seu Executor
                            successCount++;
                        }
                        catch (Exception ex)
                        {
                            // Loga o erro mas tenta continuar (ou pare, dependendo da regra de negócio)
                            logger($"[ERRO] Falha no comando: {sql.Substring(0, Math.Min(50, sql.Length))}... \nMsg: {ex.Message}");
                        }
                    }

                    logger($"Concluído: {Path.GetFileName(file)} ({successCount} comandos executados).");
                }
                catch (Exception ex)
                {
                    logger($"[FATAL] Erro ao processar arquivo {Path.GetFileName(file)}: {ex.Message}");
                }
            }
        }

        private string RemoveSqlPlusCommands(string script)
        {
            StringBuilder sb = new StringBuilder();
            using (StringReader sr = new StringReader(script))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string trimmed = line.Trim().ToUpper();
                    // Ignora linhas que começam com comandos exclusivos do SQL*Plus
                    if (trimmed.StartsWith("SET ") ||
                        trimmed.StartsWith("PROMPT") ||
                        trimmed.StartsWith("EXIT") ||
                        trimmed.StartsWith("QUIT") ||
                        trimmed.StartsWith("--"))
                    {
                        continue;
                    }
                    sb.AppendLine(line);
                }
            }
            return sb.ToString();
        }

        private List<string> SplitSqlStatements(string script)
        {
            List<string> commands = new List<string>();

            // Esta é uma lógica simplificada de parser. 
            // Scripts Oracle complexos com PL/SQL (BEGIN...END) precisam ser tratados pelo '/'
            // Comandos SQL normais (INSERT, CREATE) terminam com ';'

            // Vamos usar uma estratégia mista:
            // Se encontrar uma linha que é apenas "/", considera o bloco anterior um comando.
            // Se não, quebra por ";" (cuidado com ; dentro de strings, mas para DML simples funciona)

            string[] rawParts = script.Split(new[] { "\r\n/", "\n/", "\r/" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in rawParts)
            {
                string trimmedPart = part.Trim();
                if (string.IsNullOrWhiteSpace(trimmedPart)) continue;

                // Se parece um bloco PL/SQL (BEGIN/DECLARE), adiciona inteiro
                if (trimmedPart.StartsWith("DECLARE", StringComparison.OrdinalIgnoreCase) ||
                    trimmedPart.StartsWith("BEGIN", StringComparison.OrdinalIgnoreCase) ||
                    trimmedPart.StartsWith("CREATE OR REPLACE", StringComparison.OrdinalIgnoreCase))
                {
                    commands.Add(trimmedPart);
                }
                else
                {
                    // Se for SQL comum (INSERT, UPDATE), pode ter vários separados por ;
                    // Remove o ; final para o Oracle executar
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
    }
}