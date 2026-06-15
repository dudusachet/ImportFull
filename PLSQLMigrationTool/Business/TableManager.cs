using System;
using System.Collections.Generic;
using PLSQLImportFull.Data;
using PLSQLImportFull.Models;

namespace PLSQLImportFull.Business
{
    public class TableManager
    {
        private OracleQueryExecutor _queryExecutor;
        private MetadataRepository _metadataRepository;
        public TableManager(OracleQueryExecutor queryExecutor, MetadataRepository metadataRepository)
        {
            _queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
            _metadataRepository = metadataRepository ?? throw new ArgumentNullException(nameof(metadataRepository));
        }
        public List<TableInfo> GetAllTables()
        {
            var tables = _metadataRepository.GetAllTables();

            if (tables != null)
            {
                tables.RemoveAll(t => t.TableName.Equals("GER_DDL_LOG", StringComparison.OrdinalIgnoreCase));
            }

            return tables;
        }
        public void TruncateTable(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentException("Nome da tabela não pode ser vazio.", nameof(tableName));
            }

            string command = $"TRUNCATE TABLE {tableName}";
            _queryExecutor.ExecuteNonQuery(command);
        }
        public int TruncateTables(List<string> tableNames)
        {
            if (tableNames == null || tableNames.Count == 0)
            {
                return 0;
            }

            int successCount = 0;
            List<string> errors = new List<string>();

            foreach (string tableName in tableNames)
            {
                // --- NOVA REGRA 1: Ignorar a GER_DDL_LOG silenciosamente ---
                if (tableName.Equals("GER_DDL_LOG", StringComparison.OrdinalIgnoreCase))
                {
                    continue; // Pula para a próxima tabela sem fazer nada
                }

                try
                {
                    TruncateTable(tableName);
                    successCount++;
                }
                catch (Exception ex)
                {
                    // --- NOVA REGRA 2: Ignorar se a tabela não existe ---
                    if (ex.Message.Contains("ORA-00942"))
                    {
                        continue; // Apenas ignora o erro e não joga na tela
                    }

                    // Se for um erro grave de verdade (ex: banco caiu), aí sim guarda o erro
                    errors.Add($"Erro ao truncar tabela {tableName}: {ex.Message}");
                }
            }

            if (errors.Count > 0)
            {
                throw new Exception($"Algumas tabelas não puderam ser truncadas:\n{string.Join("\n", errors)}");
            }

            return successCount;
        }
    }
}
