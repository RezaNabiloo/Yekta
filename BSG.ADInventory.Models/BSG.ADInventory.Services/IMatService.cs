namespace BSG.ADInventory.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.Mat;
    using BSG.ADInventory.Models.Report;
    using Zcf.Services;

    /// <summary>
    /// Create Interface for MatService
    /// </summary>
    public interface IMatService : ICrudService<Mat>
    {
        IEnumerable<MatListModel> GetDataList();
        double GetMatQty(long branchId, long matId);
        List<MatInventoryModel> RepInventoryList(InventoryListReportParam filterModel);

        bool IsDuplicateMatCode(string matCode);

        List<MatStockViewModel> RepMatStock(MatStockRepParam paramModel);
    }
}