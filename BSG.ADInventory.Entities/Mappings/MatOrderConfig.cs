namespace BSG.ADInventory.Entities.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;

    public class MatOrderConfig : EntityTypeConfiguration<MatOrder>
    {
        public MatOrderConfig()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);                        

            this.HasRequired(c => c.Project).WithMany(c => c.MatOrders).HasForeignKey(c => c.ProjectId).WillCascadeOnDelete(false);
            this.HasOptional(c => c.ConfirmUser).WithMany(c => c.MatOrders).HasForeignKey(c => c.ConfirmUserId).WillCascadeOnDelete(false);
        }
    }
}