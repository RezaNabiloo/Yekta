using BSG.ADInventory.Entities;
using BSG.ADInventory.Models.DataTable;
using BSG.ADInventory.Models.PurchaseDoc;
using BSG.ADInventory.Models.PurchaseDocDetail;
using BSG.ADInventory.Resources;
using BSG.ADInventory.Services;
using System.Linq;
using System.Web.Mvc;
using WebMarkupMin.AspNet4.Mvc;
using Zcf.Data;
using Zcf.Models;
using Zcf.Paging;
using Zcf.Web.Mvc.Controllers;


namespace BSG.ADInventory.WebUI.Controllers
{
    [MinifyHtml]
    [Authorize]
    public class PurchaseDocController : CrudController<PurchaseDoc>
    {

        private IUnitOfWork unitOfWork;
        private readonly IUserService userService;
        private readonly IPeopleService peopleService;
        private readonly IPurchaseDocService purchaseDocService;
        private readonly IMatService matService;
        private readonly IProjectService projectService;
        private readonly IPurchaseDocDetailService purchaseDocDetailService;
        private readonly IMatOrderDetailService matOrderDetailService;
        private readonly IProviderService providerService;



        public PurchaseDocController(IUnitOfWork unitOfWork, IUserService userService,
            IPeopleService peopleService,
            IPurchaseDocService purchaseDocService,
            IPlateCharacterService plateChara,
            IMatService matService,
            IProjectService projectService,
            IPurchaseDocDetailService purchaseDocDetailService,
            IMatOrderDetailService matOrderDetailService,
            IProviderService providerService
            )
             : base(purchaseDocService)
        {
            this.unitOfWork = unitOfWork;
            this.userService = userService;
            this.peopleService = peopleService;
            this.purchaseDocService = purchaseDocService;
            this.matService = matService;
            this.projectService = projectService;
            this.purchaseDocDetailService = purchaseDocDetailService;
            this.matOrderDetailService = matOrderDetailService;
            this.providerService = providerService;
        }

        [NonAction]
        public override ActionResult Index(PagedQueryable criteria)
        {
            return base.Index(criteria);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Archive()
        {
            return View();
        }

        public ActionResult OnWay()
        {            
            return View();
        }

        public ActionResult GetDataList(JqDataTable model, int type)
        {

            var data = this.purchaseDocService.GetDataList(type);


            if (model.iSortCol_0 == 1 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Id); }
            else if (model.iSortCol_0 == 1 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Id); }

