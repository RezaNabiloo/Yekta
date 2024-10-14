using BSG.ADInventory.Entities;
using BSG.ADInventory.Models.DataTable;
using BSG.ADInventory.Resources;
using BSG.ADInventory.Services;
using System;
using System.Linq;
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
    [Permission(PermissionIds = "SystemManagement,BaseDataManagement,InvYearCycleManagement", LoginUrl = "~/Error/Index/403")]
    public class InvYearCycleController : CrudController<InvYearCycle>
    {
        #region ctor
        private IUnitOfWork unitOfWork;
        private readonly IInvYearCycleService invYearCycleService;
        private readonly ICalendarYearService calendarYearService;
        private readonly IUserService userService;
        private readonly IMatService matService;
        private readonly IProjectService projectService;

        public InvYearCycleController(IUnitOfWork unitOfWork, IInvYearCycleService invYearCycleService, ICalendarYearService calendarYearService,
            IUserService userService, IMatService matService, IProjectService projectService)
            : base(invYearCycleService)
        {
            this.unitOfWork = unitOfWork;
            this.invYearCycleService = invYearCycleService;
            this.calendarYearService = calendarYearService;
            this.userService = userService;
            this.matService = matService;
            this.projectService = projectService;
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
            ViewBag.EntityName = RouteData.Values["controller"];
            return this.View();
        }



        public ActionResult GetDataList(JqDataTable model)
        {

            var data = this.invYearCycleService.GetDataList();

            if (model.iSortCol_0 == 1 && model.sSortDir_0 == "asc")
            {
                data = data.OrderBy(x => x.Id);
            }
            else if (model.iSortCol_0 == 1 && model.sSortDir_0 == "desc")
            {
                data = data.OrderByDescending(x => x.Id);
            }


            if (model.iSortCol_0 == 2 && model.sSortDir_0 == "asc")
            {
                data = data.OrderBy(x => x.Year);
            }
            else if (model.iSortCol_0 == 2 && model.sSortDir_0 == "desc")
            {
                data = data.OrderByDescending(x => x.Year);
            }

            if (model.iSortCol_0 == 3 && model.sSortDir_0 == "asc")
            {
                data = data.OrderBy(x => x.Project);
            }
            else if (model.iSortCol_0 == 3 && model.sSortDir_0 == "desc")
            {
                data = data.OrderByDescending(x => x.Project);
            }

            if (model.iSortCol_0 == 4 && model.sSortDir_0 == "asc")
            {
                data = data.OrderBy(x => x.MatId);
            }
            else if (model.iSortCol_0 == 4 && model.sSortDir_0 == "desc")
            {
                data = data.OrderByDescending(x => x.MatId);
            }

            if (model.iSortCol_0 == 5 && model.sSortDir_0 == "asc")
            {
                data = data.OrderBy(x => x.MatTitle);
            }
            else if (model.iSortCol_0 == 5 && model.sSortDir_0 == "desc")
            {
                data = data.OrderByDescending(x => x.MatTitle);
            }

            if (model.iSortCol_0 == 6 && model.sSortDir_0 == "asc")
            {
                data = data.OrderBy(x => x.ConfirmedCountQty);
            }
            else if (model.iSortCol_0 == 6 && model.sSortDir_0 == "desc")
            {
                data = data.OrderByDescending(x => x.ConfirmedCountQty);
            }



            //Global Search functionality 

            if (!(string.IsNullOrEmpty(model.sSearch)) && !(string.IsNullOrWhiteSpace(model.sSearch)))
            {
                data = data.Where(x => x.Id.ToString().Contains(model.sSearch) || x.Year.Contains(model.sSearch) || x.Project.Contains(model.sSearch)
                || x.MatTitle.Contains(model.sSearch) || x.MatId.Contains(model.sSearch) || x.ConfirmedCountQty.Contains(model.sSearch));
            }



            //Id Column Filtering
            if (!(string.IsNullOrEmpty(model.sSearch_1)) && !(string.IsNullOrWhiteSpace(model.sSearch_1)))
            {
                data = data.Where(x => x.Id.ToString().Contains(model.sSearch_1));
            }
            //Title Column Filtering
            if (!(string.IsNullOrEmpty(model.sSearch_2)) && !(string.IsNullOrWhiteSpace(model.sSearch_2)))
            {
                data = data.Where(x => x.Year.Contains(model.sSearch_2));
            }

            if (!(string.IsNullOrEmpty(model.sSearch_3)) && !(string.IsNullOrWhiteSpace(model.sSearch_3)))
            {
                data = data.Where(x => x.Project.Contains(model.sSearch_3));
            }

            if (!(string.IsNullOrEmpty(model.sSearch_4)) && !(string.IsNullOrWhiteSpace(model.sSearch_4)))
            {
                data = data.Where(x => x.MatId.Contains(model.sSearch_4));
            }
            //Is Capital Column Filtering
            if (!(string.IsNullOrEmpty(model.sSearch_5)) && !(string.IsNullOrWhiteSpace(model.sSearch_5)))
            {
                data = data.Where(x => x.MatTitle.Contains(model.sSearch_5));
            }
            //Lat Column Filtering
            if (!(string.IsNullOrEmpty(model.sSearch_6)) && !(string.IsNullOrWhiteSpace(model.sSearch_6)))
            {
                data = data.Where(x => x.ConfirmedCountQty.Contains(model.sSearch_6));
            }


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




        public ActionResult CrudAction(long id, long matId)
        {
            var reza = this.userService.GetCurrentUser().Projects.FirstOrDefault();

            long? userProject = this.userService.GetCurrentUser().Projects.FirstOrDefault()?.Id;
            ViewBag.Id = id;
            CalendarYearIds();
            ProjectIds();
            MaterialIds(matId);

            ViewBag.DefaultProject = this.userService.GetCurrentUser().Projects.FirstOrDefault()?.Id;
            InvYearCycle data = new InvYearCycle { CalendarYearId = 1, MatId = matId, ProjectId=userProject?? 0};
            if (id != 0)
            {
                data = this.invYearCycleService.GetItemByKey(id);
            }
            return PartialView("CrudAction", data);
        }

        [HttpPost]
        public ActionResult SetCrudAction(InvYearCycle dataModel)
        {
            ViewBag.DefaultProject = this.userService.GetCurrentUser().Projects.FirstOrDefault()?.Id;
            CalendarYearIds();
            ProjectIds();
            MaterialIds(dataModel.MatId);

            var duplicate = this.invYearCycleService.GetAllItems().Where(i => i.ProjectId == dataModel.ProjectId && i.MatId == dataModel.MatId && i.CalendarYearId == dataModel.CalendarYearId).FirstOrDefault();
            if (duplicate!=null)
            {
                ModelState.AddModelError("MatId", Resource.DuplicateData);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (dataModel.Id > 0)
                    {
                        this.invYearCycleService.Update(dataModel);
                    }
                    else
                    {
                        this.invYearCycleService.Add(dataModel);
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

        private void ProjectIds()
        {
            this.ViewBag.ProjectIds = new SelectList(this.projectService.GetAllItems().Select(s => new { s.Id, s.Title }), "Id", "Title");
        }

        private void MaterialIds(long matId)
        {
            var mats = this.matService.GetDataList();
            this.ViewBag.Materials = mats;
            this.ViewBag.MaterialIds = new SelectList(mats.Select(s => new { Id = s.Id, Title = s.Id + " - " + s.Title + " " + s.TechnicalSpec + " " + s.Dimention + " | واحدشمارش: " + s.MatUnit }), "Id", "Title", matId);
        }

        private void CalendarYearIds()
        {
            this.ViewBag.CalendarYearIds = new SelectList(this.calendarYearService.GetAllItems().Select(s => new { s.Id, s.YearNo }), "Id", "YearNo", 1);
        }

    }
}