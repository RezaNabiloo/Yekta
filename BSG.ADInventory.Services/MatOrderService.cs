namespace BSG.ADInventory.Services
{
    using AutoMapper;
    using BSG.ADInventory.Common.Enum;
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.InvDoc;
    using BSG.ADInventory.Models.MatOrder;
    using BSG.ADInventory.Models.MatOrderDetail;
    using BSG.ADInventory.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading;
    using Zcf.Common;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Models;
    using Zcf.Paging;
    using Zcf.Services;


    /// <summary>
    /// Create Service For MatOrder" />
    /// </summary>
    public class MatOrderService : CrudService<MatOrder>, IMatOrderService
    {
        private readonly IMatOrderRepository matOrderRepository;
        private readonly IUserRepository userRepository;
        private readonly IMatOrderDetailRepository matOrderDetailRepository;
        private readonly IPurchaseDocDetailRepository purchaseDocDetailRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatOrderService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="MatOrderRepository"></param>
        public MatOrderService(IUnitOfWork unitOfWork, IMatOrderRepository matOrderRepository,
            IUserRepository userRepository,
            IMatOrderDetailRepository matOrderDetailRepository, IPurchaseDocDetailRepository purchaseDocDetailRepository,
            IMapper mapper)
            : base(unitOfWork, matOrderRepository)
        {
            this.matOrderRepository = matOrderRepository;
            this.userRepository = userRepository;
            this.matOrderDetailRepository = matOrderDetailRepository;
            this.purchaseDocDetailRepository = purchaseDocDetailRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<MatOrder> GetItems(PagedQueryable criteria)
        {
            var data = this.matOrderRepository.Query;
            return data.ToPagedQueryable(criteria);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"> type 1 => sendbox, type 2 => confirmbox, type 3 > inbox, type 4 => All(Archive)</param>
        /// <returns></returns>
        public IEnumerable<MatOrder> GetDataList(int type)
        {
            var user = this.userRepository.GetCurrentUser();
            var data = this.matOrderRepository.Query.AsEnumerable().Where(t => (type == 1 && t.CreateUserId == user.Id && t.Archived == false)
                                                               || (type == 2 && t.ConfirmTime == null)
                                                               || (type == 3 && t.ConfirmStatus == ConfirmStatus.Confirmed && !OrderComplete(t.Id))
                                                               || (type == 4)).OrderByDescending(o => o.Id).ToList();

            return (data);
        }

        public CustomResult<bool> SetRequestDocData(MatOrderCEDTO dataModel)
        {
            CustomResult<bool> data = new CustomResult<bool>();


            // Edit
            if (dataModel.Id > 0)
            {

                var matOrder = this.matOrderRepository.GetItemByKey(dataModel.Id);
                if (matOrder.ConfirmStatus != Common.Enum.ConfirmStatus.Pending)
                {
                    data.Result = false;
                    data.ErrorMessage = Resource.RequestIsOutOfYourAccess;
                    return data;
                }

                mapper.Map(dataModel, matOrder);
                // Synchronize MatOrderDetail items
                // Remove deleted details
                var detailsToRemove = matOrder.MatOrderDetails
                                                   .Where(ed => !dataModel.MatOrderDetails.Any(d => d.Id == ed.Id))
                                                   .ToList();
                foreach (var detail in detailsToRemove)
                {
                    matOrderDetailRepository.Remove(detail);
                }

                // Update existing and add new details
                foreach (var dtoDetail in dataModel.MatOrderDetails)
                {
                    var existingDetail = matOrder.MatOrderDetails
                                                      .FirstOrDefault(ed => ed.Id == dtoDetail.Id);

                    if (existingDetail != null)
                    {
                        // Update existing detail
                        mapper.Map(dtoDetail, existingDetail);
                    }
                    else
                    {
                        // Add new detail
                        var newDetail = mapper.Map<MatOrderDetail>(dtoDetail);
                        matOrder.MatOrderDetails.Add(newDetail);
                    }
                }
                this.matOrderRepository.Update(matOrder);

            }

            // Create
            else
            {
                MatOrder matOrder = mapper.Map<MatOrder>(dataModel);
                this.matOrderRepository.Add(matOrder);
            }

            try
            {                
                this.UnitOfWork.Commit();
                data.Result = true;
                data.ErrorMessage = Resource.DataSavedSuccessfully;

            }
            catch (System.Exception ex)
            {
                var err = ex;
                data.Result = false;
                data.ErrorMessage = ex.Message;
            }

            return data;

        }

        //public CustomResult<bool> SetRequestDocData(MatOrderCEDTO dataModel)
        //{
        //    CustomResult<bool> data = new CustomResult<bool>();





        //    MatOrder matOrder = new MatOrder();

        //    // Edit
        //    if (dataModel.Id > 0)
        //    {

        //        var oldReq = this.matOrderRepository.GetItemByKey(dataModel.Id);
        //        if (oldReq.ConfirmStatus != Common.Enum.ConfirmStatus.Pending)
        //        {
        //            data.Result = false;
        //            data.ErrorMessage = Resource.RequestIsOutOfYourAccess;
        //            return data;
        //        }

        //        foreach (var item in oldReq.MatOrderDetails.ToList())
        //        {
        //            this.matOrderDetailRepository.Remove(item);
        //        }

        //        matOrder = oldReq;

        //        matOrder.ProjectId = dataModel.ProjectId;
        //        matOrder.RequiredDate = dataModel.RequiredDate;
        //        matOrder.Priority = dataModel.Priority;
        //        matOrder.RequestDescription = dataModel.RequestDescription;
        //        //foreach (var item in dataModel.Mats)
        //        //{
        //        //    MatOrderDetail det = new MatOrderDetail
        //        //    {
        //        //        MatOrder = matOrder,
        //        //        MatId = item.MatId,
        //        //        RequiredAmount = item.RequiredAmount
        //        //    };
        //        //    matOrder.MatOrderDetails.Add(det);
        //        //}
        //    }

        //    // Create
        //    else
        //    {

        //        matOrder = new MatOrder
        //        {
        //            ProjectId = dataModel.ProjectId,
        //            RequiredDate = dataModel.RequiredDate,
        //            Priority = dataModel.Priority,
        //            RequestDescription = dataModel.RequestDescription,
        //            ConfirmStatus = ConfirmStatus.Pending
        //        };
        //        //foreach (var item in dataModel.Mats)
        //        //{
        //        //    MatOrderDetail det = new MatOrderDetail
        //        //    {
        //        //        MatOrder = matOrder,
        //        //        MatId = item.MatId,
        //        //        RequiredAmount = item.RequiredAmount
        //        //    };
        //        //    matOrder.MatOrderDetails.Add(det);
        //        //}

        //    }



        //    try
        //    {
        //        if (dataModel.Id > 0)
        //        {
        //            this.matOrderRepository.Update(matOrder);
        //        }
        //        else
        //        {
        //            this.matOrderRepository.Add(matOrder);
        //        }
        //        this.UnitOfWork.Commit();
        //        data.Result = true;
        //        data.ErrorMessage = Resource.DataSavedSuccessfully;

        //    }
        //    catch (System.Exception ex)
        //    {
        //        var err = ex;
        //        //throw;

        //        data.Result = false;
        //        data.ErrorMessage = ex.Message;
        //    }

        //    return data;

        //}

        public void ConfirmOrder(MatOrderConfirmModel dataModel)
        {

            var order = this.matOrderRepository.GetItemByKey(dataModel.Id);
            order.ConfirmStatus = ConfirmStatus.Confirmed;
            order.ConfirmTime = DateTime.Now;
            order.ConfirmUserId = this.userRepository.GetCurrentUser().Id;
            order.ConfirmDescription = dataModel.ConfirmDescription;
            this.matOrderRepository.Update(order);

            foreach (var item in dataModel.Mats)
            {
                var det = this.matOrderDetailRepository.GetItemByKey(item.Id);
                det.ConfirmedAmount = item.ConfirmedAmount;
                det.ConfirmDescription = item.ConfirmDescription;
                this.matOrderDetailRepository.Update(det);
            }

            this.UnitOfWork.Commit();



        }

        private bool OrderComplete(long id)
        {
            var data = !(this.matOrderDetailRepository.Query.Where(m => m.ConfirmedAmount > 0 && m.MatOrderId == id && (this.purchaseDocDetailRepository.Query
                                                                        .Where(d => d.MatOrderDetailId == m.Id).Select(o => new { Column1 = 1 }).Take(1)).FirstOrDefault() == null)
                        .Any());
            return data;
        }
    }
}
