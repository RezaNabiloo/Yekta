using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.MatPurchase
{
    public class MatPurchaseListModel
    {
        public long Id { get; set; }
        public long VDocId { get; set; }
        public long InvDocId { get; set; }
        public string DocNo { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Material")]
        public long? MatId { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Code")]
        public string MatCode { get; set; }

        
        [Display(ResourceType = typeof(Resource), Name = "Material")]
        public string MatTitle { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Qty")]
        public float Qty { get; set; }

        public string MatUnit { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "IsFreight")]

        public bool IsFreight { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "PurchasePrice")]        
        public long PurchasePrice { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MatGroup")]        
        public long FreightPrice { get; set; }
    }
}
