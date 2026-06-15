using PLSQLImportFull.Data;
using PLSQLImportFull.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLSQLImportFull.Business
{
    public class ConstraintManager
    {
        private OracleQueryExecutor _queryExecutor;
        private MetadataRepository _metadataRepository;

        public ConstraintManager(OracleQueryExecutor queryExecutor, MetadataRepository metadataRepository)
        {
            _queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
            _metadataRepository = metadataRepository ?? throw new ArgumentNullException(nameof(metadataRepository));
        }

        public List<ConstraintInfo> GetAllConstraints(bool enabled)
        {
            return _metadataRepository.GetAllConstraints(enabled);
        }

        public void DisableConstraint(ConstraintInfo constraint)
        {
            if (constraint == null) throw new ArgumentNullException(nameof(constraint));

            if (string.IsNullOrEmpty(constraint.TableName) || string.IsNullOrEmpty(constraint.ConstraintName))
            {
                throw new ArgumentException("Nome da tabela e constraint não podem ser vazios.");
            }

            string command = $"ALTER TABLE {constraint.TableName} DISABLE CONSTRAINT {constraint.ConstraintName}";
            _queryExecutor.ExecuteNonQuery(command);
        }

        public int DisableConstraints(List<ConstraintInfo> constraints)
        {
            if (constraints == null || constraints.Count == 0) return 0;

            int successCount = 0;
            List<string> errors = new List<string>();

            foreach (ConstraintInfo constraint in constraints)
            {
                try
                {
                    DisableConstraint(constraint);
                    successCount++;
                }
                catch (Exception ex)
                {
                    errors.Add($"Erro ao desabilitar constraint {constraint.ConstraintName}: {ex.Message}");
                }
            }

            if (errors.Count > 0)
            {
                throw new Exception($"Algumas constraints não puderam ser desabilitadas:\n{string.Join("\n", errors)}");
            }

            return successCount;
        }

        public int EnableConstraints(List<ConstraintInfo> constraints, out string relatorioErros)
        {
            int successCount = 0;
            StringBuilder errorReport = new StringBuilder();
            relatorioErros = string.Empty; // Começa vazio

            if (constraints == null || constraints.Count == 0) return 0;

            foreach (var c in constraints)
            {
                try
                {
                    // Tenta habilitar usando NOVALIDATE (mais rápido e tolerante a dados antigos)
                    string sql = $"ALTER TABLE {c.TableName} ENABLE NOVALIDATE CONSTRAINT {c.ConstraintName}";
                    _queryExecutor.ExecuteNonQuery(sql);
                    successCount++;
                }
                catch (Exception ex)
                {
                    // ORA-02430: constraint não existe (ignora silenciosamente)
                    if (ex.Message.Contains("ORA-02430"))
                    {
                        continue;
                    }

                    // Se for erro real, anota no relatório
                    errorReport.AppendLine($"[FALHA] {c.TableName}.{c.ConstraintName}: {ex.Message}");
                }
            }

            // Se houve erros, preenche a variável de saída (out)
            if (errorReport.Length > 0)
            {
                relatorioErros = $"Foram habilitadas {successCount} de {constraints.Count} constraints.\n\n" +
                                 $"AS SEGUINTES CONSTRAINTS FALHARAM E PRECISAM DE ATENÇÃO:\n\n{errorReport.ToString()}";
            }

            return successCount;
        }
    }
}