namespace BSG.ADInventory.Services
{ 	
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.CarType;
    using Zcf.Services;	
		
    /// <summary>
    /// Create Interface for CarTypeService
    /// </summary>
	public interface ICarTypeService : ICrudService<CarType>
	{
	}
}