using AutoMapper;
using BSG.ADInventory.Common.Classes;
using BSG.ADInventory.Entities;
using BSG.ADInventory.Models.Brand;
using BSG.ADInventory.Models.DataTable;
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
using Zcf.Paging;
using Zcf.Web.Mvc.Controllers;
using Zcf.Web.Mvc.Security;

namespace BSG.ADInventory.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Permission(PermissionIds = "SystemAdministrator", LoginUrl = "~/Error/Index/403")]
    [MinifyHtml]
    public class BrandController : CrudController<Brand>
    {
        #region ctor
        private IUnitOfWork _unitOfWork;
        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;

        public BrandController(IUnitOfWork unitOfWork, IBrandService brandService, IMapper mapper)
            : base(brandService)
        {
            _unitOfWork = unitOfWork;
            _brandService = brandService;
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
            var query = this._brandService.GetAllItems();
            var mappedQuery = _mapper.Map<List<BrandListDTO>>(query);
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
            PictureSize size = DataHelper.GetElementSettingByKey("BrandImageSize") as PictureSize;
            ViewBag.PictureSize = size.Width.ToString() + "W" + " × " + size.Height.ToString() + "H";
            #endregion

            Brand brand = new Brand();
            if (id != 0)
            {
                brand = this._brandService.GetItemByKey(id);
            }
            var data = _mapper.Map<BrandCEDTO>(brand);
            return PartialView("CrudAction", data);
        }

        [HttpPost]
        public ActionResult SetCrudAction(BrandCEDTO dataModel, HttpPostedFileBase imageUrl)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var savedImagePaths = "";
                    //if (imageUrl != null)
                    //{

                    //    Object obj = DataHelper.GetElementSettingByKey("BrandImageSize");
                    //    PictureSize size = obj as PictureSize;

                    //    if (Path.GetExtension(imageUrl.FileName) == ".webp")
                    //    {
                    //        savedImagePaths = Common.ImageHelper.SaveWebPImage(this.HttpContext, imageUrl, size, "/Uploads/Brands/");
                    //    }
                    //    else
                    //    {
                    //        savedImagePaths = Common.ImageHelper.SavePicture(this.HttpContext, imageUrl, size, "/Uploads/Brands/");
                    //    }

                    //}
                    if (dataModel.Id > 0)
                    {
                        var brand = this._brandService.GetItemByKey(dataModel.Id);
                        _mapper.Map(dataModel, brand);
                        //if (savedImagePaths != "")
                        //{
                        //    brand.ImageUrl = savedImagePaths;
                        //}
                        this._brandService.Update(brand);
                    }
                    else
                    {
                        var brand = _mapper.Map<Brand>(dataModel);
                        //if (savedImagePaths != "")
                        //{
                        //    brand.ImageUrl = savedImagePaths;
                        //}
                        this._brandService.Add(brand);
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

        public override JsonResult DeleteAjax(long id)
        {
            try
            {
                var data = _brandService.GetItemByKey(id);
                var result = base.DeleteAjax(id);

                if (System.IO.File.Exists(Server.MapPath(data.ImageUrl)))
                {
                    System.IO.File.Delete(Server.MapPath(data.ImageUrl));
                }

                return result;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}