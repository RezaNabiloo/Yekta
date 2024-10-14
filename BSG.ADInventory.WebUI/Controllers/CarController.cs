using AutoMapper;
using BSG.ADInventory.Common.Classes;
using BSG.ADInventory.Data;
using BSG.ADInventory.Entities;
using BSG.ADInventory.Models.Car;
using BSG.ADInventory.Models.Car;
using BSG.ADInventory.Models.Country;
using BSG.ADInventory.Models.DataTable;
using BSG.ADInventory.Models.InvDoc;
using BSG.ADInventory.Resources;
using BSG.ADInventory.Services;
using BSG.ADInventory.WebUI.Helpers;
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
    [Permission(PermissionIds = "SystemManagement,BaseDataManagement,GeoLocations", LoginUrl = "~/Error/Index/403")]
    public class CarController : CrudController<Car>
    {
        #region ctor

        private readonly IUserRepository _userRepository;
        private readonly ICarTypeService _carTypeService;
        private readonly ICarService _carService;
        private readonly IPlateCharacterService _plateCharacterService;
        private readonly IPeopleService _peopleService;
        private readonly IMapper _mapper;

        public CarController(ICarService carService, IUserRepository userRepository, ICarTypeService carTypeService,
            IPlateCharacterService plateCharacterService,
            IPeopleService peopleService, IMapper mapper)
            : base(carService)
        {

            _carService = carService;
            _userRepository = userRepository;
            _carTypeService = carTypeService;
            _plateCharacterService = plateCharacterService;
            _peopleService = peopleService;
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
            var query = this._carService.GetAllItems().OrderByDescending(c=>c.Id);
            var mappedQuery = _mapper.Map<List<CarListDTO>>(query);
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
            #region Get Picture size            
            PictureSize size = DataHelper.GetElementSettingByKey("CarImageSize") as PictureSize;
            ViewBag.PictureSize = size.Width.ToString() + "W" + " × " + size.Height.ToString() + "H";
            #endregion

            Car car = new Car();
            if (id != 0)
            {
                car = this._carService.GetItemByKey(id);
            }
            var data = _mapper.Map<CarCEDTO>(car);
            this.CarTypeIds();
            this.PlateCharacterIds();
            this.PeopleIds();
            return PartialView("CrudAction", data);
        }

        [HttpPost]
        public ActionResult SetCrudAction(CarCEDTO dataModel, HttpPostedFileBase imageUrl)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var savedImagePath = "";
                    var oldImagePath = "";
                    if (imageUrl != null)
                    {
                        Object obj = DataHelper.GetElementSettingByKey("CarImageSize");
                        PictureSize size = obj as PictureSize;
                        savedImagePath = Common.ImageHelper.SavePicture(this.HttpContext, imageUrl, size, "/Uploads/Cars/");


                    }
                    if (dataModel.Id > 0)
                    {
                        var car = this._carService.GetItemByKey(dataModel.Id);
                        oldImagePath = car.ImageUrl;
                        _mapper.Map(dataModel, car);

                        if (savedImagePath != "")
                        {
                            car.ImageUrl = savedImagePath;
                        }
                        this._carService.Update(car);
                        DeleteImages(oldImagePath);
                    }
                    else
                    {
                        var car = _mapper.Map<Car>(dataModel);
                        if (savedImagePath != "")
                        {
                            car.ImageUrl = savedImagePath;
                        }
                        this._carService.Add(car);
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
                return PartialView("CreateOrEdit", dataModel);
            }

        }



        private void CarTypeIds()
        {
            this.ViewBag.CarTypeIds = new SelectList(this._carTypeService.GetAllItems().Select(s => new { s.Id, s.Title }), "Id", "Title");
        }
        private void PlateCharacterIds()
        {
            this.ViewBag.PlateCharacterIds = new SelectList(this._plateCharacterService.GetAllItems().Select(s => new { s.Id, s.Title }), "Id", "Title");
        }

        private void PeopleIds()
        {
            this.ViewBag.PeopleIds = new SelectList(this._peopleService.GetAllItems().Select(s => new { Id = s.Id, Title = s.FirstName + " " + s.LastName }), "Id", "Title");
        }

        private void DeleteImages(string path)
        {
            if (path != "" && System.IO.File.Exists(Server.MapPath(path)))
            {
                System.IO.File.Delete(Server.MapPath(path));
            }

        }

    }
}