namespace BSG.ADInventory.Services
{ 	
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.Country;
    using Zcf.Services;	
		
    /// <summary>
    /// Create Interface for CountryService
    /// </summary>
	public interface ICountryService : ICrudService<Country>
	{
        IEnumerable<CountryListModel> GetDataList();
    }
}