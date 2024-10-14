using AutoMapper;
using BSG.ADInventory.Data;
using BSG.ADInventory.Entities;
using BSG.ADInventory.Models.Brand;
using BSG.ADInventory.Models.DataTable;
using BSG.ADInventory.Models.Provider;
using BSG.ADInventory.Models.Township;
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
    [Permission(PermissionIds = "SystemManagement,BaseDataManagement,ProviderManagement", LoginUrl = "~/Error/Index/403")]
    public class ProviderController : CrudController<Provider>
    {
        #region ctor
        private IUnitOfWork unitOfWork;        
        private readonly IProviderService _providerService;
        private readonly IProvinceService _provinceService;
        private readonly ITownshipService _townshipService;
        private readonly IMapper _mapper;

        public ProviderController(IUnitOfWork unitOfWork, IProviderService providerService, IProvinceService provinceService, ITownshipService townshipService,
            IMapper mapper)
            : base(providerService)
        {
            this.unitOfWork = unitOfWork;
            _provinceService = provinceService;
            _providerService = providerService;
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
            return this.View();
        }

        public ActionResult GetDataTableFilteredList(JqDataTable filter)
        {
            var query = this._providerService.GetAllItems();            
            var filteredData = DataTableHelper.FilterAndSort(query, filter);
            var data = filteredData.Skip(filter.iDisplayStart).Take(filter.iDisplayLength).ToList();

            var result = new
            {
                iTotalRecords = filteredData.Count(),//records per page 
                iTotalDisplayRecords = filteredData.Count(), //total table count
                aaData = data //data list
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult CrudAction(long id)
        //{

        //    ViewBag.Id = id;
        //    Township township = new Township();
        //    if (id != 0)
        //    {
        //        township = this._townshipService.GetItemByKey(id);
        //    }
        //    var data = _mapper.Map<TownshipCEDTO>(township);
        //    this.ProvinceIds();
        //    return PartialView("CrudAction", data);

        //}

        //[HttpPost]
        //public ActionResult SetCrudAction(TownshipCEDTO dataModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            if (dataModel.Id > 0)
        //            {
        //                var township = _townshipService.GetItemByKey(dataModel.Id);
        //                _mapper.Map(dataModel, township);
        //                this._townshipService.Update(township);
        //            }
        //            else
        //            {
        //                var township = _mapper.Map<Township>(dataModel);
        //                this._townshipService.Add(township);
        //            }
        //            ModelState.Clear();
        //            ViewBag.Result = true;
        //            ViewBag.ErrorMessage = Resource.DataAddSuccessfully;
        //        }
        //        catch (Exception ex)
        //        {
        //            ViewBag.Result = false;
        //            ViewBag.ErrorMessage = ex.Message;

        //        }

        //        return PartialView("CreateOrEdit");
        //    }
        //    else
        //    {
        //        this.ProvinceIds();
        //        return PartialView("CreateOrEdit", dataModel);
        //    }

        //}



        //private void ProvinceIds(long? id)
        //{

        //    this.ViewBag.ProvinceIds = new SelectList(this.provinceService.GetAllItems().Select(s => new { s.Id, s.Title }), "Id", "Title", id);
        //}
        //private void TownshipIds(long? provinceId, long? townshipId)
        //{
        //    this.ViewBag.TownshipIds = new SelectList(this.townshipService.GetAllItems().Where(t => t.ProvinceId == (provinceId ?? 0)).Select(s => new { s.Id, s.Title }), "Id", "Title", townshipId);
        //}

    }
}