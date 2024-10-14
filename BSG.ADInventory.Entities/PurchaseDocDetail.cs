namespace BSG.ADInventory.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Resources;

    public class PurchaseDocDetail : BaseEntity
    {
        public long PurchaseDocId { get; set; }
        public virtual PurchaseDoc PurchaseDoc{ get; set; }

        //[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "Material")]
        public long? MatOrderDetailId { get; set; }
        public virtual MatOrderDetail MatOrderDetail { get; set; }

        public long MatId { get; set; }
        public virtual Mat Mat { get; set; }

        public float Amount { get; set; }

        public bool Recived { get; set; }
    }
}
