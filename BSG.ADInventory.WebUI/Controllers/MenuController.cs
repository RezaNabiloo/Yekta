using BSG.ADInventory.Entities;
using BSG.ADInventory.Models;
using BSG.ADInventory.Models.Menu;
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
    //[Permission(PermissionIds = "SystemManagement,BaseDataManagement,MenuManagement", LoginUrl = "~/Error/Index/403")]
    public class MenuController : CrudController<Menu>
    {
        #region ctor
        private IUnitOfWork unitOfWork;
        private IMenuService menuService;
        private IUserService userService;
        public MenuController(IUnitOfWork unitOfWork, IMenuService menuService,
            IUserService userService
            )
            : base(menuService)
        {
            this.unitOfWork = unitOfWork;
            this.menuService = menuService;
            this.userService = userService;
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
        public ActionResult IndexPublic()
        {
            return this.View();
        }

        private void ParentMenuIds()
        {
            this.ViewBag.ParentMenuIds = new SelectList(this.menuService.GetAllItems().Select(g => new { Id=g.Id, Title=g.Title}), "Id", "Title");
        }
        private void ParentPublicMenuIds()
        {
            this.ViewBag.ParentMenuIds = new SelectList(this.menuService.GetAllItems().Select(g => new { Id=g.Id, Title=g.Title }), "Id", "Title");
        }


        public ActionResult CrudAction(long id)
        {
            ViewBag.Id = id;
            MenuModel data = this.menuService.GetMenuModelForCreate();
            data.IsAdminMenu = true;
            if (id>0)
            {
                data = this.menuService.GetMenuModelForEdit(id);
            }
            
            this.ParentMenuIds();
            return PartialView("CrudAction", data);
        }
               
        [HttpPost]
        public ActionResult SetCrudAction(MenuModel dataModel)
        {

            ModelState.Remove("Id");


            if (ModelState.IsValid)
            {
                try
                {
                    
                    if (dataModel.Id > 0)
                    {
                        this.menuService.UpdateMenu(dataModel);
                    }
                    else
                    {
                        this.menuService.AddMenu(dataModel);
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
                this.ParentMenuIds();
                return PartialView("CreateOrEdit");
            }
            else
            {
                this.ParentMenuIds();
                return PartialView("CreateOrEdit", dataModel);
            }
        }

        
        public ActionResult UserAccessMenu()
        {
            var data = this.menuService.UserAccessMenu(this.userService.GetCurrentUser().Id);
            //var data = Session["UserMenus"];
            return this.View("UserAccessMenu", data);
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
                    //todo: save data

                    parentId = Int64.Parse(data.ParentId);
                    Menu s = new Menu();
                    s.Title = data.Text;                    
                    s.ParentMenuId = parentId;

                    this.menuService.Add(s);


                    return Json(new { id = s.Id.ToString() }, JsonRequestBehavior.AllowGet);


                //var rnd = new Random(); // آي دي ركورد پس از ثبت در بانك اطلاعاتي دريافت و بازگشت داده شود
                //return Json(new { id = rnd.Next() }, JsonRequestBehavior.AllowGet);

                case JsTreeOperation.DeleteNode:
                    //todo: delete data
                    Id = Int64.Parse(data.Id);
                    var es = this.menuService.GetItemByKey(Id);
                    if (es != null)
                    {
                        if (es.ChildMenus.Count() > 0)
                        {
                            return Json(new { message = "با توجه به وجود زیر مجموعه برای آیتم موردنظر، امکان حذف وجود ندارد" }, JsonRequestBehavior.AllowGet);
                        }
                        this.menuService.Remove(es);

                    }
                    //todo: delete data
                    return Json(new { message = "ok" }, JsonRequestBehavior.AllowGet);

                case JsTreeOperation.MoveNode:
                    //todo: Move note
                    Id = Int64.Parse(data.Id);

                    var moveScope = this.menuService.GetItemByKey(Id);
                    if (moveScope != null)
                    {
                        if (data.ParentId == "#")
                            moveScope.ParentMenuId = null;
                        else
                            moveScope.ParentMenuId = Int64.Parse(data.ParentId);

                        this.menuService.Update(moveScope);
                    }
                    //todo: Move node
                    return Json(new { result = "ok" }, JsonRequestBehavior.AllowGet);

                case JsTreeOperation.RenameNode:
                    //todo: Rename data
                    Id = Int64.Parse(data.Id);

                    var renameScope = this.menuService.GetItemByKey(Id);
                    if (renameScope != null)
                    {
                        renameScope.Title = data.Text;
                        this.menuService.Update(renameScope);
                    }
                    return Json(new { result = "ok" }, JsonRequestBehavior.AllowGet);
                //todo: Rename data

                default:
                    throw new InvalidOperationException(string.Format("{0} is not supported.", data.Operation));
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetTreeJson()
        {

            var nodesList = new List<JsTreeNode>();

            var data = this.menuService.GetAllItems();
            var rootScope = data.Where(x => x.ParentMenuId == null).OrderBy(m => m.ViewOrder).OrderBy(m => m.Title);
            foreach (var item in rootScope.OrderBy(m => m.ViewOrder).OrderBy(m => m.Title))
            {
                var rootNode = new JsTreeNode
                {
                    id = item.Id.ToString(),
                    text = item.Title,//+" - "+item.EnTitle,                    
                    a_attr = { href = "Menu/Edit/" + item.Id, viewOrder=item.ViewOrder }
                };
                PopulateTree(item, rootNode);
                nodesList.Add(rootNode);
            }

            return Json(nodesList, JsonRequestBehavior.AllowGet);
        }

        

        [AllowAnonymous]
        public void PopulateTree(Menu root, JsTreeNode node)
        {
            if (root.ChildMenus == null) return;
            foreach (var item in root.ChildMenus)
            {
                var jsTreeNode = new JsTreeNode
                {
                    id = item.Id.ToString(),
                    text = item.Title,                    
                    a_attr = { href = "Menu/Edit/" + item.Id.ToString() }
                };
                node.children.Add(jsTreeNode);

                PopulateTree(item, jsTreeNode);
            }
        }
        #endregion


    }
}