
using BSG.ADInventory.Models.MatOrderDetail;
using BSG.ADInventory.Models.PurchaseDocDetail;
using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Zcf.Core.Models;

namespace BSG.ADInventory.Models.PurchaseDoc
{
    public class PurchaseDocListModel
    {
        
        public long Id { get; set; }        
        public string Provider { get; set; }
        public string Project { get; set; }
        public bool IsAggregated{ get; set; }
        public DateTime CreateTime{ get; set; }
        public string CreateUser { get; set; }
        public string ItemsSummary { get; set; }
        public int Attachments { get; set; }

        public string AgentUser{ get; set; }


    }
}
