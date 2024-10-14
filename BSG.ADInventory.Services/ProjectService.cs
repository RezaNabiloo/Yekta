namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;


    /// <summary>
    /// Create Service For Project" />
    /// </summary>
    public class ProjectService : CrudService<Project>, IProjectService
    {
        private readonly IProjectRepository projectRepository;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="ProjectRepository"></param>
        public ProjectService(IUnitOfWork unitOfWork, IProjectRepository projectRepository)
            : base(unitOfWork, projectRepository)
        {
            this.projectRepository = projectRepository;            
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<Project> GetItems(PagedQueryable criteria)
        {
            var data = this.projectRepository.Query;
            return data.ToPagedQueryable(criteria);
        }        
    }
}
