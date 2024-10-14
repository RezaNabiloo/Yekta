namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.InvYearCycle;
    using System.Collections.Generic;
    using System.Linq;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;


    /// <summary>
    /// Create Service For InvYearCycle" />
    /// </summary>
    public class InvYearCycleService : CrudService<InvYearCycle>, IInvYearCycleService
    {
        private readonly IInvYearCycleRepository invYearCycleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvYearCycleService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="InvYearCycleRepository"></param>
        public InvYearCycleService(IUnitOfWork unitOfWork, IInvYearCycleRepository invYearCycleRepository)
            : base(unitOfWork, invYearCycleRepository)
        {
            this.invYearCycleRepository = invYearCycleRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<InvYearCycle> GetItems(PagedQueryable criteria)
        {
            var data = this.invYearCycleRepository.Query;
            return data.ToPagedQueryable(criteria);
        }

        public IEnumerable<InvYearCycleListModel> GetDataList()
        {
            var data = this.invYearCycleRepository.GetAllItems().Select(t => new InvYearCycleListModel
            {
                Id = t.Id,
                Year = t.CalendarYear.YearNo.ToString(),
                MatId= t.MatId.ToString(),
                MatTitle=t.Mat.Title,
                Project=t.Project.Title,
                ConfirmedCountQty=t.ConfirmedCountQty.ToString()+" "+t.Mat.MatUnit?.Title
            }).ToList();

            return (data);
        }
    }
}