            if (model.iSortCol_0 == 2 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.AgentUser); }
            else if (model.iSortCol_0 == 2 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.AgentUser); }

            if (model.iSortCol_0 == 3 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Attachments); }
            else if (model.iSortCol_0 == 3 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Attachments); }

            if (model.iSortCol_0 ==4  && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Provider); }
            else if (model.iSortCol_0 == 4 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Provider); }
            
            if (model.iSortCol_0 == 5 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Project); }
            else if (model.iSortCol_0 == 5 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Project); }

            if (model.iSortCol_0 == 6 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.CreateUser); }
            else if (model.iSortCol_0 == 6 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.CreateUser); }

            if (model.iSortCol_0 == 7 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.CreateTime); }
            else if (model.iSortCol_0 == 7 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.CreateTime); }

            if (model.iSortCol_0 == 8 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.ItemsSummary); }
            else if (model.iSortCol_0 == 8 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.ItemsSummary); }


            ////Global Search functionality 

            if (!(string.IsNullOrEmpty(model.sSearch)) && !(string.IsNullOrWhiteSpace(model.sSearch)))
            {
                data = data.Where(x => x.Id.ToString().Contains(model.sSearch) || x.Attachments.ToString().Contains(model.sSearch) 
                || x.Provider.Contains(model.sSearch)
                || x.AgentUser.Contains(model.sSearch)
                || x.Project.Contains(model.sSearch)
                || x.CreateUser.Contains(model.sSearch)
                || x.CreateTime.ToShortDateString().Contains(model.sSearch)
                || x.ItemsSummary.Contains(model.sSearch));
            }


            if (!(string.IsNullOrEmpty(model.sSearch_1)) && !(string.IsNullOrWhiteSpace(model.sSearch_1))) { data = data.Where(x => x.Id.ToString().Contains(model.sSearch_1)); }
            
            if (!(string.IsNullOrEmpty(model.sSearch_2)) && !(string.IsNullOrWhiteSpace(model.sSearch_2))) { data = data.Where(x => x.AgentUser.ToString().Contains(model.sSearch_2)); }

            if (!(string.IsNullOrEmpty(model.sSearch_3)) && !(string.IsNullOrWhiteSpace(model.sSearch_3))) { data = data.Where(x => x.Attachments.ToString().Contains(model.sSearch_3)); }

            if (!(string.IsNullOrEmpty(model.sSearch_4)) && !(string.IsNullOrWhiteSpace(model.sSearch_4))) { data = data.Where(x => x.Provider.Contains(model.sSearch_4)); }

            if (!(string.IsNullOrEmpty(model.sSearch_5)) && !(string.IsNullOrWhiteSpace(model.sSearch_5))) { data = data.Where(x => x.Project.Contains(model.sSearch_5)); }

            if (!(string.IsNullOrEmpty(model.sSearch_6)) && !(string.IsNullOrWhiteSpace(model.sSearch_6))) { data = data.Where(x => x.CreateUser.Contains(model.sSearch_6)); }

            if (!(string.IsNullOrEmpty(model.sSearch_7)) && !(string.IsNullOrWhiteSpace(model.sSearch_7))) { data = data.Where(x => x.CreateTime.ToShortDateString().Contains(model.sSearch_7)); }

            if (!(string.IsNullOrEmpty(model.sSearch_8)) && !(string.IsNullOrWhiteSpace(model.sSearch_8))) { data = data.Where(x => x.ItemsSummary.Contains(model.sSearch_8)); }

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

        public ActionResult GetOnWayDataList(JqDataTable model)
        {

            var data = this.purchaseDocService.GetPurchaseDocOnWay();


            //if (model.iSortCol_0 == 1 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Id); }
            //else if (model.iSortCol_0 == 1 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Id); }

            //if (model.iSortCol_0 == 2 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Attachments); }
            //else if (model.iSortCol_0 == 2 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Attachments); }

            //if (model.iSortCol_0 == 3 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Provider); }
            //else if (model.iSortCol_0 == 3 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Provider); }

            //if (model.iSortCol_0 == 4 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Project); }
            //else if (model.iSortCol_0 == 4 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Project); }

            //if (model.iSortCol_0 == 5 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.CreateUser); }
            //else if (model.iSortCol_0 == 5 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.CreateUser); }

            //if (model.iSortCol_0 == 6 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.CreateTime); }
            //else if (model.iSortCol_0 == 6 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.CreateTime); }

            //if (model.iSortCol_0 == 7 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.ItemsSummary); }
            //else if (model.iSortCol_0 == 7 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.ItemsSummary); }


            ////Global Search functionality 

            if (!(string.IsNullOrEmpty(model.sSearch)) && !(string.IsNullOrWhiteSpace(model.sSearch)))
            {
                //data = data.Where(x => x.Id.ToString().Contains(model.sSearch) || x.Attachments.ToString().Contains(model.sSearch)
                //|| x.Provider.Contains(model.sSearch)
                //|| x.Project.Contains(model.sSearch)
                //|| x.CreateUser.Contains(model.sSearch)
                //|| x.CreateTime.ToShortDateString().Contains(model.sSearch)
                //|| x.ItemsSummary.Contains(model.sSearch));
            }


            //if (!(string.IsNullOrEmpty(model.sSearch_1)) && !(string.IsNullOrWhiteSpace(model.sSearch_1))) { data = data.Where(x => x.Id.ToString().Contains(model.sSearch_1)); }

            //if (!(string.IsNullOrEmpty(model.sSearch_2)) && !(string.IsNullOrWhiteSpace(model.sSearch_2))) { data = data.Where(x => x.Attachments.ToString().Contains(model.sSearch_2)); }

            //if (!(string.IsNullOrEmpty(model.sSearch_3)) && !(string.IsNullOrWhiteSpace(model.sSearch_3))) { data = data.Where(x => x.Provider.Contains(model.sSearch_3)); }

            //if (!(string.IsNullOrEmpty(model.sSearch_4)) && !(string.IsNullOrWhiteSpace(model.sSearch_4))) { data = data.Where(x => x.Project.Contains(model.sSearch_4)); }

            //if (!(string.IsNullOrEmpty(model.sSearch_4)) && !(string.IsNullOrWhiteSpace(model.sSearch_5))) { data = data.Where(x => x.CreateUser.Contains(model.sSearch_5)); }

            //if (!(string.IsNullOrEmpty(model.sSearch_5)) && !(string.IsNullOrWhiteSpace(model.sSearch_6))) { data = data.Where(x => x.CreateTime.ToShortDateString().Contains(model.sSearch_6)); }

            //if (!(string.IsNullOrEmpty(model.sSearch_6)) && !(string.IsNullOrWhiteSpace(model.sSearch_7))) { data = data.Where(x => x.ItemsSummary.Contains(model.sSearch_7)); }

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
        public ActionResult OrderToPurchase(long id)
        {
            var data = new PurchaseDocDataModel();
            if (id > 0)
            {
                var purchaseDoc = this.purchaseDocService.GetItemByKey(id);
                AutoMapper.Mapper.CreateMap<PurchaseDoc, PurchaseDocDataModel>();
                data = AutoMapper.Mapper.Map<PurchaseDocDataModel>(purchaseDoc);
                foreach (var item in purchaseDoc.PurchaseDocDetails.ToList())
                {
                    PurchaseDocDetailDataModel det = new PurchaseDocDetailDataModel
                    {
                        Id = item.Id,
                        PurchaseDocId = item.PurchaseDocId,
                        MatOrderDetailId = item.MatOrderDetailId,
                        MatId = item.MatOrderDetail.MatId,
                        MatInfo = item.MatOrderDetail.Mat.Title + " - " + item.MatOrderDetail.Mat.TechnicalSpec ?? string.Empty + " - " + item.MatOrderDetail.Mat.Dimention ?? string.Empty
                    };
                    data.Details.Add(det);

                }
            }

            this.MaterialIds();
            this.ProviderIds();
            this.ProjectIds();
            this.AgentUserIds();

            this.OpenOrders();

            return PartialView("OrderToPurchase", data);
        }

        [HttpPost]
        public ActionResult SetOrderToPurchase(PurchaseDocDataModel dataModel)
        {
            CustomResult<bool> data = new CustomResult<bool>();

            if (dataModel.IsAggregated)
            {
                if (dataModel.ProjectId != null)
                {
                    var p = this.projectService.GetItemByKey(dataModel.ProjectId);
                    if (p != null)
                    {
                        if (!p.IsCentralInventory)
                        {
                            data.Result = false;
                            data.ErrorMessage = "برای پروژه/انبار مورد نظر نمیتوان دستور خرید تجمیعی صادر نمود";
                            return Json(data);
                        }
                    }
                }
                else
                {
                    var centralInv = this.projectService.GetAllItems().Where(i => i.IsCentralInventory == true).FirstOrDefault();
                    if (centralInv != null)
                    {
                        dataModel.ProjectId = centralInv.Id;
                    }
                    else
                    {
                        data.Result = false;
                        data.ErrorMessage = "هیچ پروژه/انبار مرکزی در سامانه یافت نشد.";
                        return Json(data);
                    }
                }

            }


            else
            {
                if (dataModel.ProjectId == null)
                {
                    
                        data.Result = false;
                        data.ErrorMessage = "برای دستور خرید غیر تجمیعی باید پروژه/انبار را مشخص نمائید.";
                        return Json(data);

                }
                else {
                    var p = this.projectService.GetItemByKey(dataModel.ProjectId);
                    if (p.IsCentralInventory==true)
                    {
                        data.Result = false;
                        data.ErrorMessage = "برای پروژه/انبار مورد نظر نمیتوان دستور خرید غیر تجمیعی ثبت نمود.";
                        return Json(data);
                    }
                }
            }
             if (dataModel.AgentUserId== null)
                {

                    data.Result = false;
                    data.ErrorMessage = "اقدام کننده جهت خرید را مشخص نمائید.";
                    return Json(data);

                }                
          



            data = this.purchaseDocService.OrderToPurchase(dataModel);            
            return Json(data);
        }


        public ActionResult CrudAction(long id)
        {
            PurchaseDocDataModel data = new PurchaseDocDataModel();
            if (id > 0)
            {
                var purchaseDoc = this.purchaseDocService.GetItemByKey(id);
                AutoMapper.Mapper.CreateMap<PurchaseDoc, PurchaseDocDataModel>();
                data = AutoMapper.Mapper.Map<PurchaseDocDataModel>(purchaseDoc);
                foreach (var item in purchaseDoc.PurchaseDocDetails.ToList())
                {
                    PurchaseDocDetailDataModel det = new PurchaseDocDetailDataModel
                    {
                        Id = item.Id,
                        MatId = item.MatId,
                        MatInfo = item.Mat.Title + "  " + item.Mat.TechnicalSpec + " " + item.Mat.Dimention,
                        MatOrderDetailId = item.MatOrderDetailId,
                        MatOrderId = item.MatOrderDetailId != null ? item.MatOrderDetail.MatOrderId : (long?)null,
                        Amount = item.Amount,
                        MatUnit = item.Mat.MatUnit.Title
                    };
                    data.Details.Add(det);

                }
            }

            this.ProviderIds();
            this.MaterialIds();
            this.ProjectIds();

            return PartialView("CrudAction", data);
        }

        [HttpPost]
        public ActionResult SetCrudAction(PurchaseDocDataModel dataModel)
        {
            CustomResult<bool> data = new CustomResult<bool>();

            if (dataModel.IsAggregated)
            {
                if (dataModel.ProjectId != null)
                {
                    var p = this.projectService.GetItemByKey(dataModel.ProjectId);
                    if (p != null)
                    {
                        if (!p.IsCentralInventory)
                        {
                            data.Result = false;
                            data.ErrorMessage = "برای پروژه/انبار مورد نظر نمی‌توان دستور خرید تجمیعی صادر نمود";
                            return Json(data);
                        }
                    }
                }
                else
                {
                    var centralInv = this.projectService.GetAllItems().Where(i => i.IsCentralInventory == true).FirstOrDefault();
                    if (centralInv != null)
                    {
                        dataModel.ProjectId = centralInv.Id;
                    }
                    else
                    {
                        data.Result = false;
                        data.ErrorMessage = "هیچ پروژه/انبار مرکزی در سامانه یافت نشد.";
                        return Json(data);
                    }
                }

            }


            else
            {
                if (dataModel.ProjectId == null)
                {

                    data.Result = false;
                    data.ErrorMessage = "برای دستور خرید غیر تجمیعی باید پروژه/انبار را مشخص نمائید.";
                    return Json(data);

                }
                else
                {
                    var p = this.projectService.GetItemByKey(dataModel.ProjectId);
                    if (p.IsCentralInventory == true)
                    {
                        data.Result = false;
                        data.ErrorMessage = "برای پروژه/انبار مورد نظر نمیتوان دستور خرید غیر تجمیعی ثبت نمود.";
                        return Json(data);
                    }
                }
            }



            data = this.purchaseDocService.SaveDoc(dataModel);
            return Json(data);
        }

        public ActionResult CheckOrderMat(long matId, long matOrderId)
        {
            var data = this.purchaseDocService.CheckOrderMat(matId, matOrderId);
            return Json(data);

        }


        public ActionResult FollowUpPurchaseDoc(long id)
        {
            var data = this.purchaseDocService.GetItemByKey(id);

            ViewBag.PurchaseDocItems = this.purchaseDocService.GetPurchaseDocFollowItems(id,1);
            ViewBag.PurchaseDocRecivedItems = this.purchaseDocService.GetPurchaseDocFollowItems(id, 2);
            ViewBag.ProviderName = this.providerService.GetItemByKey(data.ProviderId).Title;
            return View(data);

        }

        [HttpPost]
        public ActionResult ClosePurchaseDoc(long id) {
            CustomResult<bool> data = new CustomResult<bool>();

            var doc = this.purchaseDocService.GetItemByKey(id);
            if (doc != null)
            {
                if (doc.IsFinished)
                {
                    data.Result = true;
                    data.ErrorMessage = "ستور خرید مورد نظر قبلا اختتام یافته";
                }
                else
                {
                    doc.IsFinished = true;
                    this.purchaseDocService.Update(doc);
                    data.Result = true;
                    data.ErrorMessage = Resource.DataSavedSuccessfully;
                }
            }
            else
            {
                data.Result = true;
                data.ErrorMessage = Resource.ItemNotFound;
            }

            return Json(data);
        }


        public ActionResult CheckPurchaseDocMat(long purchaseDocId, long matId) 
        {
            CustomResult<bool> data= new CustomResult<bool>();
            var doc = this.purchaseDocService.GetItemByKey(purchaseDocId);
            if (doc==null || doc.IsFinished)
            {
                data.Result = false;
                data.ErrorMessage = "دستور خرید باز با این شماره در سیستم یافت نشد";
            }
            else if (doc.PurchaseDocDetails.Where(d => d.MatId == matId).Any())
            {
                data.Result = true;
            }
            else 
            {
                data.Result = false;
                data.ErrorMessage = "کالای ذکر شده در دستور خرید مورد نظر وجود ندارد";
            }
            return Json(data,JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult GetPurchaseDocDataForInvDoc (long id)
        {
            PurchaseDocCheckInvDocDataModel data = new PurchaseDocCheckInvDocDataModel();

            var pDoc = this.purchaseDocService.GetItemByKey(id);
            var user = this.userService.GetCurrentUser();
            if (pDoc == null || pDoc.IsFinished)
            {
                data.Result = false;
                data.ErrorMessage = "دستور خرید باز با شماه '" + id.ToString() + "' در سامانه پیدا نشد.";
            }
            else 
            {
                if (!user.Projects.Where(p => p.Id == pDoc.ProjectId).Any())
                {
                    data.Result = false;
                    data.ErrorMessage = "دستور خرید شماه '" + id.ToString() + "' مربوط به پروژه '" + pDoc.Project.Title + "' می‌باشد و شما مجاز به گزارش ورود در این انبار نمی‌باشید";
                }
                else
                {
                    data.Result = true;
                    data.ProjectId = pDoc.ProjectId;
                    data.ProviderId = pDoc.ProviderId;
                }
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        private void OpenOrders()
        {
            this.ViewBag.OpenOrders = this.matOrderDetailService.GetOpenOrders();
        }
        //[HttpGet]
        //public ActionResult GetDataList(JqDataTable model, int type)
        //{
        //    var data = this.purchaseDocService.GetDataList(type);

        //    var count = data.Count();
        //    if (model.iDisplayLength == -1)
        //    {
        //        model.iDisplayLength = count;
        //    }
        //    data = data.Skip(model.iDisplayStart).Take(model.iDisplayLength).ToList();
        //    var result = new
        //    {
        //        iTotalRecords = data.Count(),//records per page 
        //        iTotalDisplayRecords = count, //total table count
        //        aaData = data //data list

        //    };

        //    return Json(result, JsonRequestBehavior.AllowGet);



        //}


        private void MaterialIds()
        {
            var mats = this.matService.GetDataList();
            this.ViewBag.Materials = mats;
            this.ViewBag.MaterialIds = new SelectList(mats.Select(s => new { Id = s.Id, Title = s.Id + " - " + s.Title + " " + s.TechnicalSpec + " " + s.Dimention + " | واحدشمارش: " + s.MatUnit }), "Id", "Title");
        }

        private void ProjectIds()
        {
            var user = this.userService.GetCurrentUser();
            this.ViewBag.ProjectIds = new SelectList(user.Projects.Select(s => new { s.Id, s.Title }), "Id", "Title");
        }

        private void ProviderIds()
        {
            this.ViewBag.ProviderIds = new SelectList(this.providerService.GetAllItems().Select(s => new { s.Id, s.Title }), "Id", "Title");
        }

        private void AgentUserIds()
        {
            this.ViewBag.AgentUserIds = new SelectList(this.userService.GetAllItems().Select(s => new { Id=s.Id, Title=s.People.FirstName??""+" "+ s.People.LastName??"" }), "Id", "Title");            
        }

    }
}