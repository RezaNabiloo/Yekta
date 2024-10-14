namespace BSG.ADInventory.Models.Role
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using BSG.ADInventory.Resources;

    public class RolePermissionModel
    {
        private List<PermissionIdModel> permissions;
        
        public Guid Id { get; set; }       

        public List<PermissionIdModel> Permissions
        {
            get { return this.permissions ?? (this.permissions = new List<PermissionIdModel>()); }
            set { this.permissions = value; }
        }
    }
}
