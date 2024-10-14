using AutoMapper;
using BSG.ADInventory.Common.Classes;
using BSG.ADInventory.Entities;
using BSG.ADInventory.Models.DataTable;
using BSG.ADInventory.Models.Mat;
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

namespace BSG.ADInventory.WebUI.Controllers
{
    [MinifyHtml]
    [Authorize]
    [Permission(PermissionIds = "SystemManagement,BaseDataManagement,MaterialManagement", LoginUrl = "~/Error/Index/403")]
    public class MatController : CrudController<Mat>
    {
        #region ctor
        private IUnitOfWork _unitOfWork;        
        private readonly IMatGroupService _matGroupService;
        private readonly IMatService _matService;
        private readonly IBrandService _brandService;
        private readonly IMatUnitService _matUnitService;
        private readonly IMapper _mapper;

        public MatController(IUnitOfWork unitOfWork, IMatGroupService matGroupService, IMatService matService, IBrandService brandService,
            IMatUnitService matUnitService,
            IMapper mapper
            )
            : base(matService)
        {
            _unitOfWork = unitOfWork;
            
            _matGroupService = matGroupService;
            _matService = matService;
            _matUnitService = matUnitService;
            _mapper = mapper;
            _brandService = brandService;
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
            var query = this._matService.GetAllItems().OrderByDescending(p => p.Id);
            var mappedQuery = _mapper.Map<List<MatListDTO>>(query);
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
            PictureSize size = DataHelper.GetElementSettingByKey("MatImageSize") as PictureSize;
            ViewBag.PictureSize = size.Width.ToString() + "W" + " × " + size.Height.ToString() + "H";
            #endregion


            ViewBag.Id = id;
            this.MatGroupIds();
            this.BrandIds();
            this.MatUnitIds();
            ViewBag.MatGroupId = 0;
            Mat mat = new Mat();
            if (id != 0)
            {
                mat = this._matService.GetItemByKey(id);
                ViewBag.MatGroupId = mat.MatGroupId;
            }
            var data = _mapper.Map<MatCEDTO>(mat);
            return PartialView("CrudAction", data);

        }

