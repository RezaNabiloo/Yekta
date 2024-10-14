namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;
	
		  
    /// <summary>
    /// Create Service For InvDocDetail" />
    /// </summary>
    public class InvDocDetailService : CrudService<InvDocDetail>, IInvDocDetailService
    {
        private readonly IInvDocDetailRepository invDocDetailRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvDocDetailService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="InvDocDetailRepository"></param>
        public InvDocDetailService(IUnitOfWork unitOfWork, IInvDocDetailRepository invDocDetailRepository)
            : base(unitOfWork, invDocDetailRepository)
        {
            this.invDocDetailRepository = invDocDetailRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<InvDocDetail> GetItems(PagedQueryable criteria)
        {
            var data = this.invDocDetailRepository.Query;
            return data.ToPagedQueryable(criteria);
        }
    }
}
