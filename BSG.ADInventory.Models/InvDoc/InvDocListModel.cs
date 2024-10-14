using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BSG.ADInventory.Resources;
using BSG.ADInventory.Models.InvDocDetail;
using BSG.ADInventory.Common.Enum;

namespace BSG.ADInventory.Models.InvDoc
{
    public class InvDocListModel
    {

        public long Id { get; set; }

        public long InvDocTypeId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string InvDocType { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "DocNo")]
        public string DocNo { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "PurchaseDocId")]
        public long? PurchaseDocId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Status")]
        public InvDocStatus InvDocStatus { get; set; }

        public string ReferenceInternalDocNo { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "CarType")]
        public string CarType { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "PlateNumber")]
        public string CarPlateNumer { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "DriverName")]
        public string DriverName { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Operator")]
        public string Operator { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Project")]
        public string Project { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Source")]
        public string Source { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Source")]
        public string Dest { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Provider")]
        public string Provider { get; set; }

        public string Reciver { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "CreateTime")]        
        public DateTime CreateTime { get; set; }
        public int Attachments { get; set; }

        public string ItemsSummary { get; set; }
    }
}