        [HttpPost]
        public ActionResult SetCrudAction(MatCEDTO dataModel, HttpPostedFileBase imageUrl)
        {
            this.MatGroupIds();
            this.BrandIds();
            this.MatUnitIds();
            ViewBag.MatGroupId = dataModel.MatGroupId;

            #region Get Picture size            
            PictureSize size = DataHelper.GetElementSettingByKey("MatImageSize") as PictureSize;
            ViewBag.PictureSize = size.Width.ToString() + "W" + " × " + size.Height.ToString() + "H";            
            #endregion



            if (ModelState.IsValid)
            {
                try
                {
                    var savedImagePath = "";                    
                    string oldImageUrl = "";
                    
                    if (imageUrl != null)
                    {
                        savedImagePath = Common.ImageHelper.SavePicture(this.HttpContext, imageUrl, size, "/Uploads/Mats/");                        
                    }
                    

                    if (dataModel.Id > 0)
                    {
                        var mat = this._matService.GetItemByKey(dataModel.Id);
                        oldImageUrl = mat.ImageUrl;                        
                        
                        _mapper.Map(dataModel, mat);
                        if (savedImagePath!= "")
                        {
                            mat.ImageUrl = savedImagePath;
                        }
                        
                        this._matService.Update(mat);

                        DeleteImages(oldImageUrl);

                    }
                    else
                    {
                        var mat = _mapper.Map<Mat>(dataModel);
                        if (savedImagePath!="")
                        {
                            mat.ImageUrl=savedImagePath;
                        }                        

                        this._matService.Add(mat);                        
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

        private void MatGroupIds()
        {
            this.ViewBag.MatGroupIds = new SelectList(this._matGroupService.GetAllItems().Select(g => new { Id = g.Id, Title = g.Title }), "Id", "Title");
        }

        private void BrandIds()
        {
            this.ViewBag.BrandIds = new SelectList(this._brandService.GetAllItems().Select(g => new { Id = g.Id, Title = g.FaTitle }), "Id", "Title");
        }
        private void MatUnitIds()
        {
            this.ViewBag.MatUnitIds = new SelectList(this._matUnitService.GetAllItems().Select(g => new { Id = g.Id, Title = g.Title+"-"+g.Abbreviation }), "Id", "Title");
        }




        ///// <summary>
        ///// Loads the factories.
        ///// </summary>
        ///// 
        //public ActionResult MatInv(long id)
        //{

        //    //ViewBag.MatInv = _matInvService.GetMatInventory(id);
        //    var data = _matService.GetItemByKey(id);

        //    this.MatIds();

        //    return View(data);
        //}


        //[HttpPost]
        //public ActionResult GetMatQty(long id, double qty) 
        //{
        //    CustomResult<double> data = new CustomResult<double>();
        //    var currentUser = this.userService.GetCurrentUser();

        //    try
        //    {

        //        //data.Result = _matService.GetMatQty(branchId, id);
        //        data.ErrorCode = 200;
        //        data.ErrorMessage = Resource.Successful;
        //    }
        //    catch (Exception)
        //    {

        //        data.Result = 0;
        //        data.ErrorCode = 500;
        //        data.ErrorMessage = Resource.ErrorCallAdmin;
        //    }

        //    return Json(data);
        //}

        //private void MatGroupIds(long? id)
        //{
        //    this.ViewBag.MatGroupIds = new SelectList(_matGroupService.GetAllItems().Select(s => new { s.Id, s.Title }), "Id", "Title",id);
        //}
        //private void MatUnitIds()
        //{
        //    this.ViewBag.MatUnitIds = new SelectList(_matUnitService.GetAllItems().Select(s => new { s.Id, s.Title}), "Id", "Title");
        //}

        //private void BrandIds()
        //{
        //    this.ViewBag.BrandIds = new SelectList(this.brandService.GetAllItems().Select(s => new { Id=s.Id, Title=s.BrandInfo}), "Id", "Title");
        //}

        //private void MatIds()
        //{
        //    this.ViewBag.MatIds = new SelectList(_matService.GetAllItems().Select(s => new { s.Id, s.MatInfo }), "Id", "MatInfo ");
        //}




        //[HttpPost]
        //public ActionResult CheckMatQty(long id, double qty)
        //{
        //    CustomResult<double> data = new CustomResult<double>();
        //    var currentUser = this.userService.GetCurrentUser();

        //    //var branchId = currentUser.People == null ? 0 : this.userService.GetCurrentUser().People.BranchId ?? 0;
        //    //try
        //    //{

        //    //    data.Result = _matService.GetMatQty(branchId, id);
        //    //    data.ErrorCode = 200;
        //    //    data.ErrorMessage = Resource.Successful;
        //    //}
        //    //catch (Exception)
        //    //{

        //    //    data.Result = 0;
        //    //    data.ErrorCode = 500;
        //    //    data.ErrorMessage = Resource.ErrorCallAdmin;
        //    //}

        //    return Json(data);
        //}

        //[HttpGet]
        //public ActionResult CheckMatAllocationType (long id)
        //{
        //    var mat = _matService.GetItemByKey(id);
        //    int data = 0;
        //    if (mat!=null)
        //    {
        //        data = (int)mat.MatAllocationType;
        //    }
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}


        //public ActionResult CrudAction(long id)
        //{
        //    ViewBag.Id = id;
        //    Mat data = new Mat();
        //    if (id != 0)
        //    {
        //        data = _matService.GetItemByKey(id);
        //        this.MatGroupIds(data.MatGroupId);
        //        ViewBag.MatGroupId = data.MatGroupId;
        //    }
        //    else
        //    {
        //        this.MatGroupIds(null);
        //        ViewBag.MatGroupId = 0;
        //    }


        //    this.MatUnitIds();
        //    this.BrandIds();

        //    return PartialView("CrudAction", data);
        //}

        //[HttpPost]
        //public ActionResult SetCrudAction(Mat dataModel)
        //{
        //    if (!string.IsNullOrEmpty(dataModel.Code) && _matService.IsDuplicateMatCode(dataModel.Code))
        //    {
        //        ModelState.AddModelError("Code", Resource.DuplicateData);
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            if (dataModel.Id > 0)
        //            {
        //                _matService.Update(dataModel);
        //            }
        //            else
        //            {
        //                _matService.Add(dataModel);
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


        //        this.MatGroupIds(dataModel.MatGroupId);
        //        this.MatUnitIds();
        //        this.BrandIds();
        //        ViewBag.MatGroupId = dataModel.MatGroupId;
        //        return PartialView("CreateOrEdit");
        //    }
        //    else
        //    {
        //        this.MatGroupIds(dataModel.MatGroupId);
        //        this.MatUnitIds();
        //        this.BrandIds();
        //        ViewBag.MatGroupId = dataModel.MatGroupId;
        //        return PartialView("CreateOrEdit", dataModel);
        //    }

        //}



        private void DeleteImages(string path)
        {
            if (path != "" && System.IO.File.Exists(Server.MapPath(path)))
            {
                System.IO.File.Delete(Server.MapPath(path));
            }

        }

    }
}