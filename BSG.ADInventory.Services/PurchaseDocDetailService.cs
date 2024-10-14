namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    //using BSG.ADInventory.Models.PurchaseDocDetail;
    using System.Collections.Generic;
    using System.Linq;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;
	
		  
    /// <summary>
    /// Create Service For PurchaseDocDetail" />
    /// </summary>
    public class PurchaseDocDetailService : CrudService<PurchaseDocDetail>, IPurchaseDocDetailService
    {
        private readonly IPurchaseDocDetailRepository purchaseDocDetailRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseDocDetailService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="PurchaseDocDetailRepository"></param>
        public PurchaseDocDetailService(IUnitOfWork unitOfWork, IPurchaseDocDetailRepository purchaseDocDetailRepository)
            : base(unitOfWork, purchaseDocDetailRepository)
        {
            this.purchaseDocDetailRepository = purchaseDocDetailRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<PurchaseDocDetail> GetItems(PagedQueryable criteria)
        {
            var data = this.purchaseDocDetailRepository.Query;
            return data.ToPagedQueryable(criteria);
        }

        //public IEnumerable<PurchaseDocDetailListModel> GetDataList()
        //{
        //    var data = this.purchaseDocDetailRepository.GetAllItems().Select(t => new PurchaseDocDetailListModel
        //    {
        //        Id = t.Id,
        //        Title = t.Title,
        //        TitleEn = t.TitleEn
        //    }).ToList();

        //    return (data);
        //}
    }
}
