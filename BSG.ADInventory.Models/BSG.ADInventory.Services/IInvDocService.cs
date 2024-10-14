namespace BSG.ADInventory.Services
{ 	
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.InvDoc;
    using Zcf.Models;
    using Zcf.Services;	
		
    /// <summary>
    /// Create Interface for InvDocService
    /// </summary>
	public interface IInvDocService : ICrudService<InvDoc>
	{
        IEnumerable<InvDocListModel> GetDataList(long docType);

        CustomResult<bool> SetEntranceDocData(EntranceDocDataModel dataModel);

        CustomResult<bool> SetInternalEntranceDocData(InternalEntranceDocDataModel dataModel);

        CustomResult<bool> SetUseDocData(UseDocDataModel dataModel);

        CustomResult<bool> SetReturnDocData(ReturnDocDataModel dataModel);
        CustomResult<bool> SetExternalBOLDocData(ExternalBOLDocDataModel dataModel);

        CustomResult<bool> SetInternalBOLDocData(InternalBOLDocDataModel dataModel);

        InvDoc GetInvDocByDocNo(string docNo);

    }
}