namespace BSG.ADInventory.Services
{ 	
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.Car;
    using Zcf.Services;	
		
    /// <summary>
    /// Create Interface for OrgCarService
    /// </summary>
	public interface ICarService : ICrudService<Car>
	{        
    }
}