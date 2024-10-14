namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Entities;
    using Data;
    using DocumentFormat.OpenXml.Drawing.Diagrams;
    using Models.Menu;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Security;
    using Zcf.Services;


    /// <summary>
    /// Create Service For Country" />
    /// </summary>
    public class MenuService : CrudService<Menu>, IMenuService
    {
        //private readonly IUnitOfWork unitOfWork;
        private readonly IMenuRepository menuRepository;
        private readonly ISqlCommandRepository sqlCommandRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="menuRepository"></param>
        public MenuService(IUnitOfWork unitOfWork, IMenuRepository menuRepository,
            ISqlCommandRepository sqlCommandRepository)
            : base(unitOfWork, menuRepository)
        {
            this.menuRepository = menuRepository;
            this.sqlCommandRepository = sqlCommandRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<Menu> GetItems(PagedQueryable criteria)
        {
            var data = this.menuRepository.Query;
            return data.ToPagedQueryable(criteria);
        }

        public MenuModel AddMenu(MenuModel dataModel)
        {
            Menu model = new Menu
            {

                Title = dataModel.Title,
                ParentMenuId = dataModel.ParentMenuId
            };

            this.menuRepository.Add(model);
            this.UnitOfWork.Commit();
            return GenerateModel(model);

        }

        public void UpdateMenu(MenuModel model)
        {
            Menu menu = this.menuRepository.GetItemByKey(model.Id);
            if (menu!= null)
            {
                menu.Title = model.Title;                
                menu.ViewOrder = model.ViewOrder;
                menu.SpecifiedUrl = model.SpecifiedUrl;
                menu.IconClass = model.IconClass;
                menu.ParentMenuId = model.ParentMenuId;

                menu.PermissionMenus.Clear();

                foreach (var permission in model.Permissions.Where(r => r.IsSelected))
                {
                    menu.PermissionMenus.Add(new PermissionsInMenu { Menu = menu, PermissionId = permission.PermissionId, CreateTime = DateTime.Now, LastUpdateTime = DateTime.Now });
                }

                this.menuRepository.Update(menu);
                this.UnitOfWork.Commit();
            }

            
        }

        public PublicMenuModel AddPublicMenu(PublicMenuModel dataModel)
        {
            Menu model = new Menu
            {

                Title = dataModel.Title,
                
                ParentMenuId = dataModel.ParentMenuId,
                ViewOrder = dataModel.ViewOrder,
                SpecifiedUrl = dataModel.SpecifiedUrl,
                IconClass = dataModel.IconClass
            };

            this.menuRepository.Add(model);
            this.UnitOfWork.Commit();
            return dataModel;

        }

        public void UpdatePublicMenu(PublicMenuModel model)
        {
            Menu menu = this.menuRepository.GetItemByKey(model.Id);
            if (menu != null)
            {
                menu.Title = model.Title;                
                menu.ViewOrder = model.ViewOrder;
                menu.SpecifiedUrl = model.SpecifiedUrl;
                menu.IconClass = model.IconClass;
                menu.ParentMenuId = model.ParentMenuId;                
                this.menuRepository.Update(menu);
                this.UnitOfWork.Commit();
            }


        }

        public long RemoveMenu(long id)
        {
            try
            {
                var model = this.menuRepository.GetItemByKey(id);
                this.menuRepository.Remove(model);
                this.UnitOfWork.Commit();
                return id;
            }
            catch (Exception ex)
            {
                var reza = ex;
                // return null;
            }

            return 0;

        }
        private MenuModel GenerateModel(Menu model)
        {
            MenuModel data = new MenuModel
            {
                Id = model.Id,
                Title = model.Title,
                ParentMenuId = model.ParentMenuId,
                IconClass = (model.ChildMenus.Count > 0 ? "activefolder" : "file")
                //,isDirectory = (model.ChildMenus.Count > 0 ? true : false)
            };
            return data;
        }

        public MenuModel GetMenuModelForCreate()
        {
            
            MenuModel model = new MenuModel { };


            var permissions = Permissions;
            model.Permissions.Clear();

            foreach (var permission in permissions)
            {
                model.Permissions.Add(
                    new Models.Role.PermissionCheckBoxItem
                    {
                        PermissionId = permission.PermissionId,
                        IsSelected = false,
                        Description = permission.Description,
                        CategoryId = permission.CategoryId,
                        Category = permission.Category,
                        HelpDescription = permission.HelpDescription
                    });
            }
            return model;
        }
        public MenuModel GetMenuModelForEdit(long id)
        {
            var menu = this.menuRepository.GetItemByKey(id);
            MenuModel model = new MenuModel { };

            if (menu!=null)
            {
                model.Id = menu.Id;
                model.Title = menu.Title;
                model.SpecifiedUrl = menu.SpecifiedUrl;
                model.IconClass = menu.IconClass;
                model.ViewOrder = menu.ViewOrder;
                model.ParentMenuId = menu.ParentMenuId;
            }
            
            var permissions = Permissions;
            model.Permissions.Clear();
            
            foreach (var permission in permissions)
            {
                model.Permissions.Add(
                    new Models.Role.PermissionCheckBoxItem
                    {
                        PermissionId = permission.PermissionId,
                        IsSelected = menu.PermissionMenus.Any(p => p.PermissionId == permission.PermissionId),
                        Description = permission.Description,
                        CategoryId = permission.CategoryId,
                        Category = permission.Category,
                        HelpDescription = permission.HelpDescription
                    });
            }
            return model;
        }
        
        public static List<PermissionItem> Permissions
        {
            get
            {
                // Checking out of lock block to increase performance
                if (permissions != null)
                {
                    return permissions;
                }

                lock (SyncObject)
                {
                    if (permissions == null)
                    {
                        LoadPermissions();
                    }

                    return permissions;
                }
            }
        }
        private static readonly object SyncObject = new object();
        private static List<PermissionItem> permissions;

        private static void LoadPermissions()
        {
            // Search among the assemblies which defined the SecurityRegulatorAttribute.
            IEnumerable<Assembly> assemblies =
            //AppDomain.CurrentDomain.GetAssemblies().Where(a => a.IsDefined(typeof(SecurityRegulatorAttribute), false));
            AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.Contains("BSG.ADInventory.Common"));

            // Create new permission list
            permissions = new List<PermissionItem>();

            // Find PermissionContainerType in the found assemblies.
            foreach (Assembly assembly in assemblies)
            {
                
                var attribute = typeof(Common.Security.Permissions)
                                       .GetFields(BindingFlags.Public | BindingFlags.Static);

                
                if (attribute != null)
                {                   

                    List<PermissionItem> permissionItems =
                        attribute.Where(p => p.FieldType == typeof(PermissionItem)).Select(p => (PermissionItem)p.GetValue(null)).OrderBy(p => p.CategoryId).ToList();

                    // Add all found permissions to permission list
                    permissions.AddRange(permissionItems);
                }
            }

            Debug.WriteIf(permissions.Count == 0, "PermissionContainer.LoadPermissions called but no permission found.");
        }


        public List<MenuListModel> UserAccessMenu(Guid userId)
        {
            var UserId = new SqlParameter();
            UserId.ParameterName = "@UserId";
            UserId.Direction = ParameterDirection.Input;
            UserId.Value = userId.ToString();
            UserId.DbType = DbType.String;
            List<MenuListModel> data = (from t in sqlCommandRepository.ExecuteStoredProcedureList<MenuListModel>("Sp_GetUserAccessMenus @UserId", UserId)
                                    select new MenuListModel
                                    {
                                        Id = t.Id,
                                        ParentMenuId = t.ParentMenuId,
                                        Title = t.Title,
                                        SpecifiedUrl = t.SpecifiedUrl,
                                        IconClass = t.IconClass,
                                        ViewOrder = 0//t.ViewOrder
                                    }
                                          ).ToList();


            return data;
        }

        public PublicMenuModel GetPublicMenuModelForCreate()
        {

            PublicMenuModel model = new PublicMenuModel {};

            return model;
        }
        public PublicMenuModel GetPublicMenuModelForEdit(long id)
        {
            var menu = this.menuRepository.GetItemByKey(id);
            PublicMenuModel model = new PublicMenuModel { };

            if (menu != null)
            {
                model.Id = menu.Id;
                model.Title = menu.Title;
                model.SpecifiedUrl = menu.SpecifiedUrl;
                model.IconClass = menu.IconClass;
                model.ViewOrder = menu.ViewOrder;
                model.ParentMenuId = menu.ParentMenuId;
            }

            return model;
        }

    }
}
