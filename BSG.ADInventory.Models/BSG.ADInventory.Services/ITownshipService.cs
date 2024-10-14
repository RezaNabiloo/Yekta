namespace BSG.ADInventory.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Entities;
    using Zcf.Services;
    using Models.Township;

    /// <summary>
    /// Create Interface for TownshipService
    /// </summary>
    public interface ITownshipService : ICrudService<Township>
	{
        long? GetProvinceId(long? id);
        Province GetProvince(long? id);

        IEnumerable<TownshipListModel> GetDataList();
    }
}