namespace BSG.ADInventory.Entities.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;

    public class MatConfig : EntityTypeConfiguration<Mat>
    {
        public MatConfig()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasRequired(c => c.MatGroup).WithMany(c => c.Mats).HasForeignKey(c => c.MatGroupId).WillCascadeOnDelete(false);
            this.HasRequired(c => c.MatUnit).WithMany(c => c.Mats).HasForeignKey(c => c.MatUnitId).WillCascadeOnDelete(false);
            this.HasOptional(c => c.Brand).WithMany(c => c.Mats).HasForeignKey(c => c.BrandId).WillCascadeOnDelete(false);

        }
    }
}
