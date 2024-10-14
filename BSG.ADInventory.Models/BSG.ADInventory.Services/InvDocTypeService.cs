namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;
	
		  
    /// <summary>
    /// Create Service For InvDocType" />
    /// </summary>
    public class InvDocTypeService : CrudService<InvDocType>, IInvDocTypeService
    {
        private readonly IInvDocTypeRepository invDocTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvDocTypeService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="InvDocTypeRepository"></param>
        public InvDocTypeService(IUnitOfWork unitOfWork, IInvDocTypeRepository invDocTypeRepository)
            : base(unitOfWork, invDocTypeRepository)
        {
            this.invDocTypeRepository = invDocTypeRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<InvDocType> GetItems(PagedQueryable criteria)
        {
            var data = this.invDocTypeRepository.Query;
            return data.ToPagedQueryable(criteria);
        }
    }
}
