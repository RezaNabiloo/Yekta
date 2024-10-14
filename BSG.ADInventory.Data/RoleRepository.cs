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
	using Zcf.Data.Framework;

    /// <summary>
    /// Initializes a new instance of the <see cref="RoleRepository" /> class.
    /// </summary>
    /// <param name="dbFactory">The database factory.</param>
    public class RoleRepository : DbContextRoleRepository<Entities.Role>, IRoleRepository
	{        
		public RoleRepository(Zcf.Data.IDbFactory dbFactory) : base(dbFactory)
        {
        }

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <value>
        /// The query.
        /// </value>
        public override IQueryable<Role> Query
        {
            get
            {
                return base.Query.OrderBy(p => p.Id);
            }
        } 
	}
}