namespace BSG.ADInventory.Services
{ 	
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.MatPurchase;
    using Zcf.Services;	
		
    /// <summary>
    /// Create Interface for MatPurchaseService
    /// </summary>
	public interface IMatPurchaseService : ICrudService<MatPurchase>
	{
        IEnumerable<MatPurchaseListModel> GetDataList(long id);
    }
}