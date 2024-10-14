
using BSG.ADInventory.Models.MatOrderDetail;
using BSG.ADInventory.Models.PurchaseDocDetail;
using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BSG.ADInventory.Models.PurchaseDoc
{
    public class PurchaseDocDataModel
    {
        private ICollection<PurchaseDocDetailDataModel> details;

        public long Id { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Provider")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public long ProviderId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "AggregatedPurchaseDoc")]
        public bool IsAggregated { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Project")]        
        public long ProjectId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "AgentUser")]
        public Guid AgentUserId { get; set; }

        public virtual ICollection<PurchaseDocDetailDataModel> Details
        {
            get { return this.details ?? (this.details = new HashSet<PurchaseDocDetailDataModel>()); }
            set { this.details = value; }
        }

    }
}
