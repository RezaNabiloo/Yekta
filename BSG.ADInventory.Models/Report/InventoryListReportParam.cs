using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.Report
{
    public class InventoryListReportParam
    {

        [Display(ResourceType = typeof(Resource), Name = "Province")]
        public long ProvinceId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Township")]
        public long TownshipId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Branch")]
        public long BranchId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Mat")]
        public long MatId { get; set; }

    }
}
