using AutoMapper;
using BSG.ADInventory.Common.Classes;
using BSG.ADInventory.Entities;
using BSG.ADInventory.Models.DataTable;
using BSG.ADInventory.Models.People;
using BSG.ADInventory.Resources;
using BSG.ADInventory.Services;
using BSG.ADInventory.WebUI.Helpers;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMarkupMin.AspNet4.Mvc;
using Zcf.Data;
using Zcf.Paging;
using Zcf.Web.Mvc.Controllers;

namespace BSG.ADInventory.WebUI.Controllers
{
    [MinifyHtml]
    [Authorize]
    //[Permission(PermissionIds = "SystemManagement,BaseDataManagement,EmployeeManagement,OtherIOPeopleManagement,BlackListManagement,PeopleManagement", LoginUrl = "~/Error/Index/403")]
    public class PeopleController : CrudController<People>
    {
        #region ctor
        private IUnitOfWork _unitOfWork;
        private readonly IPeopleService _peopleService;
        private readonly ICountryService _countryService;
        private readonly IProvinceService _provinceService;
        private readonly ITownshipService _townshipService;
        private readonly ICarService _carService;
        private readonly IMapper _mapper;

        public PeopleController(IUnitOfWork unitOfWork, IPeopleService peopleService,
            ICountryService countryService,
            IProvinceService provinceService,
            ITownshipService townshipService,
            ICarService carService,
            IMapper mapper)
            : base(peopleService)
        {
            _unitOfWork = unitOfWork;
            _peopleService = peopleService;
            _countryService = countryService;
            _provinceService = provinceService;
            _townshipService = townshipService;
            _carService = carService;
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
        /// 

        public ActionResult Index()
        {
            return this.View();
        }


        public ActionResult GetDataTableFilteredList(JqDataTable filter)
        {
            var query = this._peopleService.GetAllItems().OrderByDescending(t => t.Id);
            var mappedQuery = _mapper.Map<List<PeopleListDTO>>(query);
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
            #region Get Picture size            
            PictureSize size = DataHelper.GetElementSettingByKey("PeopleImageSize") as PictureSize;
            ViewBag.ImageSize = size.Width.ToString() + "W" + " × " + size.Height.ToString() + "H";


            PictureSize signatureSize = DataHelper.GetElementSettingByKey("SignatureImageSize") as PictureSize;
            ViewBag.SignatureSize = size.Width.ToString() + "W" + " × " + size.Height.ToString() + "H";
            #endregion


            ViewBag.Id = id;
            //this.ProductGroupIds();
            //this.BrandIds();
            //this.VariantOptionIds();

            People people = new People();
            if (id != 0)
            {
                people = this._peopleService.GetItemByKey(id);
            }
            var data = _mapper.Map<PeopleCEDTO>(people);
            return PartialView("CrudAction", data);

        }

        [HttpPost]
        public ActionResult SetCrudAction(PeopleCEDTO dataModel, HttpPostedFileBase imageUrl, HttpPostedFileBase signatureImageUrl)
        {
            //this.ProductGroupIds();
            //this.BrandIds();
            //this.VariantOptionIds();

            #region Get Picture size            
            PictureSize imageSize = DataHelper.GetElementSettingByKey("PeopleImageSize") as PictureSize;
            ViewBag.ImageSize = imageSize.Width.ToString() + "W" + " × " + imageSize.Height.ToString() + "H";


            PictureSize signatureSize = DataHelper.GetElementSettingByKey("SignatureImageSize") as PictureSize;
            ViewBag.SignatureSize = signatureSize.Width.ToString() + "W" + " × " + signatureSize.Height.ToString() + "H";
            #endregion

            if (ModelState.IsValid)
            {
                try
                {
                    var oldImagePath = "";
                    var savedImagePath = "";
                    var savedSignatureImagePath = "";
                    var oldSignatureImagePath = "";

                    if (imageUrl != null)
                    {
                        savedImagePath = Common.ImageHelper.SavePicture(this.HttpContext, imageUrl, imageSize, "/Uploads/PeoplePic/");
                    }
                    if (signatureImageUrl != null)
                    {
                        savedSignatureImagePath = Common.ImageHelper.SavePicture(this.HttpContext, signatureImageUrl, signatureSize, "/Uploads/PeopleSignature/");
                    }

                    if (dataModel.Id > 0)
                    {
                        var people = _peopleService.GetItemByKey(dataModel.Id);
                        oldImagePath = people.ImageUrl;
                        oldSignatureImagePath = people.SignatureImageUrl;

                        _mapper.Map(dataModel, people);
                        if (savedImagePath!= "")
                        {                            
                            people.ImageUrl = savedImagePath;
                        }
                        if (savedSignatureImagePath!= "")
                        {                            
                            people.SignatureImageUrl = savedSignatureImagePath;
                        }
                        _peopleService.Update(people);

                        DeleteImage(oldImagePath);
                        DeleteImage(oldSignatureImagePath);

                    }
                    else
                    {
                        var people = _mapper.Map<People>(dataModel);
                        if (savedImagePath != "")
                        {
                            people.ImageUrl = savedImagePath;
                        }
                        if (savedSignatureImagePath != "")
                        {
                            people.SignatureImageUrl = savedSignatureImagePath;
                        }

                        _peopleService.Add(people);                        
                    }

                    ModelState.Clear();
                    ViewBag.Result = true;
                    ViewBag.ErrorMessage = Resource.DataAddSuccessfully;
                    Response.StatusDescription = "Ok";
                }
                catch (Exception ex)
                {
                    Response.StatusDescription = "NotOk";
                    ViewBag.Result = false;
                    ViewBag.ErrorMessage = ex.Message;

                }

                return PartialView("CreateOrEdit", dataModel);
            }
            else
            {
                Response.StatusDescription = "NotOk";
                ViewBag.Result = false;
                return PartialView("CreateOrEdit", dataModel);
            }
        }




        private void ProvinceIds()
        {
            this.ViewBag.ProvinceIds = new SelectList(_provinceService.GetAllItems().Select(s => new { s.Id, s.Title }), "Id", "Title");
        }

        private void TownshipIds(long? provinceId, long? townshipId)
        {
            this.ViewBag.TownshipIds = new SelectList(_townshipService.GetAllItems().Where(t => t.ProvinceId == (provinceId ?? 0)).Select(s => new { s.Id, s.Title }), "Id", "Title", townshipId);
        }

        private void DeleteImage(string path)
        {

            if (path != "" && System.IO.File.Exists(Server.MapPath(path)))
            {
                System.IO.File.Delete(Server.MapPath(path));
            }

        }
    }
}