namespace PLSQLImportFull.Models
{
    /// <summary>
    /// Representa informações de uma constraint do banco de dados
    /// </summary>
    public class ConstraintInfo
    {
        /// <summary>
        /// Nome da constraint
        /// </summary>
        public string ConstraintName { get; set; }

        /// <summary>
        /// Nome da tabela associada
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Tipo da constraint (P=PK, R=FK, U=Unique, C=Check)
        /// </summary>
        public string ConstraintType { get; set; }

        /// <summary>
        /// Descrição do tipo da constraint
        /// </summary>
        public string ConstraintTypeDescription { get; set; }

        /// <summary>
        /// Status da constraint (ENABLED/DISABLED)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Condição de busca (para constraints CHECK)
        /// </summary>
        public string SearchCondition { get; set; }

        /// <summary>
        /// Nome da constraint referenciada (para Foreign Keys)
        /// </summary>
        public string RConstraintName { get; set; }
    }
}
