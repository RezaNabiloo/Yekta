using BSG.ADInventory.Common.Enum;
using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.PurchaseDoc
{
    public class PurchaseDocInvTransactionListModel
    {
        [Display(ResourceType = typeof(Resource), Name = "PurchaseDocId")]
        public long PurchaseDocId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "DocNo")]
        public string DocNo { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "InvDocType")]
        public string InvDocType { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Status")]
        public InvDocStatus InvDocStatus { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Status")]
        public string InvDocStatusTitle { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Project")]
        public string Project { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "MatCode")]
        public long MatId { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "MatTitle")]
        public string MatTitle { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MatUnit")]
        public string MatUnit { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Amount")]
        public float Amount { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Source")]
        public string Source { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Dest")]
        public string Dest { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "CreateTime")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime CreateTime { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "ConfirmTime")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? ConfirmTime { get; set; }

    }
}
