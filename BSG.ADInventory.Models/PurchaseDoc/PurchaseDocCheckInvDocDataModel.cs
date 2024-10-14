
using BSG.ADInventory.Models.MatOrderDetail;
using BSG.ADInventory.Models.PurchaseDocDetail;
using BSG.ADInventory.Resources;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BSG.ADInventory.Models.PurchaseDoc
{
    public class PurchaseDocCheckInvDocDataModel
    {
        

        public long Id { get; set; }        
        public bool Result { get; set; }        
        public bool IsAggregated { get; set; }        
        public long ProjectId { get; set; }
        public long ProviderId{ get; set; }
        public string ErrorMessage { get; set; }
    }
}
