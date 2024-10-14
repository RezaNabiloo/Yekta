namespace BSG.ADInventory.Services
{ 	
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.InvDocAttachment;
    using Zcf.Services;	
		
    /// <summary>
    /// Create Interface for InvDocAttachmentService
    /// </summary>
	public interface IInvDocAttachmentService : ICrudService<InvDocAttachment>
	{
        IEnumerable<InvDocAttachmentDataModel> GetInvDocAttachments(long id);
    }
}