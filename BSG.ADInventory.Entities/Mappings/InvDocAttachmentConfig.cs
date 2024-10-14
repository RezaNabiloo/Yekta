namespace BSG.ADInventory.Entities.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;

    public class InvDocAttachmentConfig : EntityTypeConfiguration<InvDocAttachment>
    {
        public InvDocAttachmentConfig()
        {
            this.HasKey(c => c.Id);
            this.HasRequired(c => c.InvDoc).WithMany(c => c.InvDocAttachments).HasForeignKey(c => c.InvDocId).WillCascadeOnDelete(true);                                    
        }
    }
}