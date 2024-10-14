using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.PurchaseDoc
{
    public class PurchaseDocOnWayListModel
    {
        [Display(ResourceType = typeof(Resource), Name = "PurchaseDocId")]
        public long PurchaseDocId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Aggregated")]
        public bool IsAggregated { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "Project")]
        public string ProjectTitle{ get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MatCode")]
        public long MatId { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "MatTitle")]
        public string MatTitle { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MatUnit")]
        public string MatUnit { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "PurchaseQty")]
        public double PurchaseQty { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "ReciveQty")]
        public double ReciveQty { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "CreateTime")]
        public DateTime CreateTime { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "ProviderName")]
        public string ProviderName { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "RequestCode")]
        public long? RequestId { get; set; }


    }
}
