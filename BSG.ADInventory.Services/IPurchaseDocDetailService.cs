namespace BSG.ADInventory.Services
{ 	
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Entities;
    //using BSG.ADInventory.Models.PurchaseDocDetail;
    using Zcf.Services;	
		
    /// <summary>
    /// Create Interface for PurchaseDocDetailService
    /// </summary>
	public interface IPurchaseDocDetailService : ICrudService<PurchaseDocDetail>
	{
        //IEnumerable<PurchaseDocDetailListModel> GetDataList();
    }
}