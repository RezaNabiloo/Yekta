namespace BSG.ADInventory.Services
{ 	
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.People;
    using BSG.Models.People;
    
    using Zcf.Services;
    

    /// <summary>
    /// Create Interface for CountryService
    /// </summary>
    public interface IPeopleService : ICrudService<People>
	{

     
        
        long RemovePeople(long id,Guid? userId);

        IEnumerable<PeopleListModel> GetDataList();


        
    }
}