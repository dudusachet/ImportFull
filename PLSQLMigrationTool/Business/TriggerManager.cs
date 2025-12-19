using System;
using System.Collections.Generic;
using PLSQLImportFull.Data;
using PLSQLImportFull.Models;

namespace PLSQLImportFull.Business
{
    public class TriggerManager
    {
        private OracleQueryExecutor _queryExecutor;
        private MetadataRepository _metadataRepository;
        public TriggerManager(OracleQueryExecutor queryExecutor, MetadataRepository metadataRepository)
        {
            _queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
            _metadataRepository = metadataRepository ?? throw new ArgumentNullException(nameof(metadataRepository));
        }
        public List<TriggerInfo> GetAllTriggers(bool enabled)
        {
            return _metadataRepository.GetAllTriggers(enabled);
        }
        public void DisableTrigger(string triggerName)
        {
            if (string.IsNullOrEmpty(triggerName))
            {
                throw new ArgumentException("Nome da trigger não pode ser vazio.", nameof(triggerName));
            }

            string command = $"ALTER TRIGGER {triggerName} DISABLE";
            _queryExecutor.ExecuteNonQuery(command);
        }
        public void EnableTrigger(string triggerName)
        {
            if (string.IsNullOrEmpty(triggerName))
            {
                throw new ArgumentException("Nome da trigger não pode ser vazio.", nameof(triggerName));
            }

            string command = $"ALTER TRIGGER {triggerName} ENABLE";
            _queryExecutor.ExecuteNonQuery(command);
        }
        public int DisableTriggers(List<string> triggerNames)
        {
            if (triggerNames == null || triggerNames.Count == 0)
            {
                return 0;
            }

            int successCount = 0;
            List<string> errors = new List<string>();

            foreach (string triggerName in triggerNames)
            {
                try
                {
                    DisableTrigger(triggerName);
                    successCount++;
                }
                catch (Exception ex)
                {
                    errors.Add($"Erro ao desabilitar trigger {triggerName}: {ex.Message}");
                }
            }

            if (errors.Count > 0)
            {
                throw new Exception($"Algumas triggers não puderam ser desabilitadas:\n{string.Join("\n", errors)}");
            }

            return successCount;
        }
        public int EnableTriggers(List<string> triggerNames)
        {
            if (triggerNames == null || triggerNames.Count == 0)
            {
                return 0;
            }

            int successCount = 0;
            List<string> errors = new List<string>();

            foreach (string triggerName in triggerNames)
            {
                try
                {
                    EnableTrigger(triggerName);
                    successCount++;
                }
                catch (Exception ex)
                {
                    errors.Add($"Erro ao habilitar trigger {triggerName}: {ex.Message}");
                }
            }

            if (errors.Count > 0)
            {
                throw new Exception($"Algumas triggers não puderam ser habilitadas:\n{string.Join("\n", errors)}");
            }

            return successCount;
        }
    }
}
