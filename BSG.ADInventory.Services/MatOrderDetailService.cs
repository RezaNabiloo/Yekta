namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.MatOrderDetail;
    using DocumentFormat.OpenXml.Wordprocessing;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Security.AccessControl;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;


    /// <summary>
    /// Create Service For MatOrderDetail" />
    /// </summary>
    public class MatOrderDetailService : CrudService<MatOrderDetail>, IMatOrderDetailService
    {
        private readonly IMatOrderDetailRepository matOrderDetailRepository;
        private readonly IPurchaseDocDetailRepository purchaseDocDetailRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatOrderDetailService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="MatOrderDetailRepository"></param>
        public MatOrderDetailService(IUnitOfWork unitOfWork, IMatOrderDetailRepository matOrderDetailRepository,
            IPurchaseDocDetailRepository purchaseDocDetailRepository)
            : base(unitOfWork, matOrderDetailRepository)
        {
            this.matOrderDetailRepository = matOrderDetailRepository;
            this.purchaseDocDetailRepository = purchaseDocDetailRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<MatOrderDetail> GetItems(PagedQueryable criteria)
        {
            var data = this.matOrderDetailRepository.Query;
            return data.ToPagedQueryable(criteria);
        }

        public List<MatOrderDetailInfoListModel> GetOpenOrders()
        {
            // var data = (from od in this.matOrderDetailRepository.GetAllItems()
            //             where
            //                   ((from d in this.purchaseDocDetailRepository.GetAllItems()
            //                     where
            //d.MatOrderDetailId == od.Id
            //                     select new
            //                     {
            //                         Column1 = 1
            //                     }).Take(1)).Single() == null
            //             select new
            //             {
            //                 Id = od.Id,
            //                 MatOrderId = od.MatOrderId,
            //                 MatId = od.MatId,
            //                 RequiredAmount = od.RequiredAmount,
            //                 ConfirmedAmount = od.ConfirmedAmount,
            //                 Description = od.Description,
            //                 CreateTime = od.CreateTime,
            //                 CreateUserId = od.CreateUserId,
            //                 LastUpdateTime = od.LastUpdateTime,
            //                 LastUpdateUserId = od.LastUpdateUserId,
            //                 ConfirmDescription = od.ConfirmDescription
            //             }).ToList();
            var data = (this.matOrderDetailRepository.Query.Where(m => m.ConfirmedAmount>0 && (this.purchaseDocDetailRepository.Query
                                                                        .Where(d => d.MatOrderDetailId == m.Id).Select(o => new { Column1 = 1 }).Take(1)).FirstOrDefault() == null)
                        .Select(m => new MatOrderDetailInfoListModel
                        {
                            Id = m.Id,
                            MatOrderId=m.MatOrderId,                            
                            ProjectId = m.MatOrder.ProjectId,
                            ProjectTitle = m.MatOrder.Project.Title,
                            RequiredDate = m.MatOrder.RequiredDate,
                            MatId=m.MatId,
                            Amount = m.ConfirmedAmount,
                            MatTitle = m.Mat.Title,
                            MatUnit=m.Mat.MatUnit.Title,
                            Model = m.Mat.Model,
                            Dimention = m.Mat.Dimention,
                            TechnicalSpec = m.Mat.TechnicalSpec,
                            Project=m.MatOrder.Project.Title
                        })).ToList();




            return data;
        }

    }
}
