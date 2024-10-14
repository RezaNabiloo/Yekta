namespace BSG.ADInventory.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using Zcf.Core;
    using Zcf.Data;
    using Zcf.Security;
    using Zcf.Paging;
    using Zcf.Data.Extensions;
    using System.Reflection;
    using System.Diagnostics;
    using BSG.ADInventory.Models.Role;
    using Zcf.Data.CustomFiltering;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Models.Menu;

    /// <summary>
    /// Create Service For role" />
    /// </summary>
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly BSG.ADInventory.Data.IRoleRepository roleRepository;
        private readonly IMenuRepository menuRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="repository">The repository.</param>
        public RoleService(IUnitOfWork unitOfWork, BSG.ADInventory.Data.IRoleRepository repository, IMenuRepository menuRepository)
        {
            this.unitOfWork = unitOfWork;
            this.roleRepository = repository;
            this.menuRepository = menuRepository;
        }

        /// <summary>
        /// Adds the specified model.
        /// </summary>
        /// <param name="model"> The model. </param>
        public void Add(RoleModel model)
        {
            var roleEntity = new BSG.ADInventory.Entities.Role
            {
                Name = model.Name
            };

            foreach (var permission in model.Permissions.Where(p => p.IsSelected))
            {
                roleEntity.RolesInPermissions.Add(new BSG.ADInventory.Entities.RolesInPermission { PermissionId = permission.PermissionId });
            }

            this.roleRepository.Add(roleEntity);
            this.unitOfWork.Commit();
        }

        /// <summary>
        /// Removes all the specified models.
        /// </summary>
        /// <param name="models"></param>
        public void Remove(IEnumerable<RoleModel> models)
        {
            try
            {
                this.roleRepository.Remove(models);
                this.unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException.InnerException as SqlException;
                if (innerException != null && innerException.Number == 547)
                {
                    this.unitOfWork.RollBack();
                }

                throw;
            }
        }

        /// <summary>
        /// Removes the specified model.
        /// </summary>
        /// <param name="model"> The model. </param>
        public void Remove(RoleModel model)
        {
            try
            {
                this.roleRepository.Remove(r => r.Id == model.Id);
                this.unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException.InnerException as SqlException;
                if (innerException != null && innerException.Number == 547)
                {
                    this.unitOfWork.RollBack();
                }

                throw;
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model"> The model. </param>
        public void Update(RoleModel model)
        {
            BSG.ADInventory.Entities.Role roleEntity = this.roleRepository.GetItemByKey(model.Id);
            if (roleEntity != null)
            {
                roleEntity.Name = model.Name;

                roleEntity.RolesInPermissions.Clear();
                
                foreach (var permission in model.Permissions.Where(r => r.IsSelected))
                {
                    roleEntity.RolesInPermissions.Add(new RolesInPermission { Role=roleEntity, PermissionId = permission.PermissionId });
                }

                this.roleRepository.Update(roleEntity);
                this.unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns> </returns>
        public IEnumerable<RoleModel> GetAllItems()
        {
            return this.roleRepository.GetAllItems()
                .Select(r => this.ConvertToRoleModel(r));
        }

        /// <summary>
        /// Gets the total number of items.
        /// </summary>
        /// <returns> </returns>
        public int GetItemCount()
        {
            return this.roleRepository.GetItemCount();
        }

        /// <summary>
        /// Gets an <see cref="Zcf.Core.Models.IPaginated"/> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IPagedQueryable<RoleModel> GetItems(Models.Role.Criteria criteria)
        {
            var query = this.roleRepository.Query;
            if (!string.IsNullOrWhiteSpace(criteria.Name))
            {
                // Check generated SQL for criteria.Name.Trim().ToLower() phrase
                query = query.Where(r => r.Name.ToLower().Contains(criteria.Name.Trim().ToLower()));
            }

            var paginatedEntities = query.ToPagedEnumerable(criteria);

            var paginatedModels = new PagedQueryable<Models.Role.RoleModel>(
                paginatedEntities.Data.Select(r => this.ConvertToRoleModel(r)).ToList().AsQueryable(),
                0,
                paginatedEntities.TotalRowcount);

            return paginatedModels;
        }

        public IPagedQueryable<RoleModel> GetItems(int? page, int? count, DataServiceFilter filters, IList<DataServiceSort> sort)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets paginated items supporting sorting.
        /// </summary>
        /// <param name="startIndex"> The start index. </param>
        /// <param name="itemCount"> The item count. </param>
        /// <param name="sortDescriptions"> The sort descriptions. </param>
        /// <returns> </returns>
        //public IPagedEnumerable<Models.Role.RoleModel> GetItems(int startIndex, int itemCount, SortDescription[] sortDescriptions = null)
        //{
        //    return this.roleRepository.Query
        //        .ToPage(startIndex, itemCount, sortDescriptions)
        //        .AsEnumerable()
        //        .Select(r => this.ConvertToRoleModel(r)).ToList();
        //}

        /// <summary>
        /// Gets an item by its key(s).
        /// </summary>
        /// <param name="keys"> The key(s). </param>
        /// <returns> </returns>
        public RoleModel GetItemByKey(params object[] keys)
        {
            var role = this.roleRepository.GetItemByKey(keys);
            if (role == null)
            {
                return null;
            }

            return this.ConvertToRoleModel(role);
        }

        /// <summary>
        /// Reloads the specified model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public RoleModel Reload(Models.Role.RoleModel entity)
        {
            return this.ConvertToRoleModel(this.roleRepository.GetItemByKey(entity.Id), entity);
        }

        /// <summary>
        /// Checks if the specified role name is duplicated.
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="excludedRoleId"></param>
        /// <returns></returns>
        public bool CheckRoleNameDuplicate(string roleName, Guid? excludedRoleId = null)
        {
            return this.roleRepository.CheckRoleNameDuplicate(roleName, excludedRoleId);
        }

        /// <summary>
        /// Gets the role model for create.
        /// </summary>
        /// <returns></returns>
        public RoleModel GetRoleModelForCreate()
        {
            var roleModel = new Models.Role.RoleModel { Id=Guid.Empty};
            var permissions = Permissions;//Zcf.Security.PermissionContainer.Permissions;
            foreach (var permission in permissions)
            {
                roleModel.Permissions.Add(
                    new Models.Role.PermissionCheckBoxItem
                    {
                        PermissionId = permission.PermissionId,
                        IsSelected = false,
                        Description = permission.Description,                        
                        Category = permission.Category,
                        CategoryId=permission.CategoryId,
                        HelpDescription=permission.HelpDescription
                    });
            }

           

            return roleModel;
        }

        /// <summary>
        /// Converts the role entity to role model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        private RoleModel ConvertToRoleModel(Role entity, RoleModel model = null)
        {
            if (model == null)
            {
                model = new RoleModel();
            }

            model.Id = entity.Id;
            model.Name = entity.Name;
            model.IsBuiltin = entity.IsBuiltin;

            var permissions = Permissions;
            model.Permissions.Clear();
            //model.Menus.Clear();
            foreach (var permission in permissions)
            {
                model.Permissions.Add(
                    new Models.Role.PermissionCheckBoxItem
                    {
                        PermissionId = permission.PermissionId,
                        IsSelected = entity.RolesInPermissions.Any(p => p.PermissionId == permission.PermissionId),
                        Description = permission.Description,
                        CategoryId=permission.CategoryId,
                        Category=permission.Category,
                        HelpDescription=permission.HelpDescription
                    });
            }
            return model;
        }

        private static readonly object SyncObject = new object();
        private static List<PermissionItem> permissions;

        ///// <summary>
        ///// Gets the permissions.
        ///// </summary>
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

        /// <summary>
        /// Loads the permissions.
        /// </summary>
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
                //FieldInfo[] fields1 = assembly.GetType().GetFields(BindingFlags.Public | BindingFlags.Static
                //                              BindingFlags.NonPublic |
                //                              BindingFlags.Instance);
                //PropertyInfo[] propertyInfos;
                //propertyInfos = typeof(Common.Security.Permissions).GetProperties(BindingFlags.Public | BindingFlags.Static);

                //var property = assembly.GetType("Permissions")
                //                       .GetFields(BindingFlags.Public | BindingFlags.Static);

                var attribute = typeof(BSG.ADInventory.Common.Security.Permissions)
                                       .GetFields(BindingFlags.Public | BindingFlags.Static);

                //var attribute =
                //    (SecurityRegulatorAttribute)assembly.GetCustomAttributes(typeof(SecurityRegulatorAttribute), false).FirstOrDefault();

                if (attribute != null)
                {
                    //FieldInfo[] fields = attribute.PermissionContainerType.GetFields(BindingFlags.Public | BindingFlags.Static);

                    List<PermissionItem> permissionItems =
                        attribute.Where(p => p.FieldType == typeof(PermissionItem)).Select(p => (PermissionItem)p.GetValue(null)).OrderBy(p=>p.CategoryId).ToList();

                    // Add all found permissions to permission list
                    permissions.AddRange(permissionItems);
                }
            }

            Debug.WriteIf(permissions.Count == 0, "PermissionContainer.LoadPermissions called but no permission found.");
        }

        public List<RoleModel> GetRoleAll()
        {
            List<RoleModel> data = new List<RoleModel>();
            var roles = this.roleRepository.GetAllItems();

            foreach (var item in roles)
            {
                data.Add(this.ConvertToRoleModel(item));
            }

            return data;
        }
        public RoleModel AddRole(RoleModel model)
        {
            var roleEntity = new Role
            {
                Name = model.Name
                
            };

            foreach (var permission in model.Permissions.Where(p => p.IsSelected))
            {
                roleEntity.RolesInPermissions.Add(new BSG.ADInventory.Entities.RolesInPermission { PermissionId = permission.PermissionId });
            }
          
            //foreach (var menu in model.Menus.Where(m => m.IsSelected))
            //{
            //    roleEntity.MenusInRole.Add(new MenusInRole { MenuId = menu.MenuId });
            //}
            this.roleRepository.Add(roleEntity);
            this.unitOfWork.Commit();

            return this.ConvertToRoleModel(roleEntity);
        }
        public RoleModel GetRoleById(Guid id)
        {
            var role = this.roleRepository.GetItemByKey(id);
            if (role==null)
            {
                return null;
            }          

            return this.ConvertToRoleModel(role);
        }
        public RoleModel UpdateRole(RoleModel model)
        {
            BSG.ADInventory.Entities.Role roleEntity = this.roleRepository.GetItemByKey(model.Id);
            if (roleEntity != null)
            {
                roleEntity.Name = model.Name;

                roleEntity.RolesInPermissions.Clear();

                foreach (var permission in model.Permissions.Where(r => r.IsSelected))
                {
                    roleEntity.RolesInPermissions.Add(new BSG.ADInventory.Entities.RolesInPermission { PermissionId = permission.PermissionId });
                }
                //roleEntity.MenusInRole.Clear();
                //foreach (var menu in model.Menus.Where(r => r.IsSelected))
                //{
                //    roleEntity.MenusInRole.Add(new MenusInRole { MenuId = menu.MenuId });
                //}
                this.roleRepository.Update(roleEntity);
                this.unitOfWork.Commit();

                return this.ConvertToRoleModel(roleEntity);
            }
            return null;
        }

        public RoleModel UpdateRolePermission(RolePermissionModel model)
        {
            BSG.ADInventory.Entities.Role roleEntity = this.roleRepository.GetItemByKey(model.Id);
            if (roleEntity != null)
            {
                
                roleEntity.RolesInPermissions.Clear();

                foreach (var permission in model.Permissions)
                {
                    roleEntity.RolesInPermissions.Add(new RolesInPermission { PermissionId = permission.PermissionId });
                }

                this.roleRepository.Update(roleEntity);
                this.unitOfWork.Commit();

                return this.ConvertToRoleModel(roleEntity);
            }
            return null;
        }

        public Guid? RemoveRole(Guid id)
        {
            var model = this.roleRepository.GetItemByKey(id);
            if (model==null)
            {
                return null;
            }
            try
            {
                this.roleRepository.Remove(r => r.Id == model.Id);
                this.unitOfWork.Commit();
                return model.Id;
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException.InnerException as SqlException;
                if (innerException != null && innerException.Number == 547)
                {
                    this.unitOfWork.RollBack();
                }
                return null;
                //throw;
            }
        }

        public IEnumerable<RoleListModel> GetDataList()
        {
            var data = this.roleRepository.GetAllItems().Select(t => new RoleListModel
            {
                Id = t.Id,
                Name = t.Name
            }).ToList();

            return (data);
        }
    }
}
