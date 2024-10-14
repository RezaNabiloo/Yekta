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
    /// Create Interface for CountryRepository
    /// </summary>
	public interface IInvDocRepository : IQueryableRepository<InvDoc>
	{        
	}
}