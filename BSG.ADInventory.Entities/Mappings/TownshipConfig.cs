namespace BSG.ADInventory.Entities.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;

    public class TownshipConfig : EntityTypeConfiguration<Township>
    {
        public TownshipConfig()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Title).HasMaxLength(255).IsRequired();
            this.Property(c => c.ProvinceId).IsOptional();

            this.HasRequired(c => c.Province)
                .WithMany(c => c.Townships)
                .HasForeignKey(c => c.ProvinceId)
                .WillCascadeOnDelete(false);
        }
    }
}