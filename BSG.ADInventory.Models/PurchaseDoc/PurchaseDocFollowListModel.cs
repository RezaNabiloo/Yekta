using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.PurchaseDoc
{
    public class PurchaseDocFollowListModel
    {
        [Display(ResourceType = typeof(Resource), Name = "PurchaseDocId")]
        public long? PurchaseDocId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Aggregated")]
        public bool IsAggregated { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "Project")]
        public string Project { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MatCode")]
        public long MatId { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "MatTitle")]
        public string MatTitle { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MatUnit")]
        public string MatUnit { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "PurchaseQty")]
        public float PurchaseQty { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "ReciveQty")]
        public float ReciveQty { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "DistribQty")]
        public float DistribQty { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "ExitQty")]
        public float ExitQty { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "CreateTime")]
        public DateTime CreateTime { get; set; }

    }
}
