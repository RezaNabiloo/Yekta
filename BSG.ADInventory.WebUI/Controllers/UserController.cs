using BSG.ADInventory.Data;
using BSG.ADInventory.Entities;
using BSG.ADInventory.Models.DataTable;
using BSG.ADInventory.Models.User;
using BSG.ADInventory.Resources;
using BSG.ADInventory.Services;
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

namespace BSG.ADInventory.WebUI.Controllers
{
    [MinifyHtml]
    [Authorize]
   // [Permission(PermissionIds = "SystemManagement,UserManagement", LoginUrl = "~/Error/Index/403")]
    public class UserController : CrudController<UserModel, BSG.ADInventory.Models.User.Criteria, Guid>
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IPeopleService peopleService;


        public UserController(IUserService userService, IRoleService roleService, IPeopleService peopleService)
            : base(userService)
        {
            this.userService = userService;
            this.roleService = roleService;
            this.peopleService = peopleService;
        }

        [NonAction]
        public override System.Web.Mvc.ActionResult Index(Models.User.Criteria criteria)
        {
            return base.Index(criteria);
        }


        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //var data = this.userService.GetAllItems();

            //var model = data.ToList().Select(u => new Entities.User()
            //{
            //    Id = u.Id,
            //    UserName = u.UserName,
            //    IsActive = u.IsActive,  
            //    People=u.People,
            //    //Type = u.Type,
            //    Email = u.Email
            //});

            //return this.View(model);

            return View();
           
        }

      


        /// <summary>
        /// GET: /User/GetUsers/
        /// </summary>
        /// <param name="term">The term.</param>
        /// <returns></returns>
        public ActionResult GetUsers(string term)
        {
            var data = this.userService.GetUsers(term)
                .Select(c =>
                    new Models.AutoComplete.Item
                    {
                        Value = c.Id.ToString(),
                        Label = c.UserName
                    });

            return this.Json(data, JsonRequestBehavior.AllowGet);
        }


        private void PeopleIds()
        {
            this.ViewBag.PeopleIds = new SelectList(this.peopleService.GetAllItems().Select(s => new { s.Id, s.FullName }), "Id", "FullName");
        }

        public ActionResult GetDataList(JqDataTable model)
        {

            var data = this.userService.GetDataList();


            if (model.iSortCol_0 == 1 && model.sSortDir_0 == "asc"){data = data.OrderBy(x => x.UserName);}
            else if (model.iSortCol_0 == 1 && model.sSortDir_0 == "desc"){data = data.OrderByDescending(x => x.UserName);}

            if (model.iSortCol_0 == 2 && model.sSortDir_0 == "asc"){data = data.OrderBy(x => x.FullName);}
            else if (model.iSortCol_0 == 2 && model.sSortDir_0 == "desc"){data = data.OrderByDescending(x => x.FullName);}

            if (model.iSortCol_0 == 3 && model.sSortDir_0 == "asc"){data = data.OrderBy(x => x.Roles);}
            else if (model.iSortCol_0 == 3 && model.sSortDir_0 == "desc"){data = data.OrderByDescending(x => x.Roles);}

            if (model.iSortCol_0 == 4 && model.sSortDir_0 == "asc"){data = data.OrderBy(x => x.IsActive);}
            else if (model.iSortCol_0 == 4 && model.sSortDir_0 == "desc"){data = data.OrderByDescending(x => x.IsActive);}

            
            //Global Search functionality

            if (!(string.IsNullOrEmpty(model.sSearch)) && !(string.IsNullOrWhiteSpace(model.sSearch)))
            {
                data = data.Where(x => x.FullName.Contains(model.sSearch) 
                                    || x.UserName.Contains(model.sSearch) 
                                    || x.Roles.Contains(model.sSearch)                                     
                                    );
            }

            
            if (!(string.IsNullOrEmpty(model.sSearch_1)) && !(string.IsNullOrWhiteSpace(model.sSearch_1)))
            {
                data = data.Where(x => x.UserName.Contains(model.sSearch_1));
            }            
            if (!(string.IsNullOrEmpty(model.sSearch_2)) && !(string.IsNullOrWhiteSpace(model.sSearch_2)))
            {
                data = data.Where(x => x.FullName.Contains(model.sSearch_2));
            }            
            if (!(string.IsNullOrEmpty(model.sSearch_3)) && !(string.IsNullOrWhiteSpace(model.sSearch_3)))
            {
                data = data.Where(x => x.Roles.Contains(model.sSearch_3));
            }            
            
            //etc....
            var count = data.Count();

            var emplist = data.Skip(model.iDisplayStart).Take(model.iDisplayLength).ToList();
            var result = new
            {
                iTotalRecords = emplist.Count(),//records per page
                iTotalDisplayRecords = count, //total table count
                aaData = emplist //employee list

            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CrudAction(Guid? id)
        {
            ViewBag.Id = id;
            UserRoleModel data =this.userService.GetUserRoleModelForCreate();
            this.PeopleIds();
            if (id != null)
            {
                data = this.userService.GetUserRoleById(id.Value);
            }            
            return PartialView("CrudAction", data);
        }

        [HttpPost]
        public ActionResult SetCrudAction(UserRoleModel dataModel)
        {
            if (this.userService.UserNameExists(dataModel.UserName, dataModel.Id))
            {
                this.ModelState.AddModelError("UserName", Resources.Resource.DuplicatedUserName);
            }

            //if (!dataModel.Projects.Any(r => r.IsSelected))
            //{
            //    this.ModelState.AddModelError("Projects", Resources.Resource.NoRoleSelectedForUser);
            //}
            ModelState.Remove("Id");
            ModelState.Remove("Password");
            if (dataModel.Id != null)
            {
                if (!string.IsNullOrWhiteSpace(dataModel.Password) && dataModel.Password.Length < 6 && dataModel.Password!=null)
                {
                    this.ModelState.AddModelError("Password", Resources.Resource.ShortPasswordLength);
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(dataModel.Password) || dataModel.Password==null)                    
                {
                    this.ModelState.AddModelError("Password", Resources.Resource.FieldIsRequired);
                }
                if (dataModel.Password.Length < 6)
                {
                    this.ModelState.AddModelError("Password", Resources.Resource.ShortPasswordLength);
                }
            }

            //IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    if (dataModel.Id != null)
                    {
                        this.userService.UpdateUserRole(dataModel);
                    }
                    else
                    {
                        this.userService.AddUserRoleModel(dataModel);
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
                this.PeopleIds();
                return PartialView("CreateOrEdit");
            }
            else
            {
                this.PeopleIds();
                return PartialView("CreateOrEdit", dataModel);
            }

            

        }


    }
}