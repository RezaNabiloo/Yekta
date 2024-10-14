namespace BSG.ADInventory.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using BSG.ADInventory.Resources;
    

    public class Menu : BaseEntity
    {
        private ICollection<Menu> childMenus;
        private ICollection<MenusInRole> menusInRole;
        private ICollection<UserMenu> userMenus;

        private ICollection<RoleMenu> roleMenus;
        private ICollection<PermissionsInMenu> permissionMenus;
        private ICollection<UserShortcut> userShortcuts;

        public Menu()
        {
            this.ViewOrder = 1;
        }
        public long? ParentMenuId { get; set; }
        public virtual Menu ParentMenu{ get; set; }
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string Title { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "SpecifiedUrl")]
        public string SpecifiedUrl { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "IconClass")]
        [AllowHtml]
        public string IconClass { get; set; }
        public int ViewOrder { get; set; }

        /// <summary>
        /// Gets or sets the provinces.
        /// </summary>
        /// <value>
        /// The provinces.
        /// </value>
        public virtual ICollection<Menu> ChildMenus {
            get
            {
                return this.childMenus ?? (this.childMenus = new HashSet<Menu>());
            }
            set
            {
                this.childMenus = value;
            }
        }
        public virtual ICollection<MenusInRole> MenusInRole
        {
            get
            {
                return this.menusInRole ?? (this.menusInRole = new HashSet<MenusInRole>());
            }
        }

        public virtual ICollection<UserMenu> UserMenus
        {
            get
            {
                return this.userMenus ?? (this.userMenus = new HashSet<UserMenu>());
            }
            set
            {
                this.userMenus= value;
            }
        }
        public virtual ICollection<RoleMenu> RoleMenus
        {
            get
            {
                return this.roleMenus ?? (this.roleMenus = new HashSet<RoleMenu>());
            }

            set
            {
                this.roleMenus = value;
            }
        }

        public virtual ICollection<PermissionsInMenu> PermissionMenus
        {
            get
            {
                return this.permissionMenus ?? (this.permissionMenus = new HashSet<PermissionsInMenu>());
            }

            set
            {
                this.permissionMenus = value;
            }
        }
        public virtual ICollection<UserShortcut> UserShortcuts
        {
            get
            {
                return this.userShortcuts ?? (this.userShortcuts = new HashSet<UserShortcut>());
            }

            set
            {
                this.userShortcuts = value;
            }
        }


    }
}
