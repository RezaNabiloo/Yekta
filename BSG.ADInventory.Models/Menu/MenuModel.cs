using BSG.ADInventory.Models.Role;
using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.Menu
{
    public class MenuModel
    {
        private List<PermissionCheckBoxItem> permissions;
        public long Id { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "ParentMenu")]
        public long? ParentMenuId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string Title { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "SpecifiedUrl")]
        public string SpecifiedUrl { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "IconClass")]
        public string IconClass { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "ViewOrder")]
        public int ViewOrder { get; set; }

        public bool IsAdminMenu { get; set; }
        public List<PermissionCheckBoxItem> Permissions
        {
            get { return this.permissions ?? (this.permissions = new List<PermissionCheckBoxItem>()); }
            set { this.permissions = value; }
        }
    }
}
