namespace BSG.ADInventory.Services
{ 	
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.Role;
    using Zcf.Services;	
		
    /// <summary>
    /// Create Interface for RoleService
    /// </summary>
    public interface IRoleService : ICrudService<Models.Role.RoleModel, Models.Role.Criteria>
	{
        /// <summary>
        /// Checks if the specified role name is duplicated.
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="excludedRoleId"></param>
        /// <returns></returns>
        bool CheckRoleNameDuplicate(string roleName, Guid? excludedRoleId = null);

        /// <summary>
        /// Gets the role model for create.
        /// </summary>
        /// <returns></returns>

        List<RoleModel> GetRoleAll();
        RoleModel GetRoleModelForCreate();
        RoleModel GetRoleById(Guid id);
        RoleModel AddRole(RoleModel model);

        RoleModel UpdateRole(RoleModel model);
        RoleModel UpdateRolePermission(RolePermissionModel model);
        Guid? RemoveRole(Guid id);

        IEnumerable<RoleListModel> GetDataList();
    }
}