namespace BSG.ADInventory.Services
{ 	
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.Inventory;
    using Zcf.Services;	
		
    /// <summary>
    /// Create Interface for InventoryService
    /// </summary>
	public interface IInventoryService : ICrudService<Inventory>
	{
        IEnumerable<InventoryListModel> GetDataList();
	}
}