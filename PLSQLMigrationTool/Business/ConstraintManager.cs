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
            if (constraint == null)
            {
                throw new ArgumentNullException(nameof(constraint));
            }

            if (string.IsNullOrEmpty(constraint.TableName) || string.IsNullOrEmpty(constraint.ConstraintName))
            {
                throw new ArgumentException("Nome da tabela e constraint não podem ser vazios.");
            }

            string command = $"ALTER TABLE {constraint.TableName} DISABLE CONSTRAINT {constraint.ConstraintName}";
            _queryExecutor.ExecuteNonQuery(command);
        }

        public int DisableConstraints(List<ConstraintInfo> constraints)
        {
            if (constraints == null || constraints.Count == 0)
            {
                return 0;
            }

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
        public int EnableConstraints(List<ConstraintInfo> constraints)
        {
            int successCount = 0;
            StringBuilder errorReport = new StringBuilder();

            foreach (var c in constraints)
            {
                try
                {
                    // Tenta habilitar usando NOVALIDATE (mais rápido e tolerante a dados antigos)
                    // Se a constraint não existir mais (foi dropada), vai gerar erro aqui
                    string sql = $"ALTER TABLE {c.TableName} ENABLE NOVALIDATE CONSTRAINT {c.ConstraintName}";
                    _queryExecutor.ExecuteNonQuery(sql);
                    successCount++;
                }
                catch (Exception ex)
                {
                    // Filtra erros irrelevantes
                    // ORA-02430: constraint não existe (acontece se você dropou as checks de usuario/maquina antes)
                    if (ex.Message.Contains("ORA-02430"))
                    {
                        // Apenas ignora, pois se não existe, não precisa habilitar
                        continue;
                    }

                    // Se for outro erro (ex: ORA-02298 - Pai não encontrado), adiciona ao relatório
                    errorReport.AppendLine($"[FALHA] {c.TableName}.{c.ConstraintName}: {ex.Message}");
                }
            }

            // Se houve erros reais, lança uma exceção para o MainForm mostrar na tela
            if (errorReport.Length > 0)
            {
                // Adiciona um cabeçalho ao erro
                string finalMsg = $"Habilitadas: {successCount}/{constraints.Count}\n\nERROS ENCONTRADOS:\n{errorReport.ToString()}";
                throw new Exception(finalMsg);
            }
            return successCount;
        }
    }
}
