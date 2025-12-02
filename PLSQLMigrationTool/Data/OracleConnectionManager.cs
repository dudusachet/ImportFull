using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace PLSQLImportFull.Data
{
    /// <summary>
    /// Gerencia conexões com o banco de dados Oracle
    /// </summary>
    public class OracleConnectionManager : IDisposable
    {
        private string _connectionString;
        private OracleConnection _connection;

        /// <summary>
        /// Obtém ou define a string de conexão
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        /// Obtém a conexão atual
        /// </summary>
        public OracleConnection Connection
        {
            get { return _connection; }
        }

        /// <summary>
        /// Verifica se está conectado
        /// </summary>
        public bool IsConnected
        {
            get { return _connection != null && _connection.State == ConnectionState.Open; }
        }

        public OracleConnectionManager() { }

        public OracleConnectionManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Testa a conexão com o banco de dados
        /// </summary>
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

        /// <summary>
        /// Abre uma conexão com o banco de dados
        /// </summary>
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

        /// <summary>
        /// Fecha a conexão com o banco de dados
        /// </summary>
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

        /// <summary>
        /// Obtém uma nova conexão (para operações paralelas/threads)
        /// </summary>
        public OracleConnection GetNewConnection()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("String de conexão não foi definida.");
            }

            OracleConnection newConn = new OracleConnection(_connectionString);
            newConn.Open();
            return newConn;
        }

        /// <summary>
        /// Executa um comando SQL sem retorno (INSERT, UPDATE, DELETE, PL/SQL Block)
        /// Útil para comandos rápidos como ALTER SESSION ou DBMS_STATS
        /// </summary>
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

        /// <summary>
        /// Libera recursos da classe (Implementação de IDisposable)
        /// </summary>
        public void Dispose()
        {
            Disconnect();
            GC.SuppressFinalize(this);
        }
    }
}