using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.PurchaseDocDetail
{
    public class PurchaseDocDetailDataModel
    {
        public long Id { get; set; }
        public long PurchaseDocId { get; set; }
        public long? MatOrderId { get; set; }
        public long? MatOrderDetailId { get; set; }
        public long MatId { get; set; }
        public float Amount { get; set; }        
        public string MatInfo { get; set; }
        public string MatUnit { get; set; }

    }
}
