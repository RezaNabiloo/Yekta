namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.InvYearCycle;
    using System.Collections.Generic;
    using Zcf.Services;

    /// <summary>
    /// Create Interface for InvYearCycleService
    /// </summary>
    public interface IInvYearCycleService : ICrudService<InvYearCycle>
	{
        IEnumerable<InvYearCycleListModel> GetDataList();
    }
}