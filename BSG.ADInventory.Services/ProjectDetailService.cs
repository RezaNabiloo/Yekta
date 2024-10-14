namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using System.Collections.Generic;
    using System.Linq;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;


    /// <summary>
    /// Create Service For ProjectDetail" />
    /// </summary>
    public class ProjectDetailService : CrudService<ProjectDetail>, IProjectDetailService
    {
        private readonly IProjectDetailRepository projectDetailRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectDetailService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="ProjectDetailRepository"></param>
        public ProjectDetailService(IUnitOfWork unitOfWork, IProjectDetailRepository projectDetailRepository)
            : base(unitOfWork, projectDetailRepository)
        {
            this.projectDetailRepository = projectDetailRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<ProjectDetail> GetItems(PagedQueryable criteria)
        {
            var data = this.projectDetailRepository.Query;
            return data.ToPagedQueryable(criteria);
        }

        public IEnumerable<ProjectDetail> GetProjectDetails(long id)
        {
            var data = this.projectDetailRepository.Query.Where(d => d.ProjectId == id).OrderByDescending(d=>d.Id).ToList();            
            return data;
        }
    }
}
