namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.Mat;
    using BSG.ADInventory.Models.Report;
    using System.Collections.Generic;
    using Zcf.Services;

    /// <summary>
    /// Create Interface for MatService
    /// </summary>
    public interface IMatService : ICrudService<Mat>
    {
        
        double GetMatQty(long branchId, long matId);
        List<MatInventoryModel> RepInventoryList(InventoryListReportParam filterModel);

        bool IsDuplicateMatCode(string matCode);

        List<MatStockViewModel> RepMatStock(MatStockRepParam paramModel);
        List<MatStockViewModel> RepMatUse(MatStockRepParam paramModel);


        List<MatStockViewModel> RepCentralInventoryMatStock();
    }
}