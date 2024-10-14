namespace BSG.ADInventory.Services
{ 	
    using BSG.ADInventory.Entities;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;	
		  
    /// <summary>
    /// Create Service For baseentity" />
    /// </summary>
    public class BaseEntityService : CrudService<BaseEntity>, IBaseEntityService
    {
        private readonly BSG.ADInventory.Data.IBaseEntityRepository baseentityRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEntityService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="baseentityRepository"></param>
        public BaseEntityService(IUnitOfWork unitOfWork, BSG.ADInventory.Data.IBaseEntityRepository baseentityRepository)
            : base(unitOfWork, baseentityRepository)
        {
            this.baseentityRepository = baseentityRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<BaseEntity> GetItems(PagedQueryable criteria)
        {
            var data = this.baseentityRepository.Query;
            return data.ToPagedQueryable(criteria);
        }
    }
}
