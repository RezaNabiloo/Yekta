
namespace BSG.ADInventory.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Resources;

    public class PurchaseDocAttachment : BaseEntity
    {

        public long PurchaseDocId { get; set; }
        public virtual PurchaseDoc PurchaseDoc { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string Title { get; set; }

        public string FilePath { get; set; }
        
       
    }
}
