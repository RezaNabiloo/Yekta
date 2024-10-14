namespace BSG.ADInventory.Services
{ 	
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Entities;
    using Zcf.Services;	
		
    /// <summary>
    /// Create Interface for InvDocDetailService
    /// </summary>
	public interface IInvDocDetailService : ICrudService<InvDocDetail>
	{

        long GetMatInvQty(long id, long projectId);

    }
}