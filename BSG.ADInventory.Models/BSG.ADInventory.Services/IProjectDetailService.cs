namespace BSG.ADInventory.Services
{ 	
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.ProjectDetail;
    using Zcf.Services;	
		
    /// <summary>
    /// Create Interface for ProjectDetailService
    /// </summary>
	public interface IProjectDetailService : ICrudService<ProjectDetail>
	{
        IEnumerable<ProjectDetailListModel> GetDataList(long id);
    }
}