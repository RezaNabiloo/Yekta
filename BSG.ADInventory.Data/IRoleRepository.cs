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
	using Zcf.Security;

    /// <summary>
    /// Create Interface for RoleRepository
    /// </summary>
    public interface IRoleRepository : IRoleRepository<Entities.Role>, IQueryableRepository<Entities.Role>
	{        

	}
}