namespace BSG.ADInventory.Models.Role
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using BSG.ADInventory.Models.Menu;
    using BSG.ADInventory.Resources;

    public class RoleModel
    {
        private List<PermissionCheckBoxItem> permissions;
        //private List<MenuCheckBoxItem> menus;

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public Guid? Id { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Display(ResourceType = typeof(Resource), Name = "RoleName")]
        [Zcf.Globalization.DataAnnotations.Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is builtin.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is builtin; otherwise, <c>false</c>.
        /// </value>
        public bool IsBuiltin { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public List<PermissionCheckBoxItem> Permissions
        {
            get { return this.permissions ?? (this.permissions = new List<PermissionCheckBoxItem>()); }
            set { this.permissions = value; }
        }

        //public List<MenuCheckBoxItem> Menus
        //{
        //    get { return this.menus ?? (this.menus = new List<MenuCheckBoxItem>()); }
        //    set { this.menus = value; }
        //}
    }
}
