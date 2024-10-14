using AutoMapper;
using BSG.ADInventory.Data;
using BSG.ADInventory.Entities;
using BSG.ADInventory.Models.DataTable;
using BSG.ADInventory.Models.Township;
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
    public class TownshipController : CrudController<Township>
    {
        #region ctor
        private IUnitOfWork _unitOfWork;
        private readonly IUserRepository userRepository;        
        private readonly ITownshipService _townshipService;        
        private readonly IProvinceService _provinceService;
        private readonly IMapper _mapper;

        public TownshipController(IUnitOfWork unitOfWork, ITownshipService townshipService, IProvinceService provinceService, IMapper mapper)
            : base(townshipService)
        {
            _unitOfWork = unitOfWork;
            _provinceService = provinceService;
            _mapper = mapper;
            _townshipService = townshipService;
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
            return View();        }


        /// <summary>
        /// Loads the factories.
        /// </summary>

        private void ProvinceIds()
        {
            this.ViewBag.ProvinceIds = new SelectList(this._provinceService.GetAllItems().Select(s => new { s.Id, s.Title }), "Id", "Title");
        }

        public ActionResult GetDataTableFilteredList(JqDataTable filter)
        {
            var query = this._townshipService.GetAllItems().OrderByDescending(t=>t.Id);
            var mappedQuery = _mapper.Map<List<TownshipListDTO>>(query);
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
            Township township = new Township();
            if (id != 0)
            {
                township = this._townshipService.GetItemByKey(id);
            }
            var data = _mapper.Map<TownshipCEDTO>(township);
            this.ProvinceIds();
            return PartialView("CrudAction", data);

        }

        [HttpPost]
        public ActionResult SetCrudAction(TownshipCEDTO dataModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (dataModel.Id > 0)
                    {
                        var township = _townshipService.GetItemByKey(dataModel.Id);
                        _mapper.Map(dataModel, township);
                        this._townshipService.Update(township);
                    }
                    else
                    {
                        var township = _mapper.Map<Township>(dataModel);
                        this._townshipService.Add(township);
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
                this.ProvinceIds();
                return PartialView("CreateOrEdit", dataModel);
            }

        }

    }
}