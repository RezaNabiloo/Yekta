using BSG.ADInventory.Data;
using BSG.ADInventory.Entities;
using BSG.ADInventory.Models.DataTable;
using BSG.ADInventory.Resources;
using BSG.ADInventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMarkupMin.AspNet4.Mvc;
using Zcf.Data;
using Zcf.Models;
using Zcf.Paging;
using Zcf.Web.Mvc.Controllers;
using Zcf.Web.Mvc.Security;

namespace BSG.ADInventory.WebUI.Controllers
{
    [MinifyHtml]
    [Authorize]
    [Permission(PermissionIds = "SystemManagement,BaseDataManagement,GeoLocations", LoginUrl = "~/Error/Index/403")]
    public class MatPurchaseController : CrudController<MatPurchase>
    {
        #region ctor
        private IUnitOfWork unitOfWork;        
        private readonly IMatPurchaseService matPurchaseService;
        private readonly IInvDocService invDocService;
        private readonly IInvDocDetailService invDocDetailService;


        public MatPurchaseController(IUnitOfWork unitOfWork, IMatPurchaseService matPurchaseService,
            IInvDocService invDocService,
            IInvDocDetailService invDocDetailService)
            : base(matPurchaseService)
        {
            this.unitOfWork = unitOfWork;
            this.matPurchaseService = matPurchaseService;
            this.invDocService = invDocService;
            this.invDocDetailService = invDocDetailService;
        }

        [NonAction]
        public override ActionResult Index(PagedQueryable criteria)
        {
            return base.Index(criteria);
        }
        #endregion

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {            
            return this.View();
        }


        public ActionResult GetInvDocData(string docNo)
        {
            var data = this.invDocService.GetInvDocByDocNo(docNo);
            
            return PartialView("InvDocData", data);
        }

        public ActionResult GetDataList(JqDataTable model, long id)
        {

            var data = this.matPurchaseService.GetDataList(id);


            if (model.iSortCol_0 == 1 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.VDocId); }
            else if (model.iSortCol_0 == 1 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.VDocId); }

            if (model.iSortCol_0 == 2 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.DocNo); }
            else if (model.iSortCol_0 == 2 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.DocNo); }

            if (model.iSortCol_0 == 3 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.IsFreight); }
            else if (model.iSortCol_0 == 3 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.IsFreight); }

            if (model.iSortCol_0 == 4 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.MatId); }
            else if (model.iSortCol_0 == 4 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.MatId); }

            if (model.iSortCol_0 == 5 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.MatCode); }
            else if (model.iSortCol_0 == 5 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.MatCode); }

            if (model.iSortCol_0 == 6 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.MatTitle); }
            else if (model.iSortCol_0 == 6 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.MatTitle); }

            if (model.iSortCol_0 == 7 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Qty); }
            else if (model.iSortCol_0 == 7 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Qty); }

            if (model.iSortCol_0 == 8 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.PurchasePrice); }
            else if (model.iSortCol_0 == 8 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.PurchasePrice); }

            if (model.iSortCol_0 == 3 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.FreightPrice); }
            else if (model.iSortCol_0 == 3 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.FreightPrice); }


            
            //Global Search functionality 

            if (!(string.IsNullOrEmpty(model.sSearch)) && !(string.IsNullOrWhiteSpace(model.sSearch)))
            {
                //data = data.Where(x => x.Id.ToString().Contains(model.sSearch) || x.Title.Contains(model.sSearch) || x.TitleEn.Contains(model.sSearch));
            }


            if (!(string.IsNullOrEmpty(model.sSearch_1)) && !(string.IsNullOrWhiteSpace(model.sSearch_1))) { data = data.Where(x => x.VDocId.ToString().Contains(model.sSearch_1)); }

            if (!(string.IsNullOrEmpty(model.sSearch_2)) && !(string.IsNullOrWhiteSpace(model.sSearch_2))) { data = data.Where(x => x.DocNo.ToString().Contains(model.sSearch_2)); }

            if (!(string.IsNullOrEmpty(model.sSearch_3)) && !(string.IsNullOrWhiteSpace(model.sSearch_3))) { data = data.Where(x => x.IsFreight.ToString().Contains(model.sSearch_3)); }

            if (!(string.IsNullOrEmpty(model.sSearch_4)) && !(string.IsNullOrWhiteSpace(model.sSearch_4))) { data = data.Where(x => x.MatId.ToString().Contains(model.sSearch_4)); }

            if (!(string.IsNullOrEmpty(model.sSearch_5)) && !(string.IsNullOrWhiteSpace(model.sSearch_5))) { data = data.Where(x => x.MatCode.ToString().Contains(model.sSearch_5)); }

            if (!(string.IsNullOrEmpty(model.sSearch_6)) && !(string.IsNullOrWhiteSpace(model.sSearch_6))) { data = data.Where(x => x.MatTitle.ToString().Contains(model.sSearch_6)); }

            if (!(string.IsNullOrEmpty(model.sSearch_7)) && !(string.IsNullOrWhiteSpace(model.sSearch_7))) { data = data.Where(x => x.Qty.ToString().Contains(model.sSearch_7)); }

            if (!(string.IsNullOrEmpty(model.sSearch_8)) && !(string.IsNullOrWhiteSpace(model.sSearch_8))) { data = data.Where(x => x.PurchasePrice.ToString().Contains(model.sSearch_8)); }

            if (!(string.IsNullOrEmpty(model.sSearch_9)) && !(string.IsNullOrWhiteSpace(model.sSearch_9))) { data = data.Where(x => x.FreightPrice.ToString().Contains(model.sSearch_9)); }

            //etc....
            var count = data.Count();
            if (model.iDisplayLength == -1)
            {
                model.iDisplayLength = count;
            }
            data = data.Skip(model.iDisplayStart).Take(model.iDisplayLength).ToList();
            var result = new
            {
                iTotalRecords = data.Count(),//records per page 
                iTotalDisplayRecords = count, //total table count
                aaData = data //data list

            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CrudAction(long id, long invDocId, long matId)
        {
            
            MatPurchase data = new MatPurchase { InvDocId=invDocId, MatId=matId, IsFreight=false};
            if (id != 0)
            {
                data = this.matPurchaseService.GetItemByKey(id);
            }
            else 
            {
                var invDoc = this.invDocService.GetItemByKey(invDocId);
                var oldData = invDoc.InvDocDetails.Where(i => i.MatId == matId).FirstOrDefault();

                if (oldData!=null)
                {
                    data.Qty = oldData.RealAmount;
                }
            }
            
            return PartialView("CrudAction", data);
        }

        [HttpPost]
        public ActionResult SetCrudAction(MatPurchase dataModel)
        {   
            if (ModelState.IsValid)
            {
                try
                {
                    if (dataModel.IsFreight)
                    {
                        dataModel.MatId = null;
                        dataModel.Qty= 0;
                    }
                    if (dataModel.Id > 0)
                    {                        
                        this.matPurchaseService.Update(dataModel);
                    }
                    else
                    {
                        this.matPurchaseService.Add(dataModel);
                    }
                    ModelState.Clear();
                    ViewBag.Result = true;
                    ViewBag.ErrorMessage = Resource.DataAddSuccessfully;
                }
                catch (Exception ex)
                {
                    ViewBag.Result = false;
                    ViewBag.ErrorMessage = ex.Message;

                }

                return PartialView("CreateOrEdit");
            }
            else
            {                
                return PartialView("CreateOrEdit", dataModel);
            }

        }


    }
}