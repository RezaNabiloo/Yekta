namespace BSG.ADInventory.Entities.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;

    public class PurchaseDocAttachmentConfig : EntityTypeConfiguration<PurchaseDocAttachment>
    {
        public PurchaseDocAttachmentConfig()
        {
            this.HasKey(c => c.Id);
            this.HasRequired(c => c.PurchaseDoc).WithMany(c => c.PurchaseDocAttachments).HasForeignKey(c => c.PurchaseDocId).WillCascadeOnDelete(true);                                    
        }
    }
}