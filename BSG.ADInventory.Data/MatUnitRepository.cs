namespace BSG.ADInventory.Data
{ 	
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.Entity;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Web;
	using Zcf.Data;
	using BSG.ADInventory.Entities;
		
    /// <summary>
    /// Initializes a new instance of the <see cref="MatUnitRepository" /> class.
    /// </summary>
    /// <param name="dbFactory">The database factory.</param>
	public class MatUnitRepository : EntityRepositoryBase<MatUnit>, IMatUnitRepository
	{        
		public MatUnitRepository(Zcf.Data.IDbFactory dbFactory) : base(dbFactory)
        {
        }

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <value>
        /// The query.
        /// </value>
        public override IQueryable<MatUnit> Query
        {
            get
            {
                return base.Query.OrderBy(p => p.Id);
            }
        } 
	}
}