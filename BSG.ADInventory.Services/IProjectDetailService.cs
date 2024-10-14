namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Entities;
    using System.Collections.Generic;
    using Zcf.Services;

    /// <summary>
    /// Create Interface for ProjectDetailService
    /// </summary>
    public interface IProjectDetailService : ICrudService<ProjectDetail>
	{
        IEnumerable<ProjectDetail> GetProjectDetails(long id);
    }
}