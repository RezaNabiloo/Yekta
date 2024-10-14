namespace BSG.ADInventory.Services
{ 	
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.InvDoc;
    using BSG.ADInventory.Models.MatOrder;
    using BSG.ADInventory.Models.MatOrderDetail;
    using Zcf.Models;
    using Zcf.Services;

    /// <summary>
    /// Create Interface for MatOrderDetailService
    /// </summary>
    public interface IMatOrderDetailService : ICrudService<MatOrderDetail>
	{

        List<MatOrderDetailInfoListModel> GetOpenOrders();
    }
}