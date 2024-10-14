namespace BSG.ADInventory.Data
{
    using System.Collections.Generic;
    using Zcf.Data;
    using BSG.ADInventory.Entities;
		
    /// <summary>
    /// Create Interface for ShaparakReportRepository
    /// </summary>
    public interface ISqlCommandRepository : IQueryableRepository<SqlCommand>
	{
        /// <summary>
        /// Executes the stored procedure list.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters)
            where TEntity : class, new();

        /// <summary>
        /// Executes the SQL command.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        int ExecuteSqlCommand(string sql, int? timeout = null, params object[] parameters);        
	}
}