using System;
using System.Collections.Generic;
using System.Data;
using PLSQLImportFull.Models;

namespace PLSQLImportFull.Data
{
    /// <summary>
    /// Repositório para consultas ao dicionário de dados Oracle
    /// </summary>
    public class MetadataRepository
    {
        private OracleQueryExecutor _queryExecutor;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="queryExecutor">Executor de queries</param>
        public MetadataRepository(OracleQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
        }

        /// <summary>
        /// Obtém lista de todas as triggers do usuário
        /// </summary>
        /// <returns>Lista de TriggerInfo</returns>
        public List<TriggerInfo> GetAllTriggers()
        {
            string query = @"
                SELECT TRIGGER_NAME, TABLE_NAME, STATUS, TRIGGER_TYPE, TRIGGERING_EVENT
                FROM USER_TRIGGERS
                ORDER BY TABLE_NAME, TRIGGER_NAME";

            DataTable dt = _queryExecutor.ExecuteQuery(query);
            List<TriggerInfo> triggers = new List<TriggerInfo>();

            foreach (DataRow row in dt.Rows)
            {
                triggers.Add(new TriggerInfo
                {
                    TriggerName = row["TRIGGER_NAME"].ToString(),
                    TableName = row["TABLE_NAME"].ToString(),
                    Status = row["STATUS"].ToString(),
                    TriggerType = row["TRIGGER_TYPE"].ToString(),
                    TriggeringEvent = row["TRIGGERING_EVENT"].ToString()
                });
            }

            return triggers;
        }

        /// <summary>
        /// Obtém lista de todas as constraints do usuário
        /// </summary>
        /// <returns>Lista de ConstraintInfo</returns>
        public List<ConstraintInfo> GetAllConstraints()
        {
            string query = @"
                SELECT 
                    CONSTRAINT_NAME, 
                    TABLE_NAME, 
                    CONSTRAINT_TYPE, 
                    STATUS,
                    SEARCH_CONDITION,
                    R_CONSTRAINT_NAME
                FROM USER_CONSTRAINTS
                WHERE CONSTRAINT_TYPE IN ('P', 'R', 'U', 'C')
                ORDER BY TABLE_NAME, CONSTRAINT_NAME";

            DataTable dt = _queryExecutor.ExecuteQuery(query);
            List<ConstraintInfo> constraints = new List<ConstraintInfo>();

            foreach (DataRow row in dt.Rows)
            {
                string constraintType = row["CONSTRAINT_TYPE"].ToString();
                string constraintTypeDesc = GetConstraintTypeDescription(constraintType);

                constraints.Add(new ConstraintInfo
                {
                    ConstraintName = row["CONSTRAINT_NAME"].ToString(),
                    TableName = row["TABLE_NAME"].ToString(),
                    ConstraintType = constraintType,
                    ConstraintTypeDescription = constraintTypeDesc,
                    Status = row["STATUS"].ToString(),
                    SearchCondition = row["SEARCH_CONDITION"] != DBNull.Value ? row["SEARCH_CONDITION"].ToString() : null,
                    RConstraintName = row["R_CONSTRAINT_NAME"] != DBNull.Value ? row["R_CONSTRAINT_NAME"].ToString() : null
                });
            }

            return constraints;
        }

        /// <summary>
        /// Obtém lista de todas as tabelas do usuário
        /// </summary>
        /// <returns>Lista de TableInfo</returns>
        public List<TableInfo> GetAllTables()
        {
            string query = @"
                SELECT 
                    t.TABLE_NAME,
                    t.NUM_ROWS,
                    t.TABLESPACE_NAME
                FROM USER_TABLES t
                ORDER BY t.TABLE_NAME";

            DataTable dt = _queryExecutor.ExecuteQuery(query);
            List<TableInfo> tables = new List<TableInfo>();

            foreach (DataRow row in dt.Rows)
            {
                tables.Add(new TableInfo
                {
                    TableName = row["TABLE_NAME"].ToString(),
                    NumRows = row["NUM_ROWS"] != DBNull.Value ? Convert.ToInt64(row["NUM_ROWS"]) : 0,
                    TablespaceName = row["TABLESPACE_NAME"] != DBNull.Value ? row["TABLESPACE_NAME"].ToString() : null
                });
            }

            return tables;
        }

        /// <summary>
        /// Obtém o DDL de uma tabela
        /// </summary>
        /// <param name="tableName">Nome da tabela</param>
        /// <returns>Script DDL</returns>
        public string GetTableDDL(string tableName)
        {
            try
            {
                string query = $"SELECT DBMS_METADATA.GET_DDL('TABLE', '{tableName}') FROM DUAL";
                object result = _queryExecutor.ExecuteScalar(query);
                return result != null ? result.ToString() : string.Empty;
            }
            catch (Exception ex)
            {
                return $"-- Erro ao obter DDL: {ex.Message}";
            }
        }

        /// <summary>
        /// Obtém o DDL de uma constraint
        /// </summary>
        /// <param name="constraintName">Nome da constraint</param>
        /// <returns>Script DDL</returns>
        public string GetConstraintDDL(string constraintName)
        {
            try
            {
                string query = $"SELECT DBMS_METADATA.GET_DDL('CONSTRAINT', '{constraintName}') FROM DUAL";
                object result = _queryExecutor.ExecuteScalar(query);
                return result != null ? result.ToString() : string.Empty;
            }
            catch (Exception ex)
            {
                return $"-- Erro ao obter DDL: {ex.Message}";
            }
        }

        /// <summary>
        /// Obtém descrição do tipo de constraint
        /// </summary>
        private string GetConstraintTypeDescription(string type)
        {
            switch (type)
            {
                case "P":
                    return "Primary Key";
                case "R":
                    return "Foreign Key";
                case "U":
                    return "Unique";
                case "C":
                    return "Check";
                default:
                    return type;
            }
        }
    }
}
