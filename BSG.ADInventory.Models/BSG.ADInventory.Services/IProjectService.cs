namespace BSG.ADInventory.Services
{ 	
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.Project;
    using Zcf.Services;	
		
    /// <summary>
    /// Create Interface for ProjectService
    /// </summary>
	public interface IProjectService : ICrudService<Project>
	{
        IEnumerable<ProjectListModel> GetDataList();        
	}
}