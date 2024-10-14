using AutoMapper;
using BSG.ADInventory.Entities;
using BSG.ADInventory.Models.CarType;
using BSG.ADInventory.Models.DataTable;
using BSG.ADInventory.Resources;
using BSG.ADInventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebMarkupMin.AspNet4.Mvc;
using Zcf.Data;
using Zcf.Paging;
using Zcf.Web.Mvc.Controllers;
using Zcf.Web.Mvc.Security;

namespace BSG.ADInventory.WebUI.Controllers
{
    [Authorize]
    [Permission(PermissionIds = "SystemAdministrator", LoginUrl = "~/Error/Index/403")]
    [MinifyHtml]
    public class CarTypeController : CrudController<CarType>
    {
        #region ctor
        private IUnitOfWork _unitOfWork;
        private readonly ICarTypeService _carTypeService;
        private readonly IMapper _mapper;

        public CarTypeController(IUnitOfWork unitOfWork, ICarTypeService carTypeService, IMapper mapper)
            : base(carTypeService)
        {
            _unitOfWork = unitOfWork;
            _carTypeService = carTypeService;
            _mapper = mapper;
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


        public ActionResult GetDataTableFilteredList(JqDataTable filter)
        {
            var query = this._carTypeService.GetAllItems().OrderByDescending(c=>c.Id);
            var mappedQuery = _mapper.Map<List<CarTypeListDTO>>(query);
            var filteredData = DataTableHelper.FilterAndSort(mappedQuery, filter);
            var data = filteredData.Skip(filter.iDisplayStart).Take(filter.iDisplayLength).ToList();

            var result = new
            {
                iTotalRecords = filteredData.Count(),//records per page 
                iTotalDisplayRecords = filteredData.Count(), //total table count
                aaData = data //data list
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CrudAction(long id)
        {
            ViewBag.Id = id;
            CarType carType = new CarType();
            if (id != 0)
            {
                carType = this._carTypeService.GetItemByKey(id);
            }
            var data = _mapper.Map<CarTypeCEDTO>(carType);
            return PartialView("CrudAction", data);
        }

        [HttpPost]
        public ActionResult SetCrudAction(CarTypeCEDTO dataModel)
        {
            if (ModelState.IsValid)
            {
                try
                {                    
                    if (dataModel.Id > 0)
                    {
                        var carType = this._carTypeService.GetItemByKey(dataModel.Id);
                        _mapper.Map(dataModel, carType);                        
                        this._carTypeService.Update(carType);
                    }
                    else
                    {
                        var carType = _mapper.Map<CarType>(dataModel);
                        this._carTypeService.Add(carType);
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