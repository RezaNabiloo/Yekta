using AutoMapper;
using BSG.ADInventory.Entities;
using BSG.ADInventory.Models.MatUnit;
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
    public class MatUnitController : CrudController<MatUnit>
    {
        #region ctor
        private IUnitOfWork _unitOfWork;
        private readonly IMatUnitService _matUnitService;
        private readonly IMapper _mapper;

        public MatUnitController(IUnitOfWork unitOfWork, IMatUnitService matUnitService, IMapper mapper)
            : base(matUnitService)
        {
            _unitOfWork = unitOfWork;
            _matUnitService = matUnitService;
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
            var query = this._matUnitService.GetAllItems();
            var mappedQuery = _mapper.Map<List<MatUnitListDTO>>(query);
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
            MatUnit matUnit = new MatUnit();
            if (id != 0)
            {
                matUnit = this._matUnitService.GetItemByKey(id);
            }
            var data = _mapper.Map<MatUnitCEDTO>(matUnit);
            return PartialView("CrudAction", data);
        }

        [HttpPost]
        public ActionResult SetCrudAction(MatUnitCEDTO dataModel)
        {
            if (ModelState.IsValid)
            {
                try
                {                    
                    if (dataModel.Id > 0)
                    {
                        var matUnit = this._matUnitService.GetItemByKey(dataModel.Id);
                        _mapper.Map(dataModel, matUnit);                        
                        this._matUnitService.Update(matUnit);
                    }
                    else
                    {
                        var matUnit = _mapper.Map<MatUnit>(dataModel);
                        this._matUnitService.Add(matUnit);
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