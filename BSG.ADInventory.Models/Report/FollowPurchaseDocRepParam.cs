using BSG.ADInventory.Common.Enum;
using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.Report
{
    public class FollowPurchaseDocRepParam
    {
        [UIHint("DatePicker")]
        [Display(ResourceType =typeof(Resource), Name ="FromDate")]
        public DateTime? StartDate { get; set; }

        [UIHint("DatePicker")]
        [Display(ResourceType = typeof(Resource), Name = "ToDate")]
        public DateTime? EndDate { get; set; }

        public string StartDateStringVal { get; set; }

        public string EndDateStringVal { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "PurchaseDocId")]
        public long? PurchaseDocId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MatCode")]
        public long? MatId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "ReportType")]
        public int ReportType { get; set; }
    }
}
