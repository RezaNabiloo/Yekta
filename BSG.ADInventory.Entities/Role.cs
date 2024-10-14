namespace BSG.ADInventory.Entities
{
    using BSG.ADInventory.Resources;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using Zcf.Core.Models;
    using Zcf.Security;   

    public class Role : IRole, ICreateTime, ILastUpdateTime, IGuidKey
    {
        private ICollection<User> users;
        private ICollection<RolesInPermission> rolesInPermission;
        private ICollection<MenusInRole> menusInRole;
        private ICollection<RoleMenu> roleMenus;


        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(BSG.ADInventory.Resources.Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "Title")]
        [StringLength(256)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is builtin.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is builtin; otherwise, <c>false</c>.
        /// </value>
        public bool IsBuiltin { get; set; }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public virtual ICollection<User> Users
        {
            get
            {
                return this.users ?? (this.users = new HashSet<User>());
            }
        }

        /// <summary>
        /// Gets the roles in permissions.
        /// </summary>
        /// <value>
        /// The roles in permissions.
        /// </value>
        public virtual ICollection<RolesInPermission> RolesInPermissions
        {
            get
            {
                return this.rolesInPermission ?? (this.rolesInPermission = new HashSet<RolesInPermission>());
            }
        }

        /// <summary>
        /// Gets or sets the create time.
        /// </summary>
        /// <value>
        /// The create time.
        /// </value>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Gets or sets the last update time.
        /// </summary>
        /// <value>
        /// The last update time.
        /// </value>
        public DateTime LastUpdateTime { get; set; }

        IEnumerable<IRolesInPermission> IRole.RolesInPermissions
        {
            get { return this.RolesInPermissions; }
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        IEnumerable<IUser> IRole.Users
        {
            get { return this.Users; }
        }

        public virtual ICollection<MenusInRole> MenusInRole
        {
            get
            {
                return this.menusInRole ?? (this.menusInRole = new HashSet<MenusInRole>());
            }
        }
        public virtual ICollection<RoleMenu> RoleMenus
        {
            get { return this.roleMenus ?? (this.roleMenus = new HashSet<RoleMenu>()); }
            set { this.roleMenus = value; }
        }
    }
}
