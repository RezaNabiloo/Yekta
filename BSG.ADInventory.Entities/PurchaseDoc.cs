namespace BSG.ADInventory.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Resources;

    public class PurchaseDoc : BaseEntity
    {
        private ICollection<PurchaseDocDetail> purchaseDocDetails;
        private ICollection<PurchaseDocAttachment> purchaseDocAttachments;
        private ICollection<InvDoc> invDocs;
        

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "Provider")]
        public long ProviderId { get; set; }

        //public virtual Provider Provider{ get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string Description { get; set; }

        public bool  IsFinished { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "AggregatedPurchaseDoc")]
        public bool IsAggregated { get; set; }

        
        [Display(ResourceType = typeof(Resource), Name = "Project")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public long ProjectId { get; set; }
        public virtual Project Project { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "AgentUser")]
        public Guid AgentUserId { get; set; }
        public virtual User AgentUser { get; set; }

        /// <summary>
        /// Gets or sets the provinces.
        /// </summary>
        /// <value>
        /// The provinces.
        /// </value>
        public virtual ICollection<PurchaseDocDetail> PurchaseDocDetails
        {
            get { return this.purchaseDocDetails ?? (this.purchaseDocDetails = new HashSet<PurchaseDocDetail>());}
            set { this.purchaseDocDetails = value;}
        }

        public virtual ICollection<PurchaseDocAttachment> PurchaseDocAttachments
        {
            get { return this.purchaseDocAttachments ?? (this.purchaseDocAttachments = new HashSet<PurchaseDocAttachment>()); }
            set { this.purchaseDocAttachments = value; }
        }

        public virtual ICollection<InvDoc> InvDocs
        {
            get { return this.invDocs?? (this.InvDocs= new HashSet<InvDoc>()); }
            set { this.invDocs = value; }
        }

    }
}
