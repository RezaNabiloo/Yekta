namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.Mat;
    using BSG.ADInventory.Models.MatOrderDetail;
    using BSG.ADInventory.Models.PurchaseDoc;
    using BSG.ADInventory.Models.PurchaseDocDetail;
    using BSG.ADInventory.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Data;
    using System.Linq;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Models;
    using Zcf.Models.Enums;
    using Zcf.Paging;
    using Zcf.Services;
    using DocumentFormat.OpenXml.Office2010.Excel;
    using BSG.ADInventory.Models.Report;
    using DocumentFormat.OpenXml.Office.Word;
    using BSG.ADInventory.Common.Enum;
    using BSG.ADInventory.Models.InvDoc;
    using DocumentFormat.OpenXml.Wordprocessing;
    using System.ComponentModel.DataAnnotations;
    using System.Security.AccessControl;


    /// <summary>
    /// Create Service For PurchaseDoc" />
    /// </summary>
    public class PurchaseDocService : CrudService<PurchaseDoc>, IPurchaseDocService
    {
        private readonly IPurchaseDocRepository purchaseDocRepository;
        private readonly IPurchaseDocDetailRepository purchaseDocDetailRepository;
        private readonly IMatOrderDetailRepository matOrderDetailRepository;
        private readonly ISqlCommandRepository sqlCommandRepository;



        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseDocService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="PurchaseDocRepository"></param>
        public PurchaseDocService(IUnitOfWork unitOfWork, IPurchaseDocRepository purchaseDocRepository,
            IPurchaseDocDetailRepository purchaseDocDetailRepository,
            IMatOrderDetailRepository matOrderDetailRepository,
            ISqlCommandRepository sqlCommandRepository)
            : base(unitOfWork, purchaseDocRepository)
        {
            this.purchaseDocRepository = purchaseDocRepository;
            this.purchaseDocDetailRepository = purchaseDocDetailRepository;
            this.matOrderDetailRepository = matOrderDetailRepository;
            this.sqlCommandRepository = sqlCommandRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<PurchaseDoc> GetItems(PagedQueryable criteria)
        {
            var data = this.purchaseDocRepository.Query;
            return data.ToPagedQueryable(criteria);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">type:0 all, type:1 current</param>
        /// <returns></returns>
        public IEnumerable<PurchaseDocListModel> GetDataList(int type)
        {

            var DocType = new SqlParameter { ParameterName = "@Type", Direction = ParameterDirection.Input, Value = type, DbType = DbType.Int32 };


            List<PurchaseDocListModel> data = new List<PurchaseDocListModel>();
            try
            {
                data = (from r in sqlCommandRepository.ExecuteStoredProcedureList<PurchaseDocListModel>("SP_GetPurchaseDocDataList @Type", DocType)
                        select new PurchaseDocListModel
                        {
                            Id = r.Id,
                            Provider = r.Provider,
                            Project = r.Project,
                            IsAggregated = r.IsAggregated,
                            AgentUser=r.AgentUser,
                            CreateTime = r.CreateTime,
                            CreateUser = r.CreateUser,
                            ItemsSummary = r.ItemsSummary,
                            Attachments = r.Attachments
                        }
                         ).ToList();
            }
            catch (Exception ex)
            {
                var reza = ex;
                throw;
            }
            return data;


            // Old Methid Before Connect To Tadbir

            //var data = this.purchaseDocRepository.Query.Where(d => (d.IsFinished == false && type == 1) || type == 0).AsEnumerable().OrderByDescending(t=>t.CreateTime).Select(t => new PurchaseDocListModel
            //{
            //    Id = t.Id,
            //    Provider = t.Provider.Title,
            //    Project = t.Project.Title,
            //    IsAggregated =t.IsAggregated,
            //    CreateTime = t.CreateTime,
            //    CreateUser = t.CreateUser.FullName,
            //    ItemsSummary = String.Join(",", t.PurchaseDocDetails.Select(dt => dt.Mat.Title).Distinct().ToArray()),
            //    Attachments=t.PurchaseDocAttachments.Count
            //}).ToList();

            //return (data);
        }

        public CustomResult<bool> OrderToPurchase(PurchaseDocDataModel dataModel)
        {
            CustomResult<bool> data = new CustomResult<bool>();
            if (dataModel.Id > 0)
            {
                return data;
            }
            else
            {

                var duplicate = (dataModel.Details.Where(m => (this.purchaseDocDetailRepository.Query.Where(d => d.MatOrderDetailId == m.MatOrderDetailId).Select(o => new { Column1 = 1 }).Take(1)).FirstOrDefault() != null)
                        .Select(m => new MatOrderDetailInfoListModel
                        {
                            Id = m.Id,
                            MatId = m.MatId,
                            Amount = m.Amount,
                            MatTitle = m.MatInfo
                        })).ToList();


                if (duplicate.Count > 0)
                {
                    foreach (var item in duplicate.ToList())
                    {
                        data.Messages.Add(new ResultMessage
                        {
                            MessageType = ResultMessageType.Error,
                            Message = Resource.DuplicateData,
                            Description = string.Format(Resource.DuplicateMatOrder, item.MatId, item.Amount)
                        });
                    }

                    data.Result = false;
                    data.ErrorMessage = Resource.DuplicateData;
                    return data;
                }
                else
                {
                    PurchaseDoc p = new PurchaseDoc { ProviderId = dataModel.ProviderId, IsAggregated = dataModel.IsAggregated, ProjectId = dataModel.ProjectId, AgentUserId=dataModel.AgentUserId };
                    foreach (var item in dataModel.Details)
                    {
                        PurchaseDocDetail pd = new PurchaseDocDetail
                        {
                            PurchaseDoc = p,
                            MatId = item.MatId,
                            MatOrderDetailId = item.MatOrderDetailId,
                            Amount = item.Amount
                        };
                        p.PurchaseDocDetails.Add(pd);
                    }
                    try
                    {
                        this.purchaseDocRepository.Add(p);
                        this.UnitOfWork.Commit();
                        data.Result = true;
                        data.ErrorMessage = Resource.DataSavedSuccessfully;
                    }
                    catch (System.Exception ex)
                    {

                        //throw;
                        data.Result = false;
                        data.ErrorMessage = Resource.ErrorCallAdmin + "\n" + ex.Message;
                    }

                }
            }

            return data;
        }

        public CustomResult<bool> SaveDoc(PurchaseDocDataModel dataModel)
        {
            CustomResult<bool> data = new CustomResult<bool>();
            if (dataModel.Id > 0)
            {
                var doc = this.purchaseDocRepository.GetItemByKey(dataModel.Id);
                //doc.PurchaseDocDetails.Clear();
                foreach (var item in doc.PurchaseDocDetails.ToList())
                {
                    this.purchaseDocDetailRepository.Remove(item);
                }

                var duplicate = (dataModel.Details.Where(m => (doc.PurchaseDocDetails.Where(d => d.PurchaseDocId == dataModel.Id && ((d.MatOrderDetailId == m.MatOrderDetailId && m.MatOrderDetailId != null)
                                                                || (m.MatOrderId == d.MatOrderDetail.MatOrderId && m.MatOrderId != null && d.MatId == m.MatId))).Select(o => new { Column1 = 1 }).Take(1)).FirstOrDefault() != null)
                        .Select(m => new MatOrderDetailInfoListModel
                        {
                            Id = m.Id,
                            MatId = m.MatId,
                            Amount = m.Amount,
                            MatTitle = m.MatInfo
                        })).ToList();



                if (duplicate.Count > 0)
                {
                    foreach (var item in duplicate.ToList())
                    {
                        data.Messages.Add(new ResultMessage
                        {
                            MessageType = ResultMessageType.Error,
                            Message = Resource.DuplicateData,
                            Description = string.Format(Resource.DuplicateMatOrder, item.MatId, item.Amount)
                        });
                    }

                    data.Result = false;
                    data.ErrorMessage = Resource.DuplicateData;
                    return data;
                }
                else
                {
                    doc.ProjectId = dataModel.ProjectId;
                    doc.ProviderId = dataModel.ProviderId;
                    doc.IsAggregated = dataModel.IsAggregated;

                    foreach (var item in dataModel.Details)
                    {
                        PurchaseDocDetail pd = new PurchaseDocDetail
                        {
                            PurchaseDoc = doc,
                            MatId = item.MatId,
                            MatOrderDetailId = item.MatOrderDetailId,
                            Amount = item.Amount
                        };
                        doc.PurchaseDocDetails.Add(pd);
                    }
                    try
                    {
                        this.purchaseDocRepository.Update(doc);
                        this.UnitOfWork.Commit();
                        data.Result = true;
                        data.ErrorMessage = Resource.DataSavedSuccessfully;
                    }
                    catch (System.Exception ex)
                    {

                        //throw;
                        data.Result = false;
                        data.ErrorMessage = Resource.ErrorCallAdmin + "\n" + ex.Message;
                    }

                }

                return data;
            }
            else
            {
                var duplicate = (dataModel.Details.Where(m => (this.purchaseDocDetailRepository.Query.Where(d => (d.MatOrderDetailId == m.MatOrderDetailId && m.MatOrderDetailId != null)
                                                                || (m.MatOrderId == d.MatOrderDetail.MatOrderId && m.MatOrderId != null && d.MatId == m.MatId)).Select(o => new { Column1 = 1 }).Take(1)).FirstOrDefault() != null)
                        .Select(m => new MatOrderDetailInfoListModel
                        {
                            Id = m.Id,
                            MatId = m.MatId,
                            Amount = m.Amount,
                            MatTitle = m.MatInfo
                        })).ToList();


                if (duplicate.Count > 0)
                {
                    foreach (var item in duplicate.ToList())
                    {
                        data.Messages.Add(new ResultMessage
                        {
                            MessageType = ResultMessageType.Error,
                            Message = Resource.DuplicateData,
                            Description = string.Format(Resource.DuplicateMatOrder, item.MatId, item.Amount)
                        });
                    }

                    data.Result = false;
                    data.ErrorMessage = Resource.DuplicateData;
                    return data;
                }
                else
                {
                    PurchaseDoc p = new PurchaseDoc { ProjectId = dataModel.ProjectId, ProviderId = dataModel.ProviderId, IsAggregated = dataModel.IsAggregated };
                    foreach (var item in dataModel.Details)
                    {
                        PurchaseDocDetail pd = new PurchaseDocDetail
                        {
                            PurchaseDoc = p,
                            MatId = item.MatId,
                            MatOrderDetailId = item.MatOrderDetailId,
                            Amount = item.Amount
                        };
                        p.PurchaseDocDetails.Add(pd);
                    }
                    try
                    {
                        this.purchaseDocRepository.Add(p);
                        this.UnitOfWork.Commit();
                        data.Result = true;
                        data.ErrorMessage = Resource.DataSavedSuccessfully;
                    }
                    catch (System.Exception ex)
                    {

                        //throw;
                        data.Result = false;
                        data.ErrorMessage = Resource.ErrorCallAdmin + "\n" + ex.Message;
                    }

                }
            }

            return data;
        }

        public CustomResult<bool> CheckOrderMat(long matId, long matOrderId)
        {
            CustomResult<bool> data = new CustomResult<bool>();

            var matOrder = this.matOrderDetailRepository.Query.Where(d => d.MatId == matId && d.MatOrderId == matOrderId).Any();

            if (!this.matOrderDetailRepository.Query.Where(d => d.MatId == matId && d.MatOrderId == matOrderId).Any())
            {
                data.ErrorMessage = Resource.ThereIsNotMatOrderWithThisMat;
                data.Result = false;
            }
            else if (this.purchaseDocDetailRepository.Query.Where(d => d.MatId == matId && d.MatOrderDetailId != null && d.MatOrderDetail.MatOrderId == matOrderId).Any())
            {
                data.ErrorMessage = Resource.DuplicateData;
                data.Result = false;
            }

            return data;
        }

        public List<PurchaseDocFollowModel> GetPurchaseDocFollowItems(long purchaseDocId, int type)
        {

            var PurchaseDocId = new SqlParameter { ParameterName = "@PurchaseDocId", Direction = ParameterDirection.Input, Value = purchaseDocId, DbType = DbType.Int64 };
            var Type = new SqlParameter { ParameterName = "@Type", Direction = ParameterDirection.Input, Value = type, DbType = DbType.Int32 };

            List<PurchaseDocFollowModel> data = new List<PurchaseDocFollowModel>();
            try
            {
                data = (from r in sqlCommandRepository.ExecuteStoredProcedureList<PurchaseDocFollowModel>("Sp_RepFollowPurchaseDoc @purchaseDocId, @type", PurchaseDocId, Type)
                        select new PurchaseDocFollowModel
                        {
                            MatId = Int64.Parse(r.MatId.ToString()),
                            MatTitle = r.MatTitle,
                            Amount = r.Amount,
                            MatUnit = r.MatUnit
                        }
                         ).ToList();
            }
            catch (Exception ex)
            {
                var reza = ex;
                throw;
            }
            return data;
        }

        public List<PurchaseDocFollowListModel> RepPurchaseDocFollow(FollowPurchaseDocRepParam paramModel)
        {
            var StartDate = new SqlParameter { ParameterName = "@StartDate", Direction = ParameterDirection.Input, Value = paramModel.StartDateStringVal ?? string.Empty, DbType = DbType.String };
            var EndDate = new SqlParameter { ParameterName = "@EndDate", Direction = ParameterDirection.Input, Value = paramModel.EndDateStringVal ?? string.Empty, DbType = DbType.String };
            var PurchaseDocId = new SqlParameter { ParameterName = "@PurchaseDocId", Direction = ParameterDirection.Input, Value = paramModel.PurchaseDocId ?? 0, DbType = DbType.Int64 };
            var MatId = new SqlParameter { ParameterName = "@MatId", Direction = ParameterDirection.Input, Value = paramModel.MatId ?? 0, DbType = DbType.Int64 };
            var ReportType = new SqlParameter { ParameterName = "@ReportType", Direction = ParameterDirection.Input, Value = paramModel.ReportType, DbType = DbType.Int32 };


            List<PurchaseDocFollowListModel> data = new List<PurchaseDocFollowListModel>();
            try
            {
                data = (from r in sqlCommandRepository.ExecuteStoredProcedureList<PurchaseDocFollowListModel>("BSG_RepFollowPurchaseDoc @StartDate, @EndDate, @PurchaseDocId, @MatId, @ReportType", StartDate, EndDate, PurchaseDocId, MatId, ReportType)
                        select new PurchaseDocFollowListModel
                        {
                            PurchaseDocId = r.PurchaseDocId,
                            IsAggregated = r.IsAggregated,
                            Project = r.Project,
                            MatId = r.MatId,
                            MatTitle = r.MatTitle,
                            MatUnit = r.MatUnit,
                            PurchaseQty = r.PurchaseQty,
                            ReciveQty = r.ReciveQty,
                            DistribQty = r.DistribQty,
                            ExitQty = r.ExitQty,
                            CreateTime = r.CreateTime

                        }
                         ).ToList();
            }
            catch (Exception ex)
            {
                var reza = ex;
                throw;
            }
            return data;
        }

        public List<PurchaseDocDetailListModel> RepPurchaseDocDetail(FollowPurchaseDocRepParam paramModel)
        {
            var StartDate = new SqlParameter { ParameterName = "@StartDate", Direction = ParameterDirection.Input, Value = paramModel.StartDateStringVal ?? string.Empty, DbType = DbType.String };
            var EndDate = new SqlParameter { ParameterName = "@EndDate", Direction = ParameterDirection.Input, Value = paramModel.EndDateStringVal ?? string.Empty, DbType = DbType.String };
            var PurchaseDocId = new SqlParameter { ParameterName = "@PurchaseDocId", Direction = ParameterDirection.Input, Value = paramModel.PurchaseDocId ?? 0, DbType = DbType.Int64 };
            var MatId = new SqlParameter { ParameterName = "@MatId", Direction = ParameterDirection.Input, Value = paramModel.MatId ?? 0, DbType = DbType.Int64 };
            var ReportType = new SqlParameter { ParameterName = "@ReportType", Direction = ParameterDirection.Input, Value = paramModel.ReportType, DbType = DbType.Int32 };

            List<PurchaseDocDetailListModel> data = new List<PurchaseDocDetailListModel>();
            try
            {
                data = (from r in sqlCommandRepository.ExecuteStoredProcedureList<PurchaseDocDetailListModel>("BSG_RepFollowPurchaseDoc @StartDate, @EndDate, @PurchaseDocId, @MatId, @ReportType", StartDate, EndDate, PurchaseDocId, MatId, ReportType)
                        select new PurchaseDocDetailListModel
                        {
                            PurchaseDocId = r.PurchaseDocId,
                            Project = r.Project,
                            IsAggregated = r.IsAggregated,
                            MatId = r.MatId,
                            MatTitle = r.MatTitle,
                            MatUnit = r.MatUnit,
                            RequestProject = r.RequestProject,
                            MatOrderId = r.MatOrderId,
                            RequestedAmount = r.RequestedAmount,
                            ConfirmedAmount = r.ConfirmedAmount,
                            PurchaseAmount = r.PurchaseAmount,
                            RequiredDate = r.RequiredDate

                        }
                         ).ToList();
            }
            catch (Exception ex)
            {
                var reza = ex;
                throw;
            }
            return data;
        }

        public List<PurchaseDocInvTransactionListModel> RepPurchaseDocInvTransaction(FollowPurchaseDocRepParam paramModel)
        {
            var StartDate = new SqlParameter { ParameterName = "@StartDate", Direction = ParameterDirection.Input, Value = paramModel.StartDateStringVal ?? string.Empty, DbType = DbType.String };
            var EndDate = new SqlParameter { ParameterName = "@EndDate", Direction = ParameterDirection.Input, Value = paramModel.EndDateStringVal ?? string.Empty, DbType = DbType.String };
            var PurchaseDocId = new SqlParameter { ParameterName = "@PurchaseDocId", Direction = ParameterDirection.Input, Value = paramModel.PurchaseDocId ?? 0, DbType = DbType.Int64 };
            var MatId = new SqlParameter { ParameterName = "@MatId", Direction = ParameterDirection.Input, Value = paramModel.MatId ?? 0, DbType = DbType.Int64 };
            var ReportType = new SqlParameter { ParameterName = "@ReportType", Direction = ParameterDirection.Input, Value = paramModel.ReportType, DbType = DbType.Int32 };

            List<PurchaseDocInvTransactionListModel> data = new List<PurchaseDocInvTransactionListModel>();
            try
            {
                data = (from r in sqlCommandRepository.ExecuteStoredProcedureList<PurchaseDocInvTransactionListModel>("BSG_RepFollowPurchaseDoc @StartDate, @EndDate, @PurchaseDocId, @MatId, @ReportType", StartDate, EndDate, PurchaseDocId, MatId, ReportType)
                        select new PurchaseDocInvTransactionListModel
                        {
                            DocNo = r.DocNo,
                            PurchaseDocId = r.PurchaseDocId,
                            InvDocType = r.InvDocType,
                            Project = r.Project,
                            InvDocStatus = r.InvDocStatus,
                            InvDocStatusTitle = Common.EnumHelper<InvDocStatus>.GetDisplayName(r.InvDocStatus),
                            MatId = r.MatId,
                            MatTitle = r.MatTitle,
                            MatUnit = r.MatUnit,
                            Amount = r.Amount,
                            Source = r.Source,
                            Dest = r.Dest,
                            CreateTime = r.CreateTime,
                            ConfirmTime = r.ConfirmTime
                        }
                         ).ToList();
            }
            catch (Exception ex)
            {
                var reza = ex;
                throw;
            }
            return data;
        }

        public List<PurchaseDocOnWayListModel> GetPurchaseDocOnWay()
        {
            List<PurchaseDocOnWayListModel> data = new List<PurchaseDocOnWayListModel>();
            try
            {
                data = this.sqlCommandRepository.ExecuteStoredProcedureList<PurchaseDocOnWayListModel>("BSG_RepMatOnWay")/*.Select(p => new PurchaseDocOnWayListModel
                {
                    PurchaseDocId = p.PurchaseDocId,
                    IsAggregated = p.IsAggregated,
                    ProjectTitle = p.ProjectTitle,
                    MatId = p.MatId,
                    MatTitle = p.MatTitle,
                    MatUnit = p.MatUnit,
                    PurchaseQty = p.PurchaseQty,
                    ReciveQty = p.ReciveQty,
                    CreateTime = p.CreateTime,
                    ProviderName = p.ProviderName
                })*/.ToList();
            }
            catch (Exception ex)
            {
                var reza = ex;
                throw;
            }
            return data;
        }

        //public List<PurchaseDocInvTransactionListModel> RepPurchaseDocSepeInvTransaction(FollowPurchaseDocRepParam paramModel)
        //{

        //    var PurchaseDocId = new SqlParameter { ParameterName = "@PurchaseDocId", Direction = ParameterDirection.Input, Value = paramModel.PurchaseDocId, DbType = DbType.Int64 };
        //    var MatId = new SqlParameter { ParameterName = "@MatId", Direction = ParameterDirection.Input, Value = paramModel.MatId ?? 0, DbType = DbType.Int64 };
        //    var ReportType = new SqlParameter { ParameterName = "@ReportType", Direction = ParameterDirection.Input, Value = paramModel.ReportType, DbType = DbType.Int32 };

        //    List<PurchaseDocInvTransactionListModel> data = new List<PurchaseDocInvTransactionListModel>();
        //    try
        //    {
        //        data = (from r in sqlCommandRepository.ExecuteStoredProcedureList<PurchaseDocInvTransactionListModel>("BSG_RepFollowPurchaseDoc @PurchaseDocId, @MatId, @ReportType", PurchaseDocId, MatId, ReportType)
        //                select new PurchaseDocInvTransactionListModel
        //                {
        //                    DocNo = r.DocNo,
        //                    PurchaseDocId = r.PurchaseDocId,
        //                    InvDocType = r.InvDocType,
        //                    Project = r.Project,
        //                    InvDocStatus = r.InvDocStatus,
        //                    InvDocStatusTitle = Common.EnumExtention<InvDocStatus>.GetDisplayName(r.InvDocStatus),
        //                    MatId = r.MatId,
        //                    MatTitle = r.MatTitle,
        //                    MatUnit = r.MatUnit,
        //                    Amount = r.Amount,
        //                    Source = r.Source,
        //                    Dest = r.Dest,
        //                    CreateTime = r.CreateTime,
        //                    ConfirmTime = r.ConfirmTime
        //                }
        //                 ).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        var reza = ex;
        //        throw;
        //    }
        //    return data;
        //}

        //public List<PurchaseDocInvTransactionListModel> RepPurchaseDocInvTransaction(FollowPurchaseDocRepParam paramModel)
        //{

        //    var PurchaseDocId = new SqlParameter { ParameterName = "@PurchaseDocId", Direction = ParameterDirection.Input, Value = paramModel.PurchaseDocId, DbType = DbType.Int64 };
        //    var MatId = new SqlParameter { ParameterName = "@MatId", Direction = ParameterDirection.Input, Value = paramModel.MatId ?? 0, DbType = DbType.Int64 };
        //    var ReportType = new SqlParameter { ParameterName = "@ReportType", Direction = ParameterDirection.Input, Value = paramModel.ReportType, DbType = DbType.Int32 };

        //    List<PurchaseDocInvTransactionListModel> data = new List<PurchaseDocInvTransactionListModel>();
        //    try
        //    {
        //        data = (from r in sqlCommandRepository.ExecuteStoredProcedureList<PurchaseDocInvTransactionListModel>("BSG_RepFollowPurchaseDoc @PurchaseDocId, @MatId, @ReportType", PurchaseDocId, MatId, ReportType)
        //                select new PurchaseDocInvTransactionListModel
        //                {
        //                    DocNo = r.DocNo,
        //                    PurchaseDocId = r.PurchaseDocId,
        //                    InvDocType = r.InvDocType,
        //                    Project = r.Project,
        //                    InvDocStatus = r.InvDocStatus,
        //                    InvDocStatusTitle = Common.EnumExtention<InvDocStatus>.GetDisplayName(r.InvDocStatus),
        //                    MatId = r.MatId,
        //                    MatTitle = r.MatTitle,
        //                    MatUnit = r.MatUnit,
        //                    Amount = r.Amount,
        //                    Source = r.Source,
        //                    Dest = r.Dest,
        //                    CreateTime = r.CreateTime,
        //                    ConfirmTime = r.ConfirmTime
        //                }
        //                 ).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        var reza = ex;
        //        throw;
        //    }
        //    return data;
        //}

        //public List<PurchaseDocInvTransactionListModel> RepPurchaseDocInvTransaction(FollowPurchaseDocRepParam paramModel)
        //{

        //    var PurchaseDocId = new SqlParameter { ParameterName = "@PurchaseDocId", Direction = ParameterDirection.Input, Value = paramModel.PurchaseDocId, DbType = DbType.Int64 };
        //    var MatId = new SqlParameter { ParameterName = "@MatId", Direction = ParameterDirection.Input, Value = paramModel.MatId ?? 0, DbType = DbType.Int64 };
        //    var ReportType = new SqlParameter { ParameterName = "@ReportType", Direction = ParameterDirection.Input, Value = paramModel.ReportType, DbType = DbType.Int32 };

        //    List<PurchaseDocInvTransactionListModel> data = new List<PurchaseDocInvTransactionListModel>();
        //    try
        //    {
        //        data = (from r in sqlCommandRepository.ExecuteStoredProcedureList<PurchaseDocInvTransactionListModel>("BSG_RepFollowPurchaseDoc @PurchaseDocId, @MatId, @ReportType", PurchaseDocId, MatId, ReportType)
        //                select new PurchaseDocInvTransactionListModel
        //                {
        //                    DocNo = r.DocNo,
        //                    PurchaseDocId = r.PurchaseDocId,
        //                    InvDocType = r.InvDocType,
        //                    Project = r.Project,
        //                    InvDocStatus = r.InvDocStatus,
        //                    InvDocStatusTitle = Common.EnumExtention<InvDocStatus>.GetDisplayName(r.InvDocStatus),
        //                    MatId = r.MatId,
        //                    MatTitle = r.MatTitle,
        //                    MatUnit = r.MatUnit,
        //                    Amount = r.Amount,
        //                    Source = r.Source,
        //                    Dest = r.Dest,
        //                    CreateTime = r.CreateTime,
        //                    ConfirmTime = r.ConfirmTime
        //                }
        //                 ).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        var reza = ex;
        //        throw;
        //    }
        //    return data;
        //}

    }
}
