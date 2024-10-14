using BSG.ADInventory.Entities;
using BSG.ADInventory.Models.DataTable;
using BSG.ADInventory.Resources;
using BSG.ADInventory.Services;
using System;
using System.Linq;
using System.Web.Mvc;
using WebMarkupMin.AspNet4.Mvc;
using Zcf.Data;
using Zcf.Paging;
using Zcf.Web.Mvc.Controllers;
using Zcf.Web.Mvc.Security;

namespace BSG.ADInventory.WebUI.Controllers
{
    [MinifyHtml]
    [Authorize]
    [Permission(PermissionIds = "SystemManagement,BaseDataManagement,GeoLocations", LoginUrl = "~/Error/Index/403")]
    public class CalendarYearController : CrudController<CalendarYear>
    {
        #region ctor
        private IUnitOfWork unitOfWork;        
        private readonly ICalendarYearService calendarYearService;        
        

        public CalendarYearController(IUnitOfWork unitOfWork, ICalendarYearService calendarYearService)
            : base(calendarYearService)
        {
            this.unitOfWork = unitOfWork;
            this.calendarYearService = calendarYearService;
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


        public ActionResult GetDataList(JqDataTable model)
        {

            var data = this.calendarYearService.GetDataList();


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
                data = data.OrderBy(x => x.YearNo);
            }
            else if (model.iSortCol_0 == 2 && model.sSortDir_0 == "desc")
            {
                data = data.OrderByDescending(x => x.YearNo);
            }

            if (model.iSortCol_0 == 3 && model.sSortDir_0 == "asc")
            {
                data = data.OrderBy(x => x.Description);
            }
            else if (model.iSortCol_0 == 2 && model.sSortDir_0 == "desc")
            {
                data = data.OrderByDescending(x => x.Description);
            }


            //Global Search functionality 

            if (!(string.IsNullOrEmpty(model.sSearch)) && !(string.IsNullOrWhiteSpace(model.sSearch)))
            {
                data = data.Where(x => x.Id.ToString().Contains(model.sSearch) || x.YearNo.ToString().Contains(model.sSearch) || x.Description.Contains(model.sSearch));
            }           

            
            if (!(string.IsNullOrEmpty(model.sSearch_1)) && !(string.IsNullOrWhiteSpace(model.sSearch_1)))
         
            {
                data = data.Where(x => x.Id.ToString().Contains(model.sSearch_1));
            }
            
            if (!(string.IsNullOrEmpty(model.sSearch_2)) && !(string.IsNullOrWhiteSpace(model.sSearch_2)))
            {
                data = data.Where(x => x.YearNo.ToString().Contains(model.sSearch_2));
            }
            if (!(string.IsNullOrEmpty(model.sSearch_3)) && !(string.IsNullOrWhiteSpace(model.sSearch_3)))
            {
                data = data.Where(x => x.Description.Contains(model.sSearch_3));
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

        public ActionResult CrudAction(long id)
        {
            ViewBag.Id = id;
            CalendarYear data = new CalendarYear();
            if (id != 0)
            {
                data = this.calendarYearService.GetItemByKey(id);
            }
            
            return PartialView("CrudAction", data);
        }

        [HttpPost]
        public ActionResult SetCrudAction(CalendarYear dataModel)
        {
            //ModelState.AddModelError("Title","reza nabiloo");
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (dataModel.Id > 0)
                    {
                        this.calendarYearService.Update(dataModel);
                    }
                    else
                    {
                        this.calendarYearService.Add(dataModel);
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