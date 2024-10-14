namespace BSG.ADInventory.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Entities;
    using Zcf.Services;
    using Models.Brand;

    /// <summary>
    /// Create Interface for CountryService
    /// </summary>
    public interface IBrandService : ICrudService<Brand>
    {

        List<BrandDataModel> GetBrandAll();
        BrandDataModel AddBrand(BrandDataModel dataModel);

        BrandDataModel UpdateBrand(BrandDataModel dataModel);

        long RemoveBrand(long id);

        IEnumerable<BrandListModel> GetDataList();
    }
}