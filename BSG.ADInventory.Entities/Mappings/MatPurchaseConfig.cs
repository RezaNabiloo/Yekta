namespace BSG.ADInventory.Entities.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;

    public class MatPurchaseConfig : EntityTypeConfiguration<MatPurchase>
    {
        public MatPurchaseConfig()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasRequired(c => c.InvDoc).WithMany(c => c.MatPurchases).HasForeignKey(c => c.InvDocId).WillCascadeOnDelete(false);
            this.HasOptional(c => c.Mat).WithMany(c => c.MatPurchases).HasForeignKey(c => c.MatId).WillCascadeOnDelete(false);
            

        }
    }
}
