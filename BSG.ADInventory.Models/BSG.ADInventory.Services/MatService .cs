namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Common;
    using BSG.ADInventory.Common.Enum;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.Mat;
    using BSG.ADInventory.Models.Report;
    using Data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;


    /// <summary>
    /// Create Service For mat" />
    /// </summary>
    public class MatService : CrudService<Mat>, IMatService
    {
        private readonly IMatRepository matRepository;
        //private readonly IInvDocDetailRepository invDocDetailRepository;
        private readonly ISqlCommandRepository sqlCommandRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="matRepository"></param>
        public MatService(IUnitOfWork unitOfWork, IMatRepository matRepository,
            //IInvDocDetailRepository invDocDetailRepository,
            ISqlCommandRepository sqlCommandRepository)
            : base(unitOfWork, matRepository)
        {
            this.matRepository = matRepository;
            //this.invDocDetailRepository = invDocDetailRepository;
            this.sqlCommandRepository = sqlCommandRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<Mat> GetItems(PagedQueryable criteria)
        {
            var data = this.matRepository.Query;
            return data.ToPagedQueryable(criteria);
        }

        public IEnumerable<MatListModel> GetDataList()
        {
            var data = this.matRepository.GetAllItems().Select(t => new MatListModel
            {
                Id = t.Id,
                Code = t.Code,
                Title = t.Title,
                MatGroupId = t.MatGroupId,
                MatGroupTitle = t.MatGroup.Title,
                BrandTitle = t.Brand == null ? string.Empty : (t.Brand.FaTitle + " " + t.Brand.EnTitle ?? string.Empty),
                MatUnit = t.MatUnit.Title,
                Model = t.Model,
                Dimention = t.Dimention,
                TechnicalSpec = t.TechnicalSpec,
                MinimumInventory = t.MinimumInventory,
                OrderPoint = t.OrderPoint,
                MatAllocationType = EnumExtention<MatAllocationType>.GetDisplayName(t.MatAllocationType),
                MatStorageMethod = EnumExtention<MatStorageMethod>.GetDisplayName(t.MatStorageMethod ?? 0)

            }).ToList();

            return (data);
        }
        public double GetMatQty(long branchId, long matId)
        {
            //var data = this.invDocDetailRepository.GetAllItems();
            //.Where(d => d.MatId == matId && (
            //(d.InvDoc.SourceBranchId == branchId && d.InvDoc.InvDocTypeId==1)
            //||
            //(d.InvDoc.DestBranchId == branchId && d.InvDoc.InvDocTypeId > 1)
            //))
            //.Select(d => (d.Qty * (int)d.InvDoc.InvDocType.DocSign)).Sum();
            //return data;

            return 0;
        }

        #region Reports
        public List<MatInventoryModel> RepInventoryList(InventoryListReportParam filterModel)
        {

            var ProvinceId = new SqlParameter();
            ProvinceId.ParameterName = "@ProvinceId";
            ProvinceId.Direction = ParameterDirection.Input;
            ProvinceId.Value = filterModel.ProvinceId;
            ProvinceId.DbType = DbType.Int64;

            var TownshipId = new SqlParameter();
            TownshipId.ParameterName = "@TownshipId";
            TownshipId.Direction = ParameterDirection.Input;
            TownshipId.Value = filterModel.TownshipId;
            TownshipId.DbType = DbType.Int64;

            var BranchId = new SqlParameter();
            BranchId.ParameterName = "@BranchId";
            BranchId.Direction = ParameterDirection.Input;
            BranchId.Value = filterModel.BranchId;
            BranchId.DbType = DbType.Int64;

            var MatId = new SqlParameter();
            MatId.ParameterName = "@MatId";
            MatId.Direction = ParameterDirection.Input;
            MatId.Value = 0;
            MatId.DbType = DbType.Int64;


            List<MatInventoryModel> data = new List<MatInventoryModel>();
            try
            {

                data = (from r in sqlCommandRepository.ExecuteStoredProcedureList<MatInventoryModel>("Sp_RepInventoryMain @ProvinceId, @TownshipId, @BranchId, @MatId", ProvinceId, TownshipId, BranchId, MatId)
                        select new MatInventoryModel
                        {
                            ProvinceId = r.ProvinceId,
                            ProvinceTitle = r.ProvinceTitle,
                            TownshipId = r.TownshipId,
                            TownshipTitle = r.TownshipTitle,
                            BranchId = r.BranchId,
                            BranchTitle = r.BranchTitle,
                            MatId = r.MatId,
                            MatTitle = r.MatTitle,
                            MatUnitTitle = r.MatUnitTitle,
                            MatQty = r.MatQty
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

        public bool IsDuplicateMatCode(string matCode)
        {
            return this.matRepository.Query.Where(m => m.Code == matCode).Any();
        }

        public List<MatStockViewModel> RepMatStock(MatStockRepParam paramModel)
        {
            var MatId = new SqlParameter { ParameterName = "@MatId", Direction = ParameterDirection.Input, Value = paramModel.MatId?? 0, DbType = DbType.Int64 };
            var ProjectId = new SqlParameter { ParameterName = "@CalibrationTypeId", Direction = ParameterDirection.Input, Value = paramModel.ProjectId ?? 0, DbType = DbType.Int64 };
            var StockDate = new SqlParameter { ParameterName = "@StockDate", Direction = ParameterDirection.Input, Value = paramModel.StockDate ?? "", DbType = DbType.String };



            List<MatStockViewModel> data = new List<MatStockViewModel>();
            try
            {
                data = (from r in sqlCommandRepository.ExecuteStoredProcedureList<MatStockViewModel>("BSG_RepMatStock @MatId, @ProjectId, @StockDate",
                    MatId, ProjectId, StockDate)
                        select new MatStockViewModel
                        {
                            Id = r.Id,
                            Code = r.Code,
                            Title = r.Title,
                            Project = r.Project,
                            Qty = r.Qty,
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
        #endregion Reports
    }
}


