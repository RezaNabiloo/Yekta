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
    public class PublicMenuModel
    {
        
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

        [Display(ResourceType = typeof(Resource), Name = "ShowInEnContent")]
        public bool ShowInEnContent { get; set; }

    }
}
