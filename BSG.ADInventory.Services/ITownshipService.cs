namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Entities;
    using Zcf.Services;

    /// <summary>
    /// Create Interface for TownshipService
    /// </summary>
    public interface ITownshipService : ICrudService<Township>
	{
        long? GetProvinceId(long? id);
        Province GetProvince(long? id);

    }
}