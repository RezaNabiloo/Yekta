namespace BSG.ADInventory.Services
{ 	
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.InvDoc;
    using BSG.ADInventory.Models.MatOrder;
    using Zcf.Models;
    using Zcf.Services;	
		
    /// <summary>
    /// Create Interface for MatOrderService
    /// </summary>
	public interface IMatOrderService : ICrudService<MatOrder>
	{
        IEnumerable<MatOrder> GetDataList(int type);

        CustomResult<bool> SetRequestDocData(MatOrderCEDTO dataModel);

        void ConfirmOrder(MatOrderConfirmModel dataModel);
    }
}