namespace BSG.ADInventory.Entities.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;

    public class MatOrderDetailConfig : EntityTypeConfiguration<MatOrderDetail>
    {
        public MatOrderDetailConfig()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            this.HasRequired(c => c.MatOrder).WithMany(c => c.MatOrderDetails).HasForeignKey(c => c.MatOrderId).WillCascadeOnDelete(true);
            this.HasRequired(c => c.Mat).WithMany(c => c.MatOrderDetails).HasForeignKey(c => c.MatId).WillCascadeOnDelete(false);
        }
    }
}