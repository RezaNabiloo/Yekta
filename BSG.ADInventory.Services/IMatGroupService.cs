namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.Brand;
    using BSG.ADInventory.Models.MatGroup;
    using System.Collections.Generic;
    using Zcf.Services;

    /// <summary>
    /// Create Interface for ProvinceService
    /// </summary>
    public interface IMatGroupService : ICrudService<MatGroup>
	{
        
        List<MatGroupVM> GetChildMatGroups(long id);
    }
}