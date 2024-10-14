namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;    
    using System.Collections.Generic;
    using BSG.ADInventory.Models.MatPurchase;
    using System.Linq;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;


    /// <summary>
    /// Create Service For MatPurchase" />
    /// </summary>
    public class MatPurchaseService : CrudService<MatPurchase>, IMatPurchaseService
    {
        private readonly IMatPurchaseRepository matPurchaseRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatPurchaseService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="MatPurchaseRepository"></param>
        public MatPurchaseService(IUnitOfWork unitOfWork, IMatPurchaseRepository matPurchaseRepository)
            : base(unitOfWork, matPurchaseRepository)
        {
            this.matPurchaseRepository = matPurchaseRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<MatPurchase> GetItems(PagedQueryable criteria)
        {
            var data = this.matPurchaseRepository.Query;
            return data.ToPagedQueryable(criteria);
        }

        //public IEnumerable<MatPurchaseListModel> GetDataList()
        //{
        //    var data = this.matPurchaseRepository.GetAllItems().Select(t => new MatPurchaseListModel
        //    {
        //        Id = t.Id,
        //        Title = t.Title,
        //        TitleEn = t.TitleEn
        //    }).ToList();

        //    return (data);
        //}

        public IEnumerable<MatPurchaseListModel> GetDataList(long id)
        {
            var data = this.matPurchaseRepository.Query.Where(p => p.InvDocId == id)
                .Select(p => new MatPurchaseListModel
                {
                    Id = p.Id,
                    VDocId=p.VDocId,
                    InvDocId = p.InvDocId,
                    DocNo = p.InvDoc.DocNo,
                    MatId = p.MatId,
                    MatCode = p.MatId==null?string.Empty:p.Mat.Code,
                    MatTitle = p.MatId == null ? string.Empty : p.Mat.Title+" "+p.Mat.TechnicalSpec,
                    Qty = p.Qty,
                    MatUnit=p.Mat.MatUnit.Title,
                    IsFreight = p.IsFreight,
                    PurchasePrice = p.PurchasePrice,
                    FreightPrice = p.FreightPrice,
                }).ToList();

            return data;
        }
    }
}
