
namespace BSG.ADInventory.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.Provider;
    using Zcf.Services;

    /// <summary>
    /// Create Interface for CountryService
    /// </summary>
    public interface IProviderService : ICrudService<Provider>
    {
        IEnumerable<ProviderListModel> GetDataList();
        ProviderListModel GetProviderById(long id);
        long AddProvider(ProviderDataModel dataModel);
        long UpdateProvider(ProviderDataModel dataModel);
        long RemoveProvider(long id);
    }
}
