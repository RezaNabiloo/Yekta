namespace BSG.ADInventory.Entities.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;

    public class ProvinceConfig : EntityTypeConfiguration<Province>
    {
        public ProvinceConfig()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Title).HasMaxLength(255).IsRequired();            
            this.Property(c => c.CountryId).IsRequired();

            this.HasRequired(c=> c.Country)
                .WithMany(c=> c.Provinces)
                .HasForeignKey(c=> c.CountryId)
                .WillCascadeOnDelete(false);
        }
    }
}
