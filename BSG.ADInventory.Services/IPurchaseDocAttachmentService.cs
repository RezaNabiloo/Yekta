namespace BSG.ADInventory.Services
{ 	
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.PurchaseDocAttachment;
    using Zcf.Services;	
		
    /// <summary>
    /// Create Interface for PurchaseDocAttachmentService
    /// </summary>
	public interface IPurchaseDocAttachmentService : ICrudService<PurchaseDocAttachment>
	{
        IEnumerable<PurchaseDocAttachmentDataModel> GetPurchaseDocAttachments(long id);
    }
}