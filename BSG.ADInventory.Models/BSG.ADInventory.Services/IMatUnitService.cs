namespace BSG.ADInventory.Services
{ 	
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.Mat;
    using BSG.ADInventory.Models.Province;
    using Zcf.Services;	
		
    /// <summary>
    /// Create Interface for MatUnitService
    /// </summary>
	public interface IMatUnitService : ICrudService<MatUnit>
	{

        IEnumerable<MatUnitListModel> GetDataList();
    }
}