using System;
using System.Collections.Generic;
using System.Data;
using PLSQLImportFull.Models;

namespace PLSQLImportFull.Data
{
    public class MetadataRepository
    {
        private OracleQueryExecutor _queryExecutor;
        public MetadataRepository(OracleQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
        }
        public List<TriggerInfo> GetAllTriggers(bool enabled)
        {
            var statusCondition = enabled ? "ENABLED" : "DISABLED";

            string query = $@"
SELECT TRIGGER_NAME, TABLE_NAME, STATUS, TRIGGER_TYPE, TRIGGERING_EVENT
  FROM USER_TRIGGERS
 WHERE STATUS = '{statusCondition}'
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
        public List<ConstraintInfo> GetAllConstraints(bool enabled)
        {
            string status = enabled ? "ENABLED" : "DISABLED";
            string type = enabled ? "'R','C'" : "'P','U','R','C'";
            string query = $@"
SELECT  
    CONSTRAINT_NAME, 
    TABLE_NAME, 
    CONSTRAINT_TYPE, 
    STATUS,
    SEARCH_CONDITION,
    R_CONSTRAINT_NAME
FROM USER_CONSTRAINTS
WHERE CONSTRAINT_TYPE IN ({type})
  AND STATUS = '{status}' 
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
