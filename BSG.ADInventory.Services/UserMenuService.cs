namespace BSG.ADInventory.Services
{ 	
    using BSG.ADInventory.Entities;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;
	
		  
    /// <summary>
    /// Create Service For UserMenu" />
    /// </summary>
    public class UserMenuService : CrudService<UserMenu>, IUserMenuService
    {
        private readonly BSG.ADInventory.Data.IUserMenuRepository userMenuRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserMenuService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="UserMenuRepository"></param>
        public UserMenuService(IUnitOfWork unitOfWork, BSG.ADInventory.Data.IUserMenuRepository userMenuRepository)
            : base(unitOfWork, userMenuRepository)
        {
            this.userMenuRepository = userMenuRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<UserMenu> GetItems(PagedQueryable criteria)
        {
            var data = this.userMenuRepository.Query;
            return data.ToPagedQueryable(criteria);
        }
    }
}
