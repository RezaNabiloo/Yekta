
using BSG.ADInventory.Models.MatOrderDetail;
using BSG.ADInventory.Models.PurchaseDocDetail;
using BSG.ADInventory.Resources;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BSG.ADInventory.Models.PurchaseDoc
{
    public class PurchaseDocFollowModel
    {
        public long MatId{ get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MatTitle")]        
        public string MatTitle{ get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Amount")]
        public float Amount { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MatUnit")]        
        public string MatUnit { get; set; }
        

    }
}
