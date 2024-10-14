namespace BSG.ADInventory.Entities.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;

    public class MatGroupConfig : EntityTypeConfiguration<MatGroup>
    {
        public MatGroupConfig()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasOptional(c => c.ParentMatGroup)
                .WithMany(c => c.ChildMatGroups)
                .HasForeignKey(c => c.ParentMatGroupId)
                .WillCascadeOnDelete(false);
        }
    }
}