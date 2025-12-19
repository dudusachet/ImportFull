using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace PLSQLImportFull.Data
{
    public class OracleQueryExecutor
    {
        private OracleConnectionManager _connectionManager;
        public OracleQueryExecutor(OracleConnectionManager connectionManager)
        {
            _connectionManager = connectionManager ?? throw new ArgumentNullException(nameof(connectionManager));
        }
        public DataTable ExecuteQuery(string query)
        {
            if (!_connectionManager.IsConnected)
            {
                throw new InvalidOperationException("Não há conexão ativa com o banco de dados.");
            }

            DataTable dataTable = new DataTable();

            using (OracleCommand command = new OracleCommand(query, _connectionManager.Connection))
            {
                using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }

            return dataTable;
        }
        public int ExecuteNonQuery(string commandText)
        {
            if (!_connectionManager.IsConnected)
            {
                throw new InvalidOperationException("Não há conexão ativa com o banco de dados.");
            }

            using (OracleCommand command = new OracleCommand(commandText, _connectionManager.Connection))
            {
                command.CommandType = CommandType.Text;
                return command.ExecuteNonQuery();
            }
        }
    }
}
