using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.PurchaseDocDetail
{
    public class PurchaseDocDetailListModel
    {
        [Display(ResourceType =typeof(Resource), Name ="PurchaseDocId")]
        public long PurchaseDocId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "IsAggregated")]
        public bool IsAggregated { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "PurchaseDocId")]
        public long MatId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MatTitle")]
        public string MatTitle { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MatUnit")]
        public string MatUnit { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Project")]
        public string Project { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "RequestProject")]
        public string RequestProject { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MatOrderId")]
        public long? MatOrderId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "RequestedAmount")]
        public float? RequestedAmount { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "ConfirmedAmount")]
        public float? ConfirmedAmount { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "PurchaseAmount")]
        public float PurchaseAmount { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "RequiredDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? RequiredDate { get; set; }

    }
}
