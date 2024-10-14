using AutoMapper;
using BSG.ADInventory.Entities;
using BSG.ADInventory.Models.Country;
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
    public class CountryController : CrudController<Country>
    {
        #region ctor
        private IUnitOfWork _unitOfWork;
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;

        public CountryController(IUnitOfWork unitOfWork, ICountryService countryService, IMapper mapper)
            : base(countryService)
        {
            _unitOfWork = unitOfWork;
            _countryService = countryService;
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
            var query = this._countryService.GetAllItems();
            var mappedQuery = _mapper.Map<List<CountryListDTO>>(query);
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
            Country country = new Country();
            if (id != 0)
            {
                country = this._countryService.GetItemByKey(id);
            }
            var data = _mapper.Map<CountryCEDTO>(country);
            return PartialView("CrudAction", data);
        }

        [HttpPost]
        public ActionResult SetCrudAction(CountryCEDTO dataModel)
        {
            if (ModelState.IsValid)
            {
                try
                {                    
                    if (dataModel.Id > 0)
                    {
                        var country = this._countryService.GetItemByKey(dataModel.Id);
                        _mapper.Map(dataModel, country);                        
                        this._countryService.Update(country);
                    }
                    else
                    {
                        var country = _mapper.Map<Country>(dataModel);
                        this._countryService.Add(country);
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