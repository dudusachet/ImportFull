using System;

namespace PLSQLImportFull.Models
{
    /// <summary>
    /// Representa informações de uma tabela do banco de dados
    /// </summary>
    public class TableInfo
    {
        /// <summary>
        /// Nome da tabela
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Número de linhas (aproximado)
        /// </summary>
        public long NumRows { get; set; }

        /// <summary>
        /// Nome do tablespace
        /// </summary>
        public string TablespaceName { get; set; }

        /// <summary>
        /// Retorna representação em string
        /// </summary>
        public override string ToString()
        {
            return $"{TableName} ({NumRows:N0} linhas)";
        }
    }
}
