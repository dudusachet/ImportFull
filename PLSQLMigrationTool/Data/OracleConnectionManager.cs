using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace PLSQLImportFull.Data
{
    public class OracleConnectionManager : IDisposable
    {
        private string _connectionString;
        private OracleConnection _connection;
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }
        public OracleConnection Connection
        {
            get { return _connection; }
        }
        public bool IsConnected
        {
            get { return _connection != null && _connection.State == ConnectionState.Open; }
        }

        public OracleConnectionManager() { }

        public OracleConnectionManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Adicione estes métodos dentro da classe OracleConnectionManager

        /// <summary>
        /// Executa um SELECT e retorna um DataTable
        /// </summary>
        public DataTable ExecuteQuery(string query)
        {
            if (_connection == null || _connection.State != ConnectionState.Open)
                throw new Exception("Conexão não está aberta.");

            using (OracleCommand cmd = new OracleCommand(query, _connection))
            {
                using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        /// <summary>
        /// Executa comandos sem retorno (UPDATE, DELETE, ALTER, INSERT)
        /// </summary>
        public void ExecuteNonQuery(string commandText)
        {
            if (_connection == null || _connection.State != ConnectionState.Open)
                throw new Exception("Conexão não está aberta.");

            using (OracleCommand cmd = new OracleCommand(commandText, _connection))
            {
                cmd.ExecuteNonQuery();
            }
        }
        public bool TestConnection()
        {
            try
            {
                using (OracleConnection testConn = new OracleConnection(_connectionString))
                {
                    testConn.Open();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void Connect()
        {
            // Se já estiver conectado, não faz nada
            if (IsConnected) return;

            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("String de conexão não foi definida.");
            }

            // Se existir um objeto de conexão antigo (fechado ou quebrado), limpa ele antes
            if (_connection != null)
            {
                _connection.Dispose();
            }

            _connection = new OracleConnection(_connectionString);
            _connection.Open();
        }
        public void Disconnect()
        {
            if (_connection != null)
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
                _connection.Dispose();
                _connection = null;
            }
        }
        public void Dispose()
        {
            Disconnect();
            GC.SuppressFinalize(this);
        }
    }
}