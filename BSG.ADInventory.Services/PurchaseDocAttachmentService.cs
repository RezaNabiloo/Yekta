namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.PurchaseDocAttachment;
    using System.Collections.Generic;
    using System.Linq;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;
	
		  
    /// <summary>
    /// Create Service For PurchaseDocAttachment" />
    /// </summary>
    public class PurchaseDocAttachmentService : CrudService<PurchaseDocAttachment>, IPurchaseDocAttachmentService
    {
        private readonly IPurchaseDocAttachmentRepository purchaseDocAttachmentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseDocAttachmentService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="PurchaseDocAttachmentRepository"></param>
        public PurchaseDocAttachmentService(IUnitOfWork unitOfWork, IPurchaseDocAttachmentRepository purchaseDocAttachmentRepository)
            : base(unitOfWork, purchaseDocAttachmentRepository)
        {
            this.purchaseDocAttachmentRepository = purchaseDocAttachmentRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<PurchaseDocAttachment> GetItems(PagedQueryable criteria)
        {
            var data = this.purchaseDocAttachmentRepository.Query;
            return data.ToPagedQueryable(criteria);
        }

        public IEnumerable<PurchaseDocAttachmentDataModel> GetPurchaseDocAttachments(long id)
        {
            var data = this.purchaseDocAttachmentRepository.Query.Where(i=>i.PurchaseDocId==id).Select(t => new PurchaseDocAttachmentDataModel
            {
                Id = t.Id,
                PurchaseDocId=t.PurchaseDocId,
                Title = t.Title,
                FilePath = t.FilePath
            }).ToList();

            return (data);
        }
    }
}
