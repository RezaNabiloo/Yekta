using AutoMapper;
using BSG.ADInventory.Data;
using BSG.ADInventory.Entities;
using BSG.ADInventory.Models.DataTable;
using BSG.ADInventory.Models.Province;
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
    public class ProvinceController : CrudController<Province>
    {
        #region ctor
        private IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ICountryService _countryService;
        private readonly IProvinceService _provinceService;
        private readonly ITownshipService _townshipService;
        private readonly IMapper _mapper;

        public ProvinceController(IUnitOfWork unitOfWork, ICountryService countryService, IUserRepository userRepository, IProvinceService provinceService, 
            ITownshipService townshipService, IMapper mapper)
            : base(provinceService)
        {
            _unitOfWork = unitOfWork;
            _countryService = countryService;
            _userRepository = userRepository;
            _provinceService = provinceService;
            _townshipService = townshipService;
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

            ViewBag.EntityName = RouteData.Values["controller"];
            return View();

        }

        private void CountryIds()
        {
            this.ViewBag.CountryIds = new SelectList(this._countryService.GetAllItems().Select(s => new { s.Id, s.Title }), "Id", "Title");
        }

        [HttpPost]
        public ActionResult GetTownships(long id)
        {
            SelectList Townships = new SelectList(this._provinceService.GetTownships(id), "Id", "Title", null);
            return Json(Townships);

        }

        public ActionResult GetDataTableFilteredList(JqDataTable filter)
        {
            var query = this._provinceService.GetAllItems();
            var mappedQuery = _mapper.Map<List<ProvinceListDTO>>(query);
            var filteredData = DataTableHelper.FilterAndSort(mappedQuery, filter);
            var data= filteredData.Skip(filter.iDisplayStart).Take(filter.iDisplayLength).ToList();

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
            Province province = new Province();
            if (id != 0)
            {
                province = this._provinceService.GetItemByKey(id);
            }
            var data = _mapper.Map<ProvinceCEDTO>(province);
            this.CountryIds();
            return PartialView("CrudAction", data);
        }

        [HttpPost]
        public ActionResult SetCrudAction(ProvinceCEDTO dataModel)
        {
            // ModelState.AddModelError("Title", "Reza Nabiloo");

            if (ModelState.IsValid)
            {
                try
                {                    
                    if (dataModel.Id > 0)
                    {
                        var province = _provinceService.GetItemByKey(dataModel.Id);
                            _mapper.Map(dataModel,province);
                        this._provinceService.Update(province);
                    }
                    else
                    {
                        var province = _mapper.Map<Province>(dataModel);
                        this._provinceService.Add(province);
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
                this.CountryIds();
                return PartialView("CreateOrEdit", dataModel);
            }

        }

    }
}