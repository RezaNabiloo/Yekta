using AutoMapper;
using BSG.ADInventory.Entities;
using BSG.ADInventory.Models;
using BSG.ADInventory.Models.MatGroup;
using BSG.ADInventory.Models.ProductGroup;
using BSG.ADInventory.Resources;
using BSG.ADInventory.Services;
using BSG.ADInventory.WebUI.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
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
    [MinifyHtml]
    [Authorize]
    [Permission(PermissionIds = "SystemAdministrator", LoginUrl = "~/Error/Index/403")]
    public class MatGroupController : CrudController<MatGroup>
    {
        #region ctor
        private IUnitOfWork _unitOfWork;
        private IMatGroupService _matGroupService;
        private IUserService _userService;
        private readonly IMapper _mapper;

        public MatGroupController(IUnitOfWork unitOfWork, IMatGroupService matGroupService,
            IUserService userService, IMapper mapper

            )
            : base(matGroupService)
        {
            _unitOfWork = unitOfWork;
            _matGroupService = matGroupService;
            _userService = userService;
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


        private void ParentMatGroupIds()
        {
            this.ViewBag.ParentMatGroupIds = new SelectList(this._matGroupService.GetAllItems().Select(g => new { Id = g.Id, Title = g.Title }), "Id", "Title");
        }

        public ActionResult CrudAction(long id)
        {
            
            ViewBag.Id = id;
            MatGroup matGroup = new MatGroup();
            if (id != 0)
            {
                matGroup = this._matGroupService.GetItemByKey(id);
            }
            this.ParentMatGroupIds();
            var data = _mapper.Map<MatGroupCEDTO>(matGroup);
            return PartialView("CrudAction", data);
        }

        [HttpPost]
        public ActionResult SetCrudAction(MatGroupCEDTO dataModel, HttpPostedFileBase imageUrl, HttpPostedFileBase headerImageUrl)
        {            
            var pg = this._matGroupService.GetItemByKey(dataModel.Id);
            this.ParentMatGroupIds();
            if (ModelState.IsValid)
            {                
                if (dataModel.Id > 0)
                {
                    var matGroup = this._matGroupService.GetItemByKey(dataModel.Id);
                    _mapper.Map(dataModel, matGroup);                
                    this._matGroupService.Update(matGroup);                
                }
                else
                {
                    var matGroup = _mapper.Map<MatGroup>(dataModel);                    
                    this._matGroupService.Add(matGroup);
                }
                ModelState.Clear();
                ViewBag.Result = true;
                ViewBag.ErrorMessage = Resource.DataAddSuccessfully;
                Response.StatusDescription = "Ok";                  
                return PartialView("CreateOrEdit", dataModel);
            }
            else
            {
                Response.StatusDescription = "NotOk";
                ViewBag.Result = false;
                return PartialView("CreateOrEdit", dataModel);
            }

        }


        #region JsTree
        [HttpPost]
        public ActionResult DoJsTreeOperation(JsTreeOperationData data)
        {
            long parentId;
            long Id;
            switch (data.Operation)
            {
                case JsTreeOperation.CopyNode:
                case JsTreeOperation.CreateNode:
                    parentId = Int64.Parse(data.ParentId);
                    MatGroup s = new MatGroup();
                    s.Title = data.Text;
                    s.ParentMatGroupId = parentId;
                    this._matGroupService.Add(s);

                    return Json(new { id = s.Id.ToString() }, JsonRequestBehavior.AllowGet);

                case JsTreeOperation.DeleteNode:
                    Id = Int64.Parse(data.Id);
                    var es = this._matGroupService.GetItemByKey(Id);
                    if (es != null)
                    {
                        if (es.ChildMatGroups.Count() > 0)
                        {
                            return Json(new { message = "با توجه به وجود زیر مجموعه برای آیتم موردنظر، امکان حذف وجود ندارد" }, JsonRequestBehavior.AllowGet);
                        }
                        this._matGroupService.Remove(es);

                    }

                    return Json(new { message = "ok" }, JsonRequestBehavior.AllowGet);

                case JsTreeOperation.MoveNode:
                    Id = Int64.Parse(data.Id);
                    var moveScope = this._matGroupService.GetItemByKey(Id);
                    if (moveScope != null)
                    {
                        if (data.ParentId == "#")
                            moveScope.ParentMatGroupId = null;
                        else
                            moveScope.ParentMatGroupId = Int64.Parse(data.ParentId);

                        this._matGroupService.Update(moveScope);

                    }
                    return Json(new { result = "ok" }, JsonRequestBehavior.AllowGet);

                case JsTreeOperation.RenameNode:
                    Id = Int64.Parse(data.Id);
                    var renameScope = this._matGroupService.GetItemByKey(Id);
                    if (renameScope != null)
                    {
                        renameScope.Title = data.Text;
                        this._matGroupService.Update(renameScope);
                    }
                    return Json(new { result = "ok" }, JsonRequestBehavior.AllowGet);


                default:
                    throw new InvalidOperationException(string.Format("Error. {0} is not supported.", data.Operation));
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetTreeJson()
        {

            var nodesList = new List<JsTreeNode>();

            var data = this._matGroupService.GetAllItems();
            var rootScope = data.Where(x => x.ParentMatGroupId == null).ToList();
            foreach (var item in rootScope)
            {
                var rootNode = new JsTreeNode
                {
                    id = item.Id.ToString(),
                    text = item.Title,//+" - "+item.EnTitle,                    
                    a_attr = { href = "MatGroup/Edit/" + item.Id }
                };
                PopulateTree(item, rootNode);
                nodesList.Add(rootNode);
            }

            return Json(nodesList, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public void PopulateTree(MatGroup root, JsTreeNode node)
        {
            if (root.ChildMatGroups == null) return;
            foreach (var item in root.ChildMatGroups)
            {
                var jsTreeNode = new JsTreeNode
                {
                    id = item.Id.ToString(),
                    text = item.Title,//+" - "+item.EnTitle,
                    //                  icon = Url.Content("~/Content/images/bookmark_book_open.png"),
                    a_attr = { href = "MatGroup/Edit/" + item.Id.ToString() }
                };
                node.children.Add(jsTreeNode);

                PopulateTree(item, jsTreeNode);
            }
        }
        #endregion



        

    }
}