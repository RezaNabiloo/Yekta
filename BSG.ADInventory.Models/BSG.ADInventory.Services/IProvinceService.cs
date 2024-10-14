namespace BSG.ADInventory.Services
{ 	
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.Province;
    using Zcf.Services;
			
    /// <summary>
    /// Create Interface for ProvinceService
    /// </summary>
	public interface IProvinceService : ICrudService<Province>
	{
        IEnumerable<Township> GetTownships(long id);
        IEnumerable<ProvinceListModel> GetDataList();
    }
}