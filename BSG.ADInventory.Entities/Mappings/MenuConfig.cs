namespace BSG.ADInventory.Entities.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;

    public class MenuConfig : EntityTypeConfiguration<Menu>
    {
        public MenuConfig()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasOptional(c => c.ParentMenu)
                .WithMany(c => c.ChildMenus)
                .HasForeignKey(c => c.ParentMenuId)
                .WillCascadeOnDelete(false);
        }
    }
}