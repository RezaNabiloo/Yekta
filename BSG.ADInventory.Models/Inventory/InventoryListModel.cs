using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.Inventory
{
    public class InventoryListModel
    {

        [Display(ResourceType = typeof(Resource), Name = "Id")]
        public long Id { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string Title { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Project")]
        public string Project { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Province")]
        public string Province { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Township")]
        public string Township { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "IsCapital")]
        public bool IsActive { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "IsActive")]
        public string Address{ get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Lat")]

        public double? Lat { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Lon")]
        public double? Lon { get; set; }

    }
}
