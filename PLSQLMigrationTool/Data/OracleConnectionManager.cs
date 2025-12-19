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
        public void ExecuteNonQuery(string sql)
        {
            if (!IsConnected)
                throw new InvalidOperationException("Banco desconectado.");

            using (OracleCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }
        public void Dispose()
        {
            Disconnect();
            GC.SuppressFinalize(this);
        }
    }
}