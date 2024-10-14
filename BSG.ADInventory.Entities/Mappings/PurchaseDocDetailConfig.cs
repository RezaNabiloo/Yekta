namespace BSG.ADInventory.Entities.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;

    public class PurchaseDocDetailConfig : EntityTypeConfiguration<PurchaseDocDetail>
    {
        public PurchaseDocDetailConfig()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasRequired(c => c.PurchaseDoc).WithMany(c => c.PurchaseDocDetails).HasForeignKey(c => c.PurchaseDocId).WillCascadeOnDelete(true);
            this.HasOptional(c => c.MatOrderDetail).WithMany(c => c.PurchaseDocDetails).HasForeignKey(c => c.MatOrderDetailId).WillCascadeOnDelete(false);
            this.HasRequired(c => c.Mat).WithMany(c => c.PurchaseDocDetails).HasForeignKey(c => c.MatId).WillCascadeOnDelete(false);

        }
    }
}