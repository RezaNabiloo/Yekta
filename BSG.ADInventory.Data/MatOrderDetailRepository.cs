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
    /// Initializes a new instance of the <see cref="MatOrderDetailRepository" /> class.
    /// </summary>
    /// <param name="dbFactory">The database factory.</param>
	public class MatOrderDetailRepository : EntityRepositoryBase<MatOrderDetail>, IMatOrderDetailRepository
	{        
		public MatOrderDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <value>
        /// The query.
        /// </value>
        public override IQueryable<MatOrderDetail> Query
        {
            get
            {
                return base.Query.OrderBy(p => p.Id);
            }
        } 
	}
}