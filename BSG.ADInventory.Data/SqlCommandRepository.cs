namespace BSG.ADInventory.Data
{
    using System.Collections.Generic;
	using System.Data;
	using System.Data.Entity;
	using System.Linq;
    using Zcf.Data;
    using BSG.ADInventory.Entities;
    using System.Data.Entity.Infrastructure;
    using System.Data.Common;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Initializes a new instance of the <see cref="SqlCommandRepository" /> class.
    /// </summary>
    /// <param name="dbFactory">The database factory.</param>
    public class SqlCommandRepository : EntityRepositoryBase<SqlCommand>, ISqlCommandRepository
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCommandRepository"/> class.
        /// </summary>
        /// <param name="dbFactory">The database factory.</param>
		public SqlCommandRepository(Zcf.Data.IDbFactory dbFactory) : base(dbFactory)
        {
        }

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <value>
        /// The query.
        /// </value>
        public override IQueryable<SqlCommand> Query
        {
            get
            {
                return base.Query.OrderBy(p => p.Id);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [automatic detect changes enabled].
        /// </summary>
        /// <value>
        /// <c>true</c> if [automatic detect changes enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool AutoDetectChangesEnabled
        {
            get
            {
                return this.DataContext.Configuration.AutoDetectChangesEnabled;
            }
            set
            {
                this.DataContext.Configuration.AutoDetectChangesEnabled = value;
            }
        }

        /// <summary>
        /// Executes the stored procedure list.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public virtual IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters)
            where TEntity : class , new()
        {
            //HACK: Entity Framework Code First doesn't support output parameters
            //That's why we have to manually create command and execute it.
            //just wait until EF Code First starts support them
            //
            //More info: http://weblogs.asp.net/dwahlin/archive/2011/09/23/using-entity-framework-code-first-with-stored-procedures-that-have-output-parameters.aspx

            bool hasOutputParameters = false;
            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    var outputP = p as DbParameter;
                    if (outputP == null)
                        continue;

                    if (outputP.Direction == ParameterDirection.InputOutput ||
                        outputP.Direction == ParameterDirection.Output)
                        hasOutputParameters = true;
                }
            }

            var detectChanges = this.AutoDetectChangesEnabled;
            this.AutoDetectChangesEnabled = false;

            IList<TEntity> result = null;
            try
            {
                var context = ((IObjectContextAdapter)(this.DataContext)).ObjectContext;

                if (!hasOutputParameters)
                {
                    
                    //no output parameters
                    result = this.DataContext.Database.SqlQuery<TEntity>(commandText, parameters).ToList();

                    //for (int i = 0; i < result.Count; i++)
                    //    result[i] = AttachEntityToContext(result[i]);

                    //return result;

                    //var result = context.ExecuteStoreQuery<TEntity>(commandText, parameters).ToList();
                    //foreach (var entity in result)
                    //    Set<TEntity>().Attach(entity);
                    //return result;
                }
                else
                {
                    //var connection = context.Connection;
                    
                    var connection = this.DataContext.Database.Connection;
                    //Don't close the connection after command execution


                    //open the connection for use
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    //create a command object
                    using (var cmd = connection.CreateCommand())
                    {
                        //command to execute
                        cmd.CommandText = commandText;
                        cmd.CommandType = CommandType.StoredProcedure;

                        // move parameters to command object
                        if (parameters != null)
                            foreach (var p in parameters)
                                cmd.Parameters.Add(p);

                        //database call
                        var reader = cmd.ExecuteReader();
                        //return reader.DataReaderToObjectList<TEntity>();
                        result = context.Translate<TEntity>(reader).ToList();

                        //for (int i = 0; i < result.Count; i++)
                        //    result[i] = AttachEntityToContext(result[i]);
                        //close up the reader, we're done saving results
                        reader.Close();
                        //return result;
                    }

                }
            }
            finally
            {
                this.AutoDetectChangesEnabled = detectChanges;
            }

            return result;
        }

        /// <summary>
        /// Sets this instance.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return this.DataContext.Set<TEntity>();
        }

        /// <summary>
        /// Executes the SQL command.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public int ExecuteSqlCommand(string sql, int? timeout = null, params object[] parameters)
        {
            // Guard.ArgumentNotEmpty(sql, "sql");

            int? previousTimeout = null;
            if (timeout.HasValue)
            {
                //store previous timeout
                previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
            }

            // remove the GO statements
            // sql = Regex.Replace(sql, @"\r{0,1}\n[Gg][Oo]\r{0,1}\n", "\n");
            try
            {
                var reza = this.DataContext.Database.ExecuteSqlCommand(sql, parameters);
            }
            catch (System.Exception ex)
            {
                var ali = ex;

                //throw;
            }
            

            if (timeout.HasValue)
            {
                //Set previous timeout back
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = previousTimeout;
            }

            return 1;
        }

        /// <summary>
        /// Attach an entity to the context or return an already attached entity (if it was already attached)
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns>Attached entity</returns>
        protected virtual TEntity AttachEntityToContext<TEntity>(TEntity entity) where TEntity : BaseEntity, new()
        {
            //little hack here until Entity Framework really supports stored procedures
            //otherwise, navigation properties of loaded entities are not loaded until an entity is attached to the context
            var alreadyAttached = Set<TEntity>().Local.Where(x => x.Id == entity.Id).FirstOrDefault();
            if (alreadyAttached == null)
            {
                //attach new entity
                Set<TEntity>().Attach(entity);
                return entity;
            }
            else
            {
                //entity is already loaded.
                return alreadyAttached;
            }
        }
	}
}