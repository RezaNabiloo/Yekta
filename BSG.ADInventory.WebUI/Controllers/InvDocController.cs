using BSG.ADInventory.Common.Enum;
using BSG.ADInventory.Entities;
using BSG.ADInventory.Models.DataTable;
using BSG.ADInventory.Models.InvDoc;
using BSG.ADInventory.Models.InvDocAttachment;
using BSG.ADInventory.Models.Mat;
using BSG.ADInventory.Resources;
using BSG.ADInventory.Services;
using System;
using System.IO;
using System.Linq;
using System.Web;
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
    public class InvDocController : CrudController<InvDoc>
    {

        private IUnitOfWork unitOfWork;
        private readonly IUserService userService;
        private readonly IPeopleService peopleService;
        private readonly IInvDocService invDocService;
        private readonly IInvDocTypeService invDocTypeService;
        private readonly IPlateCharacterService plateCharacterService;
        private readonly ICarTypeService carTypeService;
        private readonly ICarService carService;
        private readonly IProviderService providerService;
        private readonly IMatService matService;
        private readonly IProjectService projectService;
        private readonly IProjectDetailService projectDetailService;
        private readonly IInvDocDetailService invDocDetailService;
        private readonly IInvDocAttachmentService invDocAttachmentService;
        private readonly IPurchaseDocService purchaseDocService;



        public InvDocController(IUnitOfWork unitOfWork, IUserService userService,
            IPeopleService peopleService,
            IInvDocService invDocService,
            IInvDocTypeService invDocTypeService,
            IPlateCharacterService plateCharacterService,
            ICarTypeService carTypeService,
            ICarService carService,
            IProviderService providerService,
            IMatService matService,
            IProjectService projectService,
            IInvDocAttachmentService invDocAttachmentService,
            IProjectDetailService projectDetailService,
            IInvDocDetailService invDocDetailService,
            IPurchaseDocService purchaseDocService
            )
             : base(invDocService)
        {
            this.unitOfWork = unitOfWork;
            this.userService = userService;
            this.peopleService = peopleService;
            this.invDocService = invDocService;
            this.invDocTypeService = invDocTypeService;
            this.plateCharacterService = plateCharacterService;
            this.carTypeService = carTypeService;
            this.carService = carService;
            this.providerService = providerService;
            this.matService = matService;
            this.projectService = projectService;
            this.invDocAttachmentService = invDocAttachmentService;
            this.projectDetailService = projectDetailService;
            this.invDocDetailService = invDocDetailService;
            this.purchaseDocService = purchaseDocService;
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

        [HttpGet]
        public ActionResult GetInvDocs(long id)
        {

            ViewBag.DocTypeId = id;
            return PartialView("_GetInvDocs");

        }

        [HttpGet]
        public ActionResult GetDataList(JqDataTable model, long docType)
        {
            var data = this.invDocService.GetDataList(docType);

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

        [HttpGet]
        public ActionResult GetInvDocData(long id, long docTypeId)
        {
            var docType = this.invDocTypeService.GetItemByKey(docTypeId);
            ViewBag.DocTypeId = docTypeId;
            ViewBag.DocTypeSign = docType.Sign;


            var data = new EntranceDocDataModel { InvDocTypeId = docTypeId };
            this.InvDocTypeIds(docTypeId);
            this.PlateCharacterIds();
            this.ProviderIds();
            this.MaterialIds();
            this.ProjectIds();
            //this.InventoryIds(null);
            this.ProjectDetailIds(null);
            this.PeopleIds();

            return PartialView("_GetInvDocData", data);
        }

        [HttpPost]
        public ActionResult GetInvDocData(EntranceDocDataModel data)
        {

            return PartialView("_GetInvDocData", data);
        }

        [HttpPost]
        public ActionResult SetInvDocData(EntranceDocDataModel dataModel)
        {

            var currentDate = DateTime.Now.ToString();
            var finYear = Int32.Parse(currentDate.Substring(0, 4));
            if (Int32.Parse(currentDate.Substring(5, 2)) >= 11)
            {
                finYear++;
            }
            dataModel.FinanceYear = finYear;
            var data = this.invDocService.SetEntranceDocData(dataModel);

            return Json(data);
        }



        //private void InventoryIds(long? projectId)
        //{
        //    this.ViewBag.InventoryIds = new SelectList(this.inventoryService.GetAllItems().Where(t => t.ProjectId == (projectId ?? 0)).Select(s => new { s.Id, s.Title }), "Id", "Title", projectId);
        //}
        private void ProjectDetailIds(long? projectId)
        {
            this.ViewBag.ProjectDetailIds = new SelectList(this.projectDetailService.GetAllItems().Where(t => t.ProjectId == (projectId ?? 0)).Select(s => new { s.Id, s.Title }), "Id", "Title", projectId);
        }

        [HttpGet]
        public ActionResult GetMatInvQty(long id, long projectId)
        {
            var data = this.invDocDetailService.GetAllItems().Where(d => d.InvDoc.ProjectId == projectId && d.MatId == id && ((d.InvDoc.InvDocType.Sign > 0 && d.InvDoc.InvDocStatus == Common.Enum.InvDocStatus.Definitive) || (d.InvDoc.InvDocType.Sign < 0)))
                .Sum(d => d.RealAmount * d.InvDoc.InvDocType.Sign);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        #region Entrance Doc
        public ActionResult EntranceDoc()
        {
            return View();
        }
        public ActionResult GetDataListEntranceDoc(JqDataTable model)
        {
            var data = this.invDocService.GetDataList(1);

            if (model.iSortCol_0 == 1 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Id); }
            else if (model.iSortCol_0 == 1 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Id); }

            if (model.iSortCol_0 == 2 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.DocNo); }
            else if (model.iSortCol_0 == 2 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.DocNo); }

            if (model.iSortCol_0 == 3 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.PurchaseDocId); }
            else if (model.iSortCol_0 == 3 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.PurchaseDocId); }

            if (model.iSortCol_0 == 4 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Project); }
            else if (model.iSortCol_0 == 4 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Project); }

            if (model.iSortCol_0 == 5 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.CarType); }
            else if (model.iSortCol_0 == 5 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.CarType); }

            if (model.iSortCol_0 == 6 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.CarPlateNumer); }
            else if (model.iSortCol_0 == 6 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.CarPlateNumer); }

            if (model.iSortCol_0 == 7 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.DriverName); }
            else if (model.iSortCol_0 == 7 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.DriverName); }

            if (model.iSortCol_0 == 8 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Source); }
            else if (model.iSortCol_0 == 8 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Source); }

            if (model.iSortCol_0 == 9 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Provider); }
            else if (model.iSortCol_0 == 9 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Provider); }

            if (model.iSortCol_0 == 10 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Operator); }
            else if (model.iSortCol_0 == 10 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Operator); }

            if (model.iSortCol_0 == 11 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.ItemsSummary); }
            else if (model.iSortCol_0 == 11 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.ItemsSummary); }

            if (model.iSortCol_0 == 12 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.CreateTime); }
            else if (model.iSortCol_0 == 12 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.CreateTime); }


            if (!(string.IsNullOrEmpty(model.sSearch)) && !(string.IsNullOrWhiteSpace(model.sSearch)))
            {
                data = data.Where(x => x.Id.ToString().Contains(model.sSearch) || x.DocNo.Contains(model.sSearch) || x.CarType.Contains(model.sSearch)
                || x.PurchaseDocId.ToString().Contains(model.sSearch) || x.Project.Contains(model.sSearch)
                || x.CarPlateNumer.Contains(model.sSearch) || x.DriverName.Contains(model.sSearch) || x.Source.Contains(model.sSearch)
                || x.Provider.Contains(model.sSearch) || x.Operator.Contains(model.sSearch) || x.CreateTime.ToString().Contains(model.sSearch)
                || x.ItemsSummary.Contains(model.sSearch));
            }

            if (!(string.IsNullOrEmpty(model.sSearch_1)) && !(string.IsNullOrWhiteSpace(model.sSearch_1))) { data = data.Where(x => x.Id.ToString().Contains(model.sSearch_1)); }

            if (!(string.IsNullOrEmpty(model.sSearch_2)) && !(string.IsNullOrWhiteSpace(model.sSearch_2))) { data = data.Where(x => x.DocNo.Contains(model.sSearch_2)); }

            if (!(string.IsNullOrEmpty(model.sSearch_3)) && !(string.IsNullOrWhiteSpace(model.sSearch_3))) { data = data.Where(x => x.PurchaseDocId.ToString().Contains(model.sSearch_3)); }

            if (!(string.IsNullOrEmpty(model.sSearch_4)) && !(string.IsNullOrWhiteSpace(model.sSearch_4))) { data = data.Where(x => x.Project.Contains(model.sSearch_4)); }

            if (!(string.IsNullOrEmpty(model.sSearch_5)) && !(string.IsNullOrWhiteSpace(model.sSearch_5))) { data = data.Where(x => x.CarType.Contains(model.sSearch_5)); }

            if (!(string.IsNullOrEmpty(model.sSearch_6)) && !(string.IsNullOrWhiteSpace(model.sSearch_6))) { data = data.Where(x => x.CarPlateNumer.ToString().Contains(model.sSearch_6)); }

            if (!(string.IsNullOrEmpty(model.sSearch_7)) && !(string.IsNullOrWhiteSpace(model.sSearch_7))) { data = data.Where(x => x.DriverName.ToString().Contains(model.sSearch_7)); }

            if (!(string.IsNullOrEmpty(model.sSearch_8)) && !(string.IsNullOrWhiteSpace(model.sSearch_8))) { data = data.Where(x => x.Source.Contains(model.sSearch_8)); }

            if (!(string.IsNullOrEmpty(model.sSearch_9)) && !(string.IsNullOrWhiteSpace(model.sSearch_9))) { data = data.Where(x => x.Provider.Contains(model.sSearch_9)); }

            if (!(string.IsNullOrEmpty(model.sSearch_10)) && !(string.IsNullOrWhiteSpace(model.sSearch_10))) { data = data.Where(x => x.Operator.Contains(model.sSearch_10)); }

            if (!(string.IsNullOrEmpty(model.sSearch_11)) && !(string.IsNullOrWhiteSpace(model.sSearch_11))) { data = data.Where(x => x.ItemsSummary.Contains(model.sSearch_11)); }

            if (!(string.IsNullOrEmpty(model.sSearch_12)) && !(string.IsNullOrWhiteSpace(model.sSearch_12))) { data = data.Where(x => x.CreateTime.ToString().Contains(model.sSearch_12)); }

            var count = data.Count();
            if (model.iDisplayLength == -1)
            {
                model.iDisplayLength = count;
            }
            data = data.Skip(model.iDisplayStart).Take(model.iDisplayLength).ToList();
            var result = new
            {
                iTotalRecords = data.Count(),
                iTotalDisplayRecords = count,
                aaData = data

            };

            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetEntranceDoc(long id)
        {
            var data = new EntranceDocDataModel { InvDocTypeId = 1 };
            if (id > 0)
            {
                var invDoc = this.invDocService.GetItemByKey(id);
                AutoMapper.Mapper.CreateMap<InvDoc, EntranceDocDataModel>();
                data = AutoMapper.Mapper.Map<EntranceDocDataModel>(invDoc);
                foreach (var item in invDoc.InvDocDetails.ToList())
                {
                    InvDocMatDataModel det = new InvDocMatDataModel
                    {
                        Id = item.Id,
                        MatId = item.MatId,
                        MatUnit = item.Mat.MatUnit.Title,
                        Title = item.Mat.Title + "  " + item.Mat.TechnicalSpec + " " + item.Mat.Dimention,
                        ProjectDetailId = item.ProjectDetailId,
                        Amount = item.Amount,
                        RealAmount = item.RealAmount
                    };
                    data.Mats.Add(det);

                }
            }
            this.PlateCharacterIds();
            this.ProviderIds();
            this.MaterialIds();
            this.ProjectIds();
            this.CarTypeIds();
            this.CarIds();
            this.PeopleIds();
            ViewBag.InvDocTypeId = 1;

            return PartialView("CrudEntranceDoc", data);
        }

        [HttpPost]
        public ActionResult SetEntranceDoc(EntranceDocDataModel dataModel)
        {
            CustomResult<bool> data = new CustomResult<bool>();
            var user = this.userService.GetCurrentUser();
            var project = this.projectService.GetItemByKey(dataModel.ProjectId);
            var purchaseDoc = this.purchaseDocService.GetItemByKey(dataModel.PurchaseDocId);

            if (dataModel.InvDocStatus==InvDocStatus.Definitive)
            {
                data.Result = false;
                data.ErrorMessage = "سند مورد نظر قطعی شده و امکان تغییر آن وجود ندارد.";
                return Json(data);
            }
            // if purchaseDoc
            if (purchaseDoc != null)
            {
                if (purchaseDoc.IsFinished)
                {
                    data.Result = false;
                    data.ErrorMessage = "دستور خرید باز با شماره ذکر شده در سامانه یافت نشد.";
                    return Json(data);
                }
                else
                {
                    // Aggregated PurchaseDoc
                    if (purchaseDoc.IsAggregated)
                    {
                        if (dataModel.ProjectId != 0)
                        {
                            if (!project.IsCentralInventory)
                            {
                                data.Result = false;
                                data.ErrorMessage = "دستور خرید مذکور باید در انبار مرکزی گزارش ورود گردد.";
                                return Json(data);
                            }
                        }


                        //if user not have access
                        if (!user.Projects.Where(p => p.Id == purchaseDoc.ProjectId).Any())
                        {
                            data.Result = false;
                            data.ErrorMessage = "شما مجاز به ثبت گزارش ورود در انبار مرکزی نمی‌باشید.";
                            return Json(data);
                        }
                        dataModel.ProviderId = purchaseDoc.ProviderId;
                        dataModel.ProjectId = purchaseDoc.ProjectId;
                    }
                    else
                    {
                        if (purchaseDoc.ProjectId != dataModel.ProjectId)
                        {
                            data.Result = false;
                            data.ErrorMessage = "گزارش ورود مورد نظر برایر پروژه/انبار '" + project.Title + "' می‌باشد";
                            return Json(data);
                        }

                    }

                }
            }
            data = this.invDocService.SetEntranceDocData(dataModel);
            return Json(data);
        }
        #endregion Entrance Doc

        #region Internal Entrance Doc
        public ActionResult InternalEntranceDoc()
        {
            return View();
        }

        public ActionResult GetDataListInternalEntranceDoc(JqDataTable model)
        {
            var data = this.invDocService.GetDataList(2);

            if (model.iSortCol_0 == 1 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Id); }
            else if (model.iSortCol_0 == 1 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Id); }

            if (model.iSortCol_0 == 2 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.DocNo); }
            else if (model.iSortCol_0 == 2 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.DocNo); }

            if (model.iSortCol_0 == 3 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.ReferenceInternalDocNo); }
            else if (model.iSortCol_0 == 3 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.ReferenceInternalDocNo); }

            if (model.iSortCol_0 == 4 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.CarType); }
            else if (model.iSortCol_0 == 4 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.CarType); }

            if (model.iSortCol_0 == 5 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.CarPlateNumer); }
            else if (model.iSortCol_0 == 5 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.CarPlateNumer); }

            if (model.iSortCol_0 == 6 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.DriverName); }
            else if (model.iSortCol_0 == 6 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.DriverName); }

            if (model.iSortCol_0 == 7 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Source); }
            else if (model.iSortCol_0 == 7 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Source); }

            if (model.iSortCol_0 == 8 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Source); }
            else if (model.iSortCol_0 == 8 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Source); }

            if (model.iSortCol_0 == 9 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Operator); }
            else if (model.iSortCol_0 == 9 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Operator); }

            if (model.iSortCol_0 == 10 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.CreateTime); }
            else if (model.iSortCol_0 == 10 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.CreateTime); }


            if (!(string.IsNullOrEmpty(model.sSearch)) && !(string.IsNullOrWhiteSpace(model.sSearch)))
            {
                data = data.Where(x => x.Id.ToString().Contains(model.sSearch) || x.DocNo.Contains(model.sSearch) || x.ReferenceInternalDocNo.Contains(model.sSearch)
                || x.CarType.Contains(model.sSearch)
                || x.CarPlateNumer.Contains(model.sSearch) || x.DriverName.Contains(model.sSearch) || x.Source.Contains(model.sSearch)
                || x.Provider.Contains(model.sSearch) || x.Operator.Contains(model.sSearch) || x.CreateTime.ToString().Contains(model.sSearch));
            }

            if (!(string.IsNullOrEmpty(model.sSearch_1)) && !(string.IsNullOrWhiteSpace(model.sSearch_1)))
            {
                data = data.Where(x => x.Id.ToString().Contains(model.sSearch_1));
            }

            if (!(string.IsNullOrEmpty(model.sSearch_2)) && !(string.IsNullOrWhiteSpace(model.sSearch_2)))
            {
                data = data.Where(x => x.DocNo.Contains(model.sSearch_2));
            }

            if (!(string.IsNullOrEmpty(model.sSearch_3)) && !(string.IsNullOrWhiteSpace(model.sSearch_3)))
            {
                data = data.Where(x => x.ReferenceInternalDocNo.Contains(model.sSearch_3));
            }

            if (!(string.IsNullOrEmpty(model.sSearch_4)) && !(string.IsNullOrWhiteSpace(model.sSearch_4)))
            {
                data = data.Where(x => x.CarType.Contains(model.sSearch_4));
            }

            if (!(string.IsNullOrEmpty(model.sSearch_5)) && !(string.IsNullOrWhiteSpace(model.sSearch_5)))
            {
                data = data.Where(x => x.CarPlateNumer.ToString().Contains(model.sSearch_5));
            }

            if (!(string.IsNullOrEmpty(model.sSearch_6)) && !(string.IsNullOrWhiteSpace(model.sSearch_6)))
            {
                data = data.Where(x => x.DriverName.ToString().Contains(model.sSearch_6));
            }

            if (!(string.IsNullOrEmpty(model.sSearch_7)) && !(string.IsNullOrWhiteSpace(model.sSearch_7)))
            {
                data = data.Where(x => x.Source.Contains(model.sSearch_7));
            }

            if (!(string.IsNullOrEmpty(model.sSearch_8)) && !(string.IsNullOrWhiteSpace(model.sSearch_8)))
            {
                data = data.Where(x => x.Source.Contains(model.sSearch_8));
            }

            if (!(string.IsNullOrEmpty(model.sSearch_9)) && !(string.IsNullOrWhiteSpace(model.sSearch_9)))
            {
                data = data.Where(x => x.Operator.Contains(model.sSearch_9));
            }

            if (!(string.IsNullOrEmpty(model.sSearch_10)) && !(string.IsNullOrWhiteSpace(model.sSearch_10)))
            {
                data = data.Where(x => x.CreateTime.ToString().Contains(model.sSearch_10));
            }

            var count = data.Count();
            if (model.iDisplayLength == -1)
            {
                model.iDisplayLength = count;
            }
            data = data.Skip(model.iDisplayStart).Take(model.iDisplayLength).ToList();
            var result = new
            {
                iTotalRecords = data.Count(),
                iTotalDisplayRecords = count,
                aaData = data

            };

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetInternalEntranceDoc(long id)
        {
            var data = new InternalEntranceDocDataModel { InvDocTypeId = 2 };
            if (id > 0)
            {
                var invDoc = this.invDocService.GetItemByKey(id);
                try
                {
                    AutoMapper.Mapper.CreateMap<InvDoc, InternalEntranceDocDataModel>();
                    data = AutoMapper.Mapper.Map<InternalEntranceDocDataModel>(invDoc);
                }
                catch (Exception ex)
                {

                    //throw;
                    var reza = ex;
                }

                foreach (var item in invDoc.InvDocDetails.ToList())
                {
                    InvDocMatDataModel det = new InvDocMatDataModel
                    {
                        Id = item.Id,
                        MatId = item.MatId,
                        Title = item.Mat.Code + " - " + item.Mat.Title + " - " + item.Mat.TechnicalSpec + " - " + item.Mat.Dimention + " - واحدشمارش: " + item.Mat.MatUnit.Title,
                        ProjectDetailId = item.ProjectDetailId,
                        Amount = item.Amount,
                        RealAmount = item.RealAmount
                    };
                    data.Mats.Add(det);

                }
            }
            this.PlateCharacterIds();
            this.MaterialIds();
            this.ProjectIds();
            this.AllProjectIds();
            this.CarTypeIds();
            this.CarIds();
            this.ProjectDetailIds(null);
            ViewBag.InvDocTypeId = 2;

            return PartialView("CrudInternalEntranceDoc", data);
        }

        [HttpPost]
        public ActionResult SetInternalEntranceDoc(InternalEntranceDocDataModel dataModel)
        {

            var data = this.invDocService.SetInternalEntranceDocData(dataModel);

            return Json(data);
        }

        #endregion Internal Entrance Doc

        #region Use Doc
        public ActionResult UseDoc()
        {
            return View();
        }

        public ActionResult GetDataListUseDoc(JqDataTable model)
        {
            var data = this.invDocService.GetDataList(4);

            if (model.iSortCol_0 == 1 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Id); }
            else if (model.iSortCol_0 == 1 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Id); }

            if (model.iSortCol_0 == 2 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.DocNo); }
            else if (model.iSortCol_0 == 2 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.DocNo); }

            if (model.iSortCol_0 == 3 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Project); }
            else if (model.iSortCol_0 == 3 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Project); }

            if (model.iSortCol_0 == 4 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Reciver); }
            else if (model.iSortCol_0 == 4 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Reciver); }

            if (model.iSortCol_0 == 5 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Operator); }
            else if (model.iSortCol_0 == 5 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Operator); }

            if (model.iSortCol_0 == 6 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.CreateTime); }
            else if (model.iSortCol_0 == 6 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.CreateTime); }


            if (!(string.IsNullOrEmpty(model.sSearch)) && !(string.IsNullOrWhiteSpace(model.sSearch)))
            {
                data = data.Where(x => x.Id.ToString().Contains(model.sSearch) || x.DocNo.Contains(model.sSearch) || x.Project.Contains(model.sSearch)
                || x.Reciver.Contains(model.sSearch) || x.Operator.Contains(model.sSearch) || x.CreateTime.ToString().Contains(model.sSearch));
            }

            if (!(string.IsNullOrEmpty(model.sSearch_1)) && !(string.IsNullOrWhiteSpace(model.sSearch_1))) { data = data.Where(x => x.Id.ToString().Contains(model.sSearch_1)); }

            if (!(string.IsNullOrEmpty(model.sSearch_2)) && !(string.IsNullOrWhiteSpace(model.sSearch_2))) { data = data.Where(x => x.DocNo.Contains(model.sSearch_2)); }

            if (!(string.IsNullOrEmpty(model.sSearch_3)) && !(string.IsNullOrWhiteSpace(model.sSearch_3))) { data = data.Where(x => x.Project.Contains(model.sSearch_3)); }

            if (!(string.IsNullOrEmpty(model.sSearch_4)) && !(string.IsNullOrWhiteSpace(model.sSearch_4))) { data = data.Where(x => x.Reciver.Contains(model.sSearch_5)); }

            if (!(string.IsNullOrEmpty(model.sSearch_5)) && !(string.IsNullOrWhiteSpace(model.sSearch_5))) { data = data.Where(x => x.Operator.Contains(model.sSearch_5)); }

            if (!(string.IsNullOrEmpty(model.sSearch_3)) && !(string.IsNullOrWhiteSpace(model.sSearch_6))) { data = data.Where(x => x.CreateTime.ToString().Contains(model.sSearch_6)); }

            var count = data.Count();
            if (model.iDisplayLength == -1)
            {
                model.iDisplayLength = count;
            }
            data = data.Skip(model.iDisplayStart).Take(model.iDisplayLength).ToList();
            var result = new
            {
                iTotalRecords = data.Count(),
                iTotalDisplayRecords = count,
                aaData = data

            };

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetUseDoc(long id)
        {
            var data = new UseDocDataModel { InvDocTypeId = 4 };
            if (id > 0)
            {
                var invDoc = this.invDocService.GetItemByKey(id);
                AutoMapper.Mapper.CreateMap<InvDoc, UseDocDataModel>();
                data = AutoMapper.Mapper.Map<UseDocDataModel>(invDoc);
                foreach (var item in invDoc.InvDocDetails.ToList())
                {
                    InvDocMatDataModel det = new InvDocMatDataModel
                    {
                        Id = item.Id,
                        MatId = item.MatId,
                        Title = item.Mat.Title + " - " + item.Mat.TechnicalSpec + " - " + item.Mat.Dimention,
                        MatUnit=item.Mat.MatUnit.Title,
                        ProjectDetailId = item.ProjectDetailId,
                        ProjectDetailTitle = item.ProjectDetailId == null ? string.Empty : item.ProjectDetail.Title,
                        Amount = item.Amount,
                        RealAmount = item.RealAmount
                    };
                    data.Mats.Add(det);

                }
            }

            this.MaterialIds();
            this.ProjectIds();
            this.PeopleIds();
            this.ProjectDetailIds(null);
            ViewBag.InvDocTypeId = 4;

            return PartialView("CrudUseDoc", data);
        }

        [HttpPost]
        public ActionResult SetUseDoc(UseDocDataModel dataModel)
        {
            CustomResult<bool> data = new CustomResult<bool>();

            if (dataModel.InvDocStatus == InvDocStatus.Definitive)
            {
                data.Result = false;
                data.ErrorMessage = "سند مورد نظر قطعی شده و امکان تغییر آن وجود ندارد.";
                return Json(data);
            }

            var project = this.projectService.GetItemByKey(dataModel.ProjectId);

            if (project.IsCentralInventory)
            {
                data.Result = false;
                data.ErrorMessage = "برای انبار مرکزی نمی‌توان حواله مصرف ثبت نمود." + "\n" + "انبار مرکزی صرفا جهت دریافت و توزیع کالا می‌باشد.";
                return Json(data);
            }

            data = this.invDocService.SetUseDocData(dataModel);
            return Json(data);
        }

        #endregion Use Doc

        #region Return Doc
        public ActionResult ReturnDoc()
        {
            return View();
        }

        public ActionResult GetDataListReturnDoc(JqDataTable model)
        {
            var data = this.invDocService.GetDataList(5);

            if (model.iSortCol_0 == 1 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Id); }
            else if (model.iSortCol_0 == 1 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Id); }

            if (model.iSortCol_0 == 2 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.DocNo); }
            else if (model.iSortCol_0 == 2 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.DocNo); }

            if (model.iSortCol_0 == 3 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.DocNo); }
            else if (model.iSortCol_0 == 3 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Project); }

            if (model.iSortCol_0 == 4 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Reciver); }
            else if (model.iSortCol_0 == 4 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Reciver); }

            if (model.iSortCol_0 == 5 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Operator); }
            else if (model.iSortCol_0 == 5 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Operator); }

            if (model.iSortCol_0 == 6 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.CreateTime); }
            else if (model.iSortCol_0 == 6 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.CreateTime); }


            if (!(string.IsNullOrEmpty(model.sSearch)) && !(string.IsNullOrWhiteSpace(model.sSearch)))
            {
                data = data.Where(x => x.Id.ToString().Contains(model.sSearch) || x.DocNo.Contains(model.sSearch) || x.Project.Contains(model.sSearch)
                || x.Reciver.Contains(model.sSearch) || x.Operator.Contains(model.sSearch) || x.CreateTime.ToString().Contains(model.sSearch));
            }

            if (!(string.IsNullOrEmpty(model.sSearch_1)) && !(string.IsNullOrWhiteSpace(model.sSearch_1))) { data = data.Where(x => x.Id.ToString().Contains(model.sSearch_1)); }

            if (!(string.IsNullOrEmpty(model.sSearch_2)) && !(string.IsNullOrWhiteSpace(model.sSearch_2))) { data = data.Where(x => x.DocNo.Contains(model.sSearch_2)); }

            if (!(string.IsNullOrEmpty(model.sSearch_3)) && !(string.IsNullOrWhiteSpace(model.sSearch_3))) { data = data.Where(x => x.Project.Contains(model.sSearch_3)); }

            if (!(string.IsNullOrEmpty(model.sSearch_4)) && !(string.IsNullOrWhiteSpace(model.sSearch_4))) { data = data.Where(x => x.Reciver.Contains(model.sSearch_4)); }

            if (!(string.IsNullOrEmpty(model.sSearch_5)) && !(string.IsNullOrWhiteSpace(model.sSearch_5))) { data = data.Where(x => x.Operator.Contains(model.sSearch_5)); }

            if (!(string.IsNullOrEmpty(model.sSearch_6)) && !(string.IsNullOrWhiteSpace(model.sSearch_6))) { data = data.Where(x => x.CreateTime.ToString().Contains(model.sSearch_6)); }

            var count = data.Count();
            if (model.iDisplayLength == -1)
            {
                model.iDisplayLength = count;
            }
            data = data.Skip(model.iDisplayStart).Take(model.iDisplayLength).ToList();
            var result = new
            {
                iTotalRecords = data.Count(),
                iTotalDisplayRecords = count,
                aaData = data

            };

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetReturnDoc(long id)
        {
            var data = new ReturnDocDataModel { InvDocTypeId = 5 };
            if (id > 0)
            {
                var invDoc = this.invDocService.GetItemByKey(id);
                AutoMapper.Mapper.CreateMap<InvDoc, UseDocDataModel>();
                data = AutoMapper.Mapper.Map<ReturnDocDataModel>(invDoc);
                foreach (var item in invDoc.InvDocDetails.ToList())
                {
                    InvDocMatDataModel det = new InvDocMatDataModel
                    {
                        Id = item.Id,
                        MatId = item.MatId,
                        Title = item.Mat.Code + " - " + item.Mat.Title + " - " + item.Mat.TechnicalSpec + " - " + item.Mat.Dimention + " - واحدشمارش: " + item.Mat.MatUnit.Title,
                        ProjectDetailId = item.ProjectDetailId,
                        ProjectDetailTitle = item.ProjectDetailId == null ? string.Empty : item.ProjectDetail.Title,
                        Amount = item.Amount,
                        RealAmount = item.RealAmount
                    };
                    data.Mats.Add(det);

                }
            }

            this.MaterialIds();
            this.ProjectIds();
            this.PeopleIds();
            this.ProjectDetailIds(null);
            ViewBag.InvDocTypeId = 4;

            return PartialView("CrudReturnDoc", data);
        }

        [HttpPost]
        public ActionResult SetReturnDoc(ReturnDocDataModel dataModel)
        {
            CustomResult<bool> data = new CustomResult<bool>();
            if (dataModel.InvDocStatus == InvDocStatus.Definitive)
            {
                data.Result = false;
                data.ErrorMessage = "سند مورد نظر قطعی شده و امکان تغییر آن وجود ندارد.";
                return Json(data);
            }
            var project = this.projectService.GetItemByKey(dataModel.ProjectId);

            if (project.IsCentralInventory)
            {
                data.Result = false;
                data.ErrorMessage = "برای انبار مرکزی نمی‌توان حواله برگشت مصرف ثبت نمود." + "\n" + "انبار مرکزی صرفا جهت دریافت و توزیع کالا می‌باشد.";
                return Json(data);
            }
            data = this.invDocService.SetReturnDocData(dataModel);

            return Json(data);
        }

        #endregion Return Doc

        #region Internal BOL
        public ActionResult InternalBOLDoc()
        {
            return View();
        }
        public ActionResult GetDataListInternalBOLDoc(JqDataTable model)
        {
            var data = this.invDocService.GetDataList(6);

            if (model.iSortCol_0 == 1 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Id); }
            else if (model.iSortCol_0 == 1 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Id); }

            if (model.iSortCol_0 == 2 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.DocNo); }
            else if (model.iSortCol_0 == 2 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.DocNo); }

            if (model.iSortCol_0 == 3 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.PurchaseDocId); }
            else if (model.iSortCol_0 == 3 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.PurchaseDocId); }

            if (model.iSortCol_0 == 4 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Source); }
            else if (model.iSortCol_0 == 4 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Source); }

            if (model.iSortCol_0 == 5 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Dest); }
            else if (model.iSortCol_0 == 5 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Dest); }

            if (model.iSortCol_0 == 6 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.CarType); }
            else if (model.iSortCol_0 == 6 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.CarType); }

            if (model.iSortCol_0 == 7 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.CarPlateNumer); }
            else if (model.iSortCol_0 == 7 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.CarPlateNumer); }

            if (model.iSortCol_0 == 8 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.DriverName); }
            else if (model.iSortCol_0 == 8 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.DriverName); }

            if (model.iSortCol_0 == 9 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Source); }
            else if (model.iSortCol_0 == 9 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Source); }

            if (model.iSortCol_0 == 10 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Provider); }
            else if (model.iSortCol_0 == 10 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Provider); }

            if (model.iSortCol_0 == 11 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Operator); }
            else if (model.iSortCol_0 == 11 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Operator); }

            if (model.iSortCol_0 == 12 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.CreateTime); }
            else if (model.iSortCol_0 == 12 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.CreateTime); }


            if (!(string.IsNullOrEmpty(model.sSearch)) && !(string.IsNullOrWhiteSpace(model.sSearch)))
            {
                data = data.Where(x => x.Id.ToString().Contains(model.sSearch) || x.DocNo.Contains(model.sSearch) || x.CarType.Contains(model.sSearch)
                || x.CarPlateNumer.Contains(model.sSearch) || x.DriverName.Contains(model.sSearch) || x.Source.Contains(model.sSearch)
                || x.Provider.Contains(model.sSearch) || x.Operator.Contains(model.sSearch) || x.CreateTime.ToString().Contains(model.sSearch));
            }

            if (!(string.IsNullOrEmpty(model.sSearch_1)) && !(string.IsNullOrWhiteSpace(model.sSearch_1))) { data = data.Where(x => x.Id.ToString().Contains(model.sSearch_1)); }

            if (!(string.IsNullOrEmpty(model.sSearch_2)) && !(string.IsNullOrWhiteSpace(model.sSearch_2))) { data = data.Where(x => x.DocNo.Contains(model.sSearch_2)); }

            if (!(string.IsNullOrEmpty(model.sSearch_3)) && !(string.IsNullOrWhiteSpace(model.sSearch_3))) { data = data.Where(x => x.PurchaseDocId.ToString().Contains(model.sSearch_3)); }

            if (!(string.IsNullOrEmpty(model.sSearch_4)) && !(string.IsNullOrWhiteSpace(model.sSearch_4))) { data = data.Where(x => x.Source.Contains(model.sSearch_4)); }

            if (!(string.IsNullOrEmpty(model.sSearch_5)) && !(string.IsNullOrWhiteSpace(model.sSearch_5))) { data = data.Where(x => x.Dest.Contains(model.sSearch_5)); }

            if (!(string.IsNullOrEmpty(model.sSearch_6)) && !(string.IsNullOrWhiteSpace(model.sSearch_6))) { data = data.Where(x => x.CarType.Contains(model.sSearch_6)); }

            if (!(string.IsNullOrEmpty(model.sSearch_7)) && !(string.IsNullOrWhiteSpace(model.sSearch_7))) { data = data.Where(x => x.CarPlateNumer.ToString().Contains(model.sSearch_7)); }

            if (!(string.IsNullOrEmpty(model.sSearch_8)) && !(string.IsNullOrWhiteSpace(model.sSearch_8))) { data = data.Where(x => x.DriverName.ToString().Contains(model.sSearch_8)); }

            if (!(string.IsNullOrEmpty(model.sSearch_9)) && !(string.IsNullOrWhiteSpace(model.sSearch_9))) { data = data.Where(x => x.Source.Contains(model.sSearch_9)); }

            if (!(string.IsNullOrEmpty(model.sSearch_10)) && !(string.IsNullOrWhiteSpace(model.sSearch_10))) { data = data.Where(x => x.Provider.Contains(model.sSearch_10)); }

            if (!(string.IsNullOrEmpty(model.sSearch_11)) && !(string.IsNullOrWhiteSpace(model.sSearch_11))) { data = data.Where(x => x.Operator.Contains(model.sSearch_11)); }

            if (!(string.IsNullOrEmpty(model.sSearch_12)) && !(string.IsNullOrWhiteSpace(model.sSearch_12))) { data = data.Where(x => x.CreateTime.ToString().Contains(model.sSearch_12)); }

            var count = data.Count();
            if (model.iDisplayLength == -1)
            {
                model.iDisplayLength = count;
            }
            data = data.Skip(model.iDisplayStart).Take(model.iDisplayLength).ToList();
            var result = new
            {
                iTotalRecords = data.Count(),
                iTotalDisplayRecords = count,
                aaData = data

            };

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetInternalBOLDoc(long id)
        {
            var data = new InternalBOLDocDataModel { InvDocTypeId = 6 };
            if (id > 0)
            {
                var invDoc = this.invDocService.GetItemByKey(id);
                AutoMapper.Mapper.CreateMap<InvDoc, InternalBOLDocDataModel>();
                data = AutoMapper.Mapper.Map<InternalBOLDocDataModel>(invDoc);
                foreach (var item in invDoc.InvDocDetails.ToList())
                {
                    InvDocMatDataModel det = new InvDocMatDataModel
                    {
                        Id = item.Id,
                        MatId = item.MatId,
                        Title = item.Mat.Code + " - " + item.Mat.Title + " - " + item.Mat.TechnicalSpec + " - " + item.Mat.Dimention + " - واحدشمارش: " + item.Mat.MatUnit.Title,
                        ProjectDetailId = item.ProjectDetailId,
                        Amount = item.Amount,
                        RealAmount = item.RealAmount
                    };
                    data.Mats.Add(det);

                }
            }
            this.PlateCharacterIds();
            this.MaterialIds();
            this.ProjectIds();
            this.AllProjectIds();
            this.CarTypeIds();
            this.CarIds();
            this.AllProjectIds();
            this.PeopleIds();
            ViewBag.InvDocTypeId = 6;

            return PartialView("CrudInternalBOLDoc", data);
        }

        [HttpPost]
        public ActionResult SetInternalBOLDoc(InternalBOLDocDataModel dataModel)
        {
            CustomResult<bool> data = new CustomResult<bool>();
            if (dataModel.InvDocStatus == InvDocStatus.Definitive)
            {
                data.Result = false;
                data.ErrorMessage = "سند مورد نظر قطعی شده و امکان تغییر آن وجود ندارد.";
                return Json(data);
            }
            var SourceProject = this.projectService.GetItemByKey(dataModel.ProjectId);
            var destProject = this.projectService.GetItemByKey(dataModel.DestProjectId);

            if (dataModel.ProjectId == dataModel.DestProjectId)
            {
                data.Result = false;
                data.ErrorMessage = "مبداء و مقصد نمی‌تواند یکسان باشد.";
            }
            else
            {
                if (destProject.IsCentralInventory)
                {
                    data.Result = false;
                    data.ErrorMessage = "ثبت بارنامه داخلی به مقصد انبار مرکزی مجاز نمی‌باشد.";
                }

                else if (SourceProject.IsCentralInventory)
                {
                    if (dataModel.PurchaseDocId == null)
                    {
                        data.Result = false;
                        data.ErrorMessage = "جهت ثبت بارنامه داخلی از انبار مرکزی باید شماره دستور خرید را مشخص نمایید.";
                    }
                    else
                    {
                        var purchasedoc = this.purchaseDocService.GetItemByKey(dataModel.PurchaseDocId);
                        if (purchasedoc == null)
                        {
                            data.Result = false;
                            data.ErrorMessage = "دستور خرید با شماره '" + dataModel.PurchaseDocId.ToString() + "' در سامانه یافت نشد.";
                        }
                        else
                        {
                            data = this.invDocService.SetInternalBOLDocData(dataModel);
                        }
                    }
                }
                else
                {
                    data = this.invDocService.SetInternalBOLDocData(dataModel);
                }

            }

            return Json(data);
        }
        #endregion External BOL


        #region External BOL
        public ActionResult ExternalBOLDoc()
        {
            return View();
        }
        public ActionResult GetDataListExternalBOLDoc(JqDataTable model)
        {
            var data = this.invDocService.GetDataList(7);

            if (model.iSortCol_0 == 1 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Id); }
            else if (model.iSortCol_0 == 1 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Id); }

            if (model.iSortCol_0 == 2 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.DocNo); }
            else if (model.iSortCol_0 == 2 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.DocNo); }

            if (model.iSortCol_0 == 3 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.PurchaseDocId); }
            else if (model.iSortCol_0 == 3 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.PurchaseDocId); }


            if (model.iSortCol_0 == 4 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Project); }
            else if (model.iSortCol_0 == 4 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Project); }

            if (model.iSortCol_0 == 5 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Dest); }
            else if (model.iSortCol_0 == 5 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Dest); }


            if (model.iSortCol_0 == 6 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.CarType); }
            else if (model.iSortCol_0 == 6 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.CarType); }

            if (model.iSortCol_0 == 7 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.CarPlateNumer); }
            else if (model.iSortCol_0 == 7 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.CarPlateNumer); }

            if (model.iSortCol_0 == 8 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.DriverName); }
            else if (model.iSortCol_0 == 8 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.DriverName); }

            if (model.iSortCol_0 == 9 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Source); }
            else if (model.iSortCol_0 == 9 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Source); }

            if (model.iSortCol_0 == 10 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Provider); }
            else if (model.iSortCol_0 == 10 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Provider); }

            if (model.iSortCol_0 == 11 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.Operator); }
            else if (model.iSortCol_0 == 11 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.Operator); }

            if (model.iSortCol_0 == 12 && model.sSortDir_0 == "asc") { data = data.OrderBy(x => x.CreateTime); }
            else if (model.iSortCol_0 == 12 && model.sSortDir_0 == "desc") { data = data.OrderByDescending(x => x.CreateTime); }


            if (!(string.IsNullOrEmpty(model.sSearch)) && !(string.IsNullOrWhiteSpace(model.sSearch)))
            {
                data = data.Where(x => x.Id.ToString().Contains(model.sSearch) || x.DocNo.Contains(model.sSearch) || x.PurchaseDocId.ToString().Contains(model.sSearch) || x.CarType.Contains(model.sSearch)
                || x.Dest.Contains(model.sSearch) || x.Project.Contains(model.sSearch)
                || x.CarPlateNumer.Contains(model.sSearch) || x.DriverName.Contains(model.sSearch) || x.Source.Contains(model.sSearch)
                || x.Provider.Contains(model.sSearch) || x.Operator.Contains(model.sSearch) || x.CreateTime.ToString().Contains(model.sSearch));
            }

            if (!(string.IsNullOrEmpty(model.sSearch_1)) && !(string.IsNullOrWhiteSpace(model.sSearch_1))) { data = data.Where(x => x.Id.ToString().Contains(model.sSearch_1)); }

            if (!(string.IsNullOrEmpty(model.sSearch_2)) && !(string.IsNullOrWhiteSpace(model.sSearch_2))) { data = data.Where(x => x.DocNo.Contains(model.sSearch_2)); }

            if (!(string.IsNullOrEmpty(model.sSearch_3)) && !(string.IsNullOrWhiteSpace(model.sSearch_3))) { data = data.Where(x => x.PurchaseDocId.ToString().Contains(model.sSearch_3)); }

            if (!(string.IsNullOrEmpty(model.sSearch_4)) && !(string.IsNullOrWhiteSpace(model.sSearch_4))) { data = data.Where(x => x.Project.Contains(model.sSearch_4)); }

            if (!(string.IsNullOrEmpty(model.sSearch_5)) && !(string.IsNullOrWhiteSpace(model.sSearch_5))) { data = data.Where(x => x.Dest.Contains(model.sSearch_5)); }

            if (!(string.IsNullOrEmpty(model.sSearch_6)) && !(string.IsNullOrWhiteSpace(model.sSearch_6))) { data = data.Where(x => x.CarType.Contains(model.sSearch_6)); }

            if (!(string.IsNullOrEmpty(model.sSearch_7)) && !(string.IsNullOrWhiteSpace(model.sSearch_7))) { data = data.Where(x => x.CarPlateNumer.ToString().Contains(model.sSearch_7)); }

            if (!(string.IsNullOrEmpty(model.sSearch_8)) && !(string.IsNullOrWhiteSpace(model.sSearch_8))) { data = data.Where(x => x.DriverName.ToString().Contains(model.sSearch_8)); }

            if (!(string.IsNullOrEmpty(model.sSearch_9)) && !(string.IsNullOrWhiteSpace(model.sSearch_9))) { data = data.Where(x => x.Source.Contains(model.sSearch_9)); }

            if (!(string.IsNullOrEmpty(model.sSearch_10)) && !(string.IsNullOrWhiteSpace(model.sSearch_10))) { data = data.Where(x => x.Provider.Contains(model.sSearch_10)); }

            if (!(string.IsNullOrEmpty(model.sSearch_11)) && !(string.IsNullOrWhiteSpace(model.sSearch_11))) { data = data.Where(x => x.Operator.Contains(model.sSearch_11)); }

            if (!(string.IsNullOrEmpty(model.sSearch_12)) && !(string.IsNullOrWhiteSpace(model.sSearch_12))) { data = data.Where(x => x.CreateTime.ToString().Contains(model.sSearch_12)); }

            var count = data.Count();
            if (model.iDisplayLength == -1)
            {
                model.iDisplayLength = count;
            }
            data = data.Skip(model.iDisplayStart).Take(model.iDisplayLength).ToList();
            var result = new
            {
                iTotalRecords = data.Count(),
                iTotalDisplayRecords = count,
                aaData = data

            };

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetExternalBOLDoc(long id)
        {
            var data = new ExternalBOLDocDataModel { InvDocTypeId = 7 };
            if (id > 0)
            {
                var invDoc = this.invDocService.GetItemByKey(id);
                AutoMapper.Mapper.CreateMap<InvDoc, ExternalBOLDocDataModel>();
                data = AutoMapper.Mapper.Map<ExternalBOLDocDataModel>(invDoc);
                foreach (var item in invDoc.InvDocDetails.ToList())
                {
                    InvDocMatDataModel det = new InvDocMatDataModel
                    {
                        Id = item.Id,
                        MatId = item.MatId,
                        Title = item.Mat.Title + " - " + item.Mat.TechnicalSpec + " - " + item.Mat.Dimention,

                        MatUnit = item.Mat.MatUnit.Title,
                        Amount = item.Amount,
                        RealAmount = item.RealAmount
                    };
                    data.Mats.Add(det);

                }
            }
            this.PlateCharacterIds();
            this.MaterialIds();
            this.ProviderIds();
            this.ProjectIds();
            this.CarTypeIds();
            this.PeopleIds();
            this.CarIds();
            ViewBag.InvDocTypeId = 7;

            return PartialView("CrudExternalBOLDoc", data);
        }

        [HttpPost]
        public ActionResult SetExternalBOLDoc(ExternalBOLDocDataModel dataModel)
        {
            CustomResult<bool> data = new CustomResult<bool>();
            if (dataModel.InvDocStatus == InvDocStatus.Definitive)
            {
                data.Result = false;
                data.ErrorMessage = "سند مورد نظر قطعی شده و امکان تغییر آن وجود ندارد.";
                return Json(data);
            }
            var project = this.projectService.GetItemByKey(dataModel.ProjectId);

            if (dataModel.PurchaseDocId != null)
            {
                if (!project.IsCentralInventory)
                {
                    data.Result = false;
                    data.ErrorMessage = "شماره دستور خرید تنها برای انبارهای مرکزی می‌باشد";
                }
                else
                {
                    var purchaseDoc = this.purchaseDocService.GetItemByKey(dataModel.PurchaseDocId);
                    if (purchaseDoc == null)
                    {
                        data.Result = false;
                        data.ErrorMessage = "دستور خرید با شماره '" + dataModel.PurchaseDocId.ToString() + "' در سیستم یافت نشد.";
                    }
                    else
                    {
                        data = this.invDocService.SetExternalBOLDocData(dataModel);
                    }
                }

            }
            else
            {
                data = this.invDocService.SetExternalBOLDocData(dataModel);
            }
            return Json(data);
        }
        #endregion  External BOL


        public override JsonResult DeleteAjax(long id)
        {
            var doc = this.invDocService.GetItemByKey(id);
            var user = this.userService.GetCurrentUser();
            CustomResult<bool> data = new CustomResult<bool>();
            if (doc.InvDocStatus == Common.Enum.InvDocStatus.Definitive)
            {
                data.Result = false;
                data.ErrorMessage = "سند مورد نظر قطعی شده و امکان حذف وجود ندارد.";

                return Json(data);
            }


            return base.DeleteAjax(id);
        }

        [HttpPost]
        public JsonResult ConfirmInvDoc(long id)
        {
            CustomResult<bool> data = new CustomResult<bool>();
            var doc = this.invDocService.GetItemByKey(id);
            if (doc.InvDocStatus == Common.Enum.InvDocStatus.Definitive)
            {
                data.Result = false;
                data.ErrorMessage = "سند مورد نظر قبلا تائید شده و قطعی گردیده.";
                return Json(data);
            }
            else
            {
                if (doc.InvDocTypeId == 1 && doc.PurchaseDocId == null)
                {
                    data.Result = false;
                    data.ErrorMessage = "شماره دستور خرید مشخص نگردیده.";
                    return Json(data);
                }
                else if(doc.Project.IsCentralInventory)
                {
                    if ((doc.InvDocTypeId==6 || doc.InvDocTypeId == 7) && doc.PurchaseDocId==null) 
                    {
                        data.Result = false;
                        data.ErrorMessage = "شماره دستور خرید مشخص نگردیده.";
                        return Json(data);
                    }
                }
            }

            var purchDocValidation = this.invDocService.PurchaseDocValidation(id);
            if (purchDocValidation.Result == false)
            {
                return Json(purchDocValidation);
            }

            doc.InvDocStatus = Common.Enum.InvDocStatus.Definitive;
            doc.ConfirmUserId = this.userService.GetCurrentUser().Id;
            this.invDocService.Update(doc);
            data.Result = true;
            data.ErrorMessage = Resource.DataSavedSuccessfully;
            return Json(data);


        }

        public ActionResult InvDocAttachments(long id)
        {
            return View();
        }


        //public ActionResult GetInvDocAttachmentFile(long invDocId)
        public ActionResult GetInvDocAttachmentFile(long invDocId, string entityName)
        {
            InvDocAttachmentDataModel data = new InvDocAttachmentDataModel { InvDocId = invDocId, EntityName = entityName };
            ViewBag.EntityName = entityName;
            return PartialView("UploadFile", data);
        }

        [HttpPost]
        public ActionResult SetInvDocAttachmentFile(InvDocAttachmentDataModel dataModel, HttpPostedFileBase filePath)
        {
            ViewBag.EntityName = dataModel.EntityName;
            if (filePath == null)
            {
                ViewBag.Result = false;
                Response.StatusCode = 200;
                Response.StatusDescription = "NotOk";
                return PartialView("UploadFile", dataModel);
            }

            try
            {
                var uploadPath = Path.Combine(Server.MapPath("~/Uploads/InvDoc/" + dataModel.InvDocId + "/"));

                bool folderExits = DoesDirectoryExist(uploadPath);
                if (folderExits)
                {

                    string fileName = Guid.NewGuid().ToString().Replace("-", string.Empty);
                    string fileExtention = Path.GetExtension(filePath.FileName);

                    filePath.SaveAs(uploadPath + fileName + fileExtention);
                    InvDocAttachment idc = new InvDocAttachment { InvDocId = dataModel.InvDocId, FilePath = "/Uploads/InvDoc/" + dataModel.InvDocId + "/" + fileName + fileExtention, Title = dataModel.Title };
                    this.invDocAttachmentService.Add(idc);
                }

                ViewBag.Result = true;
                ViewBag.ErrorMessage = Resource.DataSavedSuccessfully;
                Response.StatusCode = 200;
                Response.StatusDescription = "Ok";
                return PartialView("UploadFile");
            }
            catch (Exception ex)
            {
                //throw;
                Response.StatusCode = 503;
                Response.StatusDescription = ex.Message;
                return PartialView("UploadFile", dataModel);
            }



        }




        public ActionResult GetInternalBOLDocData(string docNo, long projectId)
        {
            CustomResult<InternalBOLDocDataModel> data = new CustomResult<InternalBOLDocDataModel>();
            var invDoc = this.invDocService.GetInvDocByDocNo(docNo);
            if (invDoc == null)
            {
                data.Result = null;
                data.ErrorMessage = "بارنامه داخلی با این شماره در سامانه یافت نشد.";
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            if (invDoc.DestProjectId != projectId)
            {
                data.Result = null;
                data.ErrorMessage = "مقصد بارنامه مذکور با پروژه انتخاب شده مطابقت ندارد.";
                return Json(data, JsonRequestBehavior.AllowGet);
            }

            if (invDoc.InvDocStatus != InvDocStatus.Definitive)
            {
                data.Result = null;
                data.ErrorMessage = "بارنامه مورد نظر از طرف مبدا، قطعی نشده.";
                return Json(data, JsonRequestBehavior.AllowGet);
            }


            InternalBOLDocDataModel doc = new InternalBOLDocDataModel
            {
                Id = invDoc.Id,
                FinanceYear = invDoc.FinanceYear,
                ProjectId = invDoc.ProjectId,
                CarId = invDoc.CarId,
                DestProjectId = invDoc.DestProjectId.Value,
                DocNo = invDoc.DocNo,
                InvDocTypeId = invDoc.InvDocTypeId,
                CarTypeId = invDoc.CarTypeId,
                DriverName = invDoc.DriverName,
                DriverPhone = invDoc.DriverPhone,
                PlateSeries = invDoc.PlateSeries,
                PlateCharacterId = invDoc.PlateCharacterId,
                PlateNumberPart1 = invDoc.PlateNumberPart1,
                PlateNumberPart2 = invDoc.PlateNumberPart2,
                WeighbridgeNo = invDoc.WeighbridgeNo
            };

            foreach (var item in invDoc.InvDocDetails.ToList())
            {
                InvDocMatDataModel mat = new InvDocMatDataModel
                {
                    Id = item.Id,
                    MatId = item.MatId,
                    Title = item.Mat.Title + "-" + item.Mat.TechnicalSpec ?? string.Empty + "-" + item.Mat.Dimention ?? string.Empty + "-واحد شمارش : " + item.Mat.MatUnit.Title,
                    ProjectDetailId = item.ProjectDetailId,
                    ProjectDetailTitle = item.ProjectDetail == null ? null : item.ProjectDetail.Title,
                    Amount = item.Amount,
                    RealAmount = item.RealAmount,
                };
                doc.Mats.Add(mat);
            }

            data.Result = doc;
            data.ErrorMessage = Resource.DataFetchSuccessfully;

            return Json(data, JsonRequestBehavior.AllowGet);
        }



        private static bool DoesDirectoryExist(string folderPath)
        {

            bool exists = System.IO.Directory.Exists(folderPath);

            if (!exists)
            {
                try
                {
                    System.IO.Directory.CreateDirectory(folderPath);
                    exists = true;
                }
                catch (Exception ex)
                {
                    //throw;
                    exists = false;
                    var reza = ex;
                }
            }
            return exists;
        }



        private void CarTypeIds()
        {
            this.ViewBag.CarTypeIds = new SelectList(this.carTypeService.GetAllItems().Select(s => new { s.Id, s.Title }), "Id", "Title");
        }
        private void PlateCharacterIds()
        {
            this.ViewBag.PlateCharacterIds = new SelectList(this.plateCharacterService.GetAllItems().Select(s => new { s.Id, s.Title }), "Id", "Title");
        }

        private void InvDocTypeIds(long? id)
        {
            this.ViewBag.InvDocTypeIds = new SelectList(this.invDocTypeService.GetAllItems().Select(s => new { s.Id, s.Title }), "Id", "Title", id);
        }

        private void ProviderIds()
        {
            this.ViewBag.ProviderIds = new SelectList(this.providerService.GetAllItems().Select(s => new { s.Id, s.Title }), "Id", "Title");
        }

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

        private void AllProjectIds()
        {
            this.ViewBag.AllProjectIds = new SelectList(this.projectService.GetAllItems().Select(s => new { s.Id, s.Title }), "Id", "Title");
        }

        private void PeopleIds()
        {

            this.ViewBag.PeopleIds = new SelectList(this.peopleService.GetAllItems().Select(s => new { Id = s.Id, Title = s.FullName }), "Id", "Title");
        }

        private void CarIds()
        {

            this.ViewBag.CarIds = new SelectList(this.carService.GetAllItems().Select(s => new { Id = s.Id, Title = s.CarInfo }), "Id", "Title");
        }
    }
}