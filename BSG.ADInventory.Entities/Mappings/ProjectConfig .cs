namespace BSG.ADInventory.Entities.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;

    public class ProjectConfig : EntityTypeConfiguration<Project>
    {
        public ProjectConfig()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasRequired(c => c.Township)
                .WithMany(c => c.Projects)
                .HasForeignKey(c => c.TownshipId)
                .WillCascadeOnDelete(false);

        }
    }
}
