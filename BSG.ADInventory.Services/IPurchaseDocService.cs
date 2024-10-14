namespace BSG.ADInventory.Services
{ 	    
    using System.Collections.Generic;   
    using BSG.ADInventory.Entities;    
    using BSG.ADInventory.Models.PurchaseDoc;
    using BSG.ADInventory.Models.PurchaseDocDetail;
    using Zcf.Models;    
    using Zcf.Services;
    using BSG.ADInventory.Models.Report;

    /// <summary>
    /// Create Interface for PurchaseDocService
    /// </summary>
	public interface IPurchaseDocService : ICrudService<PurchaseDoc>
	{
        IEnumerable<PurchaseDocListModel> GetDataList(int type);

        CustomResult<bool> OrderToPurchase(PurchaseDocDataModel dataModel);
        CustomResult<bool> SaveDoc(PurchaseDocDataModel dataModel);

        CustomResult<bool> CheckOrderMat(long matId, long matOrderId);

        List<PurchaseDocFollowModel> GetPurchaseDocFollowItems(long purchaseDocId, int type);

        List<PurchaseDocFollowListModel> RepPurchaseDocFollow(FollowPurchaseDocRepParam paramModel);

        List<PurchaseDocDetailListModel> RepPurchaseDocDetail(FollowPurchaseDocRepParam paramModel);
        List<PurchaseDocInvTransactionListModel> RepPurchaseDocInvTransaction(FollowPurchaseDocRepParam paramModel);

        List<PurchaseDocOnWayListModel> GetPurchaseDocOnWay();

        //List<PurchaseDocDetailListModel> RepPurchaseDocEntrance(FollowPurchaseDocRepParam paramModel);

        //List<PurchaseDocDetailListModel> RepPurchaseDocDistrib(FollowPurchaseDocRepParam paramModel);

        //List<PurchaseDocDetailListModel> RepPurchaseDocExit(FollowPurchaseDocRepParam paramModel);
    }
}