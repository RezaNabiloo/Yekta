using BSG.ADInventory.Common.Security;
using BSG.ADInventory.Models.DataTable;
using BSG.ADInventory.Models.Role;
using BSG.ADInventory.Resources;
using BSG.ADInventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;
using WebMarkupMin.AspNet4.Mvc;
using Zcf.Web.Mvc.Controllers;
using Zcf.Web.Mvc.Security;

namespace BSG.ADInventory.WebUI.Controllers
{
    [MinifyHtml]
    [Authorize]
    [Permission(PermissionIds = "SystemManagement,RoleManagement", LoginUrl = "~/Account/Login")]
    //[MenuItem(ActionMethodName = "Index", ResourceType = typeof(Resources.Resource), TitleResourceName = "Roles", CustomerelResourceName = "Security")]
    [Authorize]
    public class RoleController : CrudController<Models.Role.RoleModel, Models.Role.Criteria, Guid>
    {
        private readonly IRoleService roleService;

        /// <summary>
        ///   Initializes a new instance of the <see cref="RoleController" /> class.
        /// </summary>
        public RoleController(IRoleService service)
            : base(service)
        {
            this.roleService = service;
        }

        [NonAction]
        public override ActionResult Index(Models.Role.Criteria criteria)
        {
            return base.Index(criteria);
        }

        public ActionResult Index()
        {
            
            return this.View();
        }

        /// <summary>
        /// GET: /Role/Create
        /// </summary>
        /// <returns></returns>
        public override ActionResult Create()
        {
            var data = this.roleService.GetRoleModelForCreate();
            return this.View(data);
        }

        /// <summary>
        /// GET: /Role/Create
        /// </summary>
        /// <param name="roleModel">The role model.</param>
        /// <returns></returns>
        [HttpPost]
        public override ActionResult Create(Models.Role.RoleModel roleModel)
        {
            if (roleModel.Name == null)
            {
                ModelState.AddModelError("Name", Resource.FieldIsRequired);
                return View(roleModel);
            }

            if (this.roleService.CheckRoleNameDuplicate(roleModel.Name))
            {
                this.ModelState.AddModelError("Name", Resources.Resource.DuplicatedRoleName);
                return this.View(this.roleService.GetRoleModelForCreate());
            }

            return base.Create(roleModel);
        }

        public override ActionResult Edit(Guid id)
        {
            var role = this.roleService.GetRoleById(id);

            ViewBag.PermissionCategory = role.Permissions.GroupBy(n => new { n.CategoryId, n.Category })
                 .Select(g => new PermissionCategoryDataModel { CategoryId = g.Key.CategoryId, Category = g.Key.Category }).OrderBy(p => p.CategoryId).ToList();

            return View(role);

        }


        /// <summary>
        /// GET: /Role/Edit/5
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="roleModel">The role model.</param>
        /// <returns></returns>
        [HttpPost]
        public override ActionResult Edit(Guid id, Models.Role.RoleModel roleModel)
        {
            if (roleModel.Name == null)
            {
                ModelState.AddModelError("Name", Resource.FieldIsRequired);

                return View(roleModel);
            }
            try
            {
                if (this.roleService.CheckRoleNameDuplicate(roleModel.Name, id))
                {
                    this.ModelState.AddModelError("Name", Resources.Resource.DuplicatedRoleName);
                    return this.View(this.roleService.GetItemByKey(id));
                }

                this.roleService.Update(roleModel);

                return this.RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var reza = ex;
                return this.View(this.roleService.GetItemByKey(id));
            }
        }

        public ActionResult GetDataList(JqDataTable model)
        {

            var data = this.roleService.GetDataList();


            if (model.iSortCol_0 == 1 && model.sSortDir_0 == "asc")
            {
                data = data.OrderBy(x => x.Name);
            }
            else if (model.iSortCol_0 == 1 && model.sSortDir_0 == "desc")
            {
                data = data.OrderByDescending(x => x.Name);
            }




            //Global Search functionality 

            if (!(string.IsNullOrEmpty(model.sSearch)) && !(string.IsNullOrWhiteSpace(model.sSearch)))
            {
                data = data.Where(x => x.Name.Contains(model.sSearch));
            }




            if (!(string.IsNullOrEmpty(model.sSearch_1)) && !(string.IsNullOrWhiteSpace(model.sSearch_1)))
            {
                data = data.Where(x => x.Name.Contains(model.sSearch_1));
            }


            //etc....
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

        public ActionResult CrudAction(Guid? id)
        {
            ViewBag.Id = id;
            RoleModel data = this.roleService.GetRoleModelForCreate();

            if (id != null)
            {
                data = this.roleService.GetRoleById(id.Value);
            }
            return PartialView("CrudAction", data);
        }

        [HttpPost]
        public ActionResult SetCrudAction(RoleModel dataModel)
        {
            if (dataModel.Name == null)
            {
                ModelState.AddModelError("Name", Resource.FieldIsRequired);
            }

            if (this.roleService.CheckRoleNameDuplicate(dataModel.Name, dataModel.Id))
            {
                this.ModelState.AddModelError("Name", Resources.Resource.DuplicatedRoleName);
            }



            ModelState.Remove("Id");

            
            if (ModelState.IsValid)
            {
                try
                {
                    if (dataModel.Id != null)
                    {                        
                        this.roleService.Update(dataModel);
                    }
                    else
                    {
                        this.roleService.Add(dataModel);
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