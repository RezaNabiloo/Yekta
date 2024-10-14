using AutoMapper;
using BSG.ADInventory.Common.Enum;
using BSG.ADInventory.Entities;
using BSG.ADInventory.Models.DataTable;
using BSG.ADInventory.Models.Mat;
using BSG.ADInventory.Models.MatOrder;
using BSG.ADInventory.Models.MatOrderDetail;
using BSG.ADInventory.Resources;
using BSG.ADInventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebMarkupMin.AspNet4.Mvc;
using Zcf.Data;
using Zcf.Models;
using Zcf.Paging;
using Zcf.Web.Mvc.Controllers;

namespace BSG.ADInventory.WebUI.Controllers
{
    [MinifyHtml]
    [Authorize]
    public class MatOrderController : CrudController<MatOrder>
    {   
        private readonly IUserService _userService;
        private readonly IPlateCharacterService _plateChara;
        private readonly IPeopleService _peopleService;
        private readonly IMatOrderService _matOrderService;
        private readonly IMatService _matService;
        private readonly IProjectService _projectService;
        private readonly IMatOrderDetailService _matOrderDetailService;
        private readonly IMapper _mapper;

        public MatOrderController(IUserService userService,
            IPeopleService peopleService,
            IMatOrderService matOrderService,
            IPlateCharacterService plateChara,
            IMatService matService,
            IProjectService projectService,
            IMatOrderDetailService matOrderDetailService,
            IMapper mapper            
            )
             : base(matOrderService)
        {
     
            _userService = userService;
            _plateChara = plateChara;
            _peopleService = peopleService;
            _matOrderService = matOrderService;
            _matService = matService;
            _projectService = projectService;
            _matOrderDetailService = matOrderDetailService;
            _mapper = mapper;
        }

        [NonAction]
        public override ActionResult Index(PagedQueryable criteria)
        {
            return base.Index(criteria);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inbox()
        {
            return View();
        }
        public ActionResult Confirm()
        {
            return View();
        }


        #region OutBox
        public ActionResult Outbox()
        {
            return View();
        }
        #endregion OutBox
        public ActionResult Archive()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetDataList(JqDataTable model, int type)
        {
            var data = this._matOrderService.GetDataList(type);

            var count = data.Count();
            if (model.iDisplayLength == -1)
            {
                model.iDisplayLength = count;
            }
            data = data.Skip(model.iDisplayStart).Take(model.iDisplayLength).ToList();
            var result = new
            {
                iTotalRecords = data.Count(),//records per page 
                iTotalDisplayRecords = count, //total table count
                aaData = data //data list

            };

            return Json(result, JsonRequestBehavior.AllowGet);



        }

        public ActionResult GetDataTableFilteredList(JqDataTable filter, int type)
        {
            var query = this._matOrderService.GetDataList(type);
            var mappedQuery = _mapper.Map<List<MatOrderListDTO>>(query);
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


        private void MaterialIds()
        {
            this.ViewBag.MaterialIds = new SelectList(this._matService.GetAllItems().Select(s => new { Id = s.Id, Title = s.Code + " - " + s.Title + " - " + s.TechnicalSpec + " - " + s.Dimention + " - واحدشمارش: " + s.MatUnit.Title }), "Id", "Title");
        }

        private void ProjectIds()
        {
            var user = this._userService.GetCurrentUser();
            this.ViewBag.ProjectIds = new SelectList(user.Projects.Select(s => new { s.Id, s.Title }), "Id", "Title");
        }


        public ActionResult CrudAction(long id)
        {
            var data = new MatOrderCEDTO { Archived = false, RequiredDate = DateTime.Now, Priority=Priority.Normal };
            if (id > 0)
            {
                var matOrder = this._matOrderService.GetItemByKey(id);                
                data = _mapper.Map<MatOrderCEDTO>(matOrder);                
            }

            this.MaterialIds();
            this.ProjectIds();

            return PartialView("CrudAction", data);
        }

        [HttpPost]
        public ActionResult SetCrudAction(MatOrderCEDTO dataModel)
        {
            var data = this._matOrderService.SetRequestDocData(dataModel);
            return Json(data);
        }


        public ActionResult MatOrderPreview(long id)
        {
            var data = this._matOrderService.GetItemByKey(id);
            return PartialView("MatOrderPreview", data);

        }


        //public override JsonResult DeleteAjax(long id)
        //{
        //    CustomResult<bool> data = new CustomResult<bool>();
        //    var req = this._matOrderService.GetItemByKey(id);
        //    if (req.ConfirmStatus!=ConfirmStatus.Pending)
        //    {
        //        data.Result = false;
        //        data.ErrorMessage = "با توجه به وضعیت درخواست ، مجاز به حذف آن نمی‌باشید.";
        //        return Json(data);
        //    }

        //    if (req != null)
        //    {

        //        foreach (var item in req.MatOrderDetails.ToList())
        //        {
        //            this._matOrderDetailService.Remove(item);
        //        }
        //    }

        //    return base.DeleteAjax(id);
        //}


        public ActionResult RejectOrder(long id)
        {
            CustomResult<bool> data = new CustomResult<bool>();
            var order = this._matOrderService.GetItemByKey(id);
            if (order == null || order.ConfirmStatus != ConfirmStatus.Pending)
            {
                data.Result = false;
                data.ErrorMessage = Resource.ItemNotFound;
            }
            else
            {
                order.ConfirmStatus = ConfirmStatus.Rejected;
                order.ConfirmTime = DateTime.Now;
                order.ConfirmUserId = this._userService.GetCurrentUser().Id;
                this._matOrderService.Update(order);
                data.Result = true;
                data.ErrorMessage = Resource.DataSavedSuccessfully;
            }
            return Json(data);
        }


        public ActionResult ConfirmOrder(long id)
        {
            var order = this._matOrderService.GetItemByKey(id);

            MatOrderConfirmModel data = new MatOrderConfirmModel
            {
                Id = order.Id,
                Project = order.Project.Title,
                RequiredDate = order.RequiredDate,
                RequestUser = order.CreateUser.FullName,
                RequestDescription = order.RequestDescription
            };

            foreach (var item in order.MatOrderDetails)
            {
                MatOrderDetailConfirmModel det = new MatOrderDetailConfirmModel { 
                    Id=item.Id,
                    MatId=item.MatId,
                    Title=item.Mat.Title+"-"+item.Mat.TechnicalSpec,
                    MatUnit=item.Mat.MatUnit.Title,
                    RequiredAmount=item.RequiredAmount,
                    ConfirmedAmount=item.ConfirmedAmount==0? item.RequiredAmount: item.ConfirmedAmount,
                    ConfirmDescription=item.ConfirmDescription

                };
                data.Mats.Add(det);
            }

            return PartialView("ConfirmOrder", data);
        }

        [HttpPost]
        public ActionResult ConfirmOrder(MatOrderConfirmModel dataModel)
        {
            if (dataModel.Mats.Where(m=>m.ConfirmedAmount>0).Count()==0)
            {
                ModelState.AddModelError("Mats", "برای هیچ یک از موارد مقدار تائید شده، مشخص نگردیده. حداقل برای یک مورد باید مقدار تائید شده مشخص گردد.");
            };

            if (ModelState.IsValid)
            {
                try
                {

                    this._matOrderService.ConfirmOrder(dataModel);
                    ViewBag.Result = true;
                    ViewBag.ErrorMessage = Resource.DataAddSuccessfully;
                }
                catch (Exception ex)
                {
                    ViewBag.Result = false;
                    ViewBag.ErrorMessage = ex.Message;

                }

                return PartialView("ConfirmOrderDetail");
            }
            else
            {
                return PartialView("ConfirmOrderDetail", dataModel);
            }
        }

    }
}