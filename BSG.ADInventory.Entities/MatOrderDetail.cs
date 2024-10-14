namespace BSG.ADInventory.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Resources;

    public class MatOrderDetail : BaseEntity
    {
        private ICollection<PurchaseDocDetail> purchaseDocDetails;

        public long MatOrderId { get; set; }
        public virtual MatOrder MatOrder{ get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "Material")]
        public long MatId { get; set; }
        public virtual Mat Mat { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "RequiredAmount")]
        public float RequiredAmount { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "ConfirmedAmount")]
        public float ConfirmedAmount { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "RequestDescription")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "ConfirmDescription")]
        public string ConfirmDescription { get; set; }

        public virtual ICollection<PurchaseDocDetail> PurchaseDocDetails
        {
            get { return this.purchaseDocDetails ?? (this.purchaseDocDetails = new HashSet<PurchaseDocDetail>()); }
            set { this.purchaseDocDetails = value; }
        }


    }
}
