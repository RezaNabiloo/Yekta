namespace BSG.ADInventory.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Common.Enum;
    using BSG.ADInventory.Resources;

    public class InvDocDetail : BaseEntity
    {
        public long InvDocId { get; set; }
        public virtual InvDoc InvDoc { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Material")]
        public long MatId { get; set; }
        public virtual Mat Mat { get; set; }

        
        [Display(ResourceType = typeof(Resource), Name = "Qty")]
        public float Amount { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "RealQty")]
        public float RealAmount { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "Description")]
        public string Description { get; set; }


        public long? ProjectDetailId { get; set; }
        public virtual ProjectDetail ProjectDetail { get; set; }

    }
}
