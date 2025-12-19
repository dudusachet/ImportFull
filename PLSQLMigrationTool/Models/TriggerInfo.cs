namespace PLSQLImportFull.Models
{
    /// <summary>
    /// Representa informações de uma trigger do banco de dados
    /// </summary>
    public class TriggerInfo
    {
        /// <summary>
        /// Nome da trigger
        /// </summary>
        public string TriggerName { get; set; }

        /// <summary>
        /// Nome da tabela associada
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Status da trigger (ENABLED/DISABLED)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Tipo da trigger (BEFORE/AFTER, etc)
        /// </summary>
        public string TriggerType { get; set; }

        /// <summary>
        /// Evento que dispara a trigger (INSERT/UPDATE/DELETE)
        /// </summary>
        public string TriggeringEvent { get; set; }

    }
}
