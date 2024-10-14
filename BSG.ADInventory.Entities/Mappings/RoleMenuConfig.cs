namespace BSG.ADInventory.Entities.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;

    public class RoleMenuConfig : EntityTypeConfiguration<RoleMenu>
    {
        public RoleMenuConfig()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            this.HasRequired(c => c.Role).WithMany(c => c.RoleMenus).HasForeignKey(c => c.RoleId).WillCascadeOnDelete(false);
            this.HasRequired(c => c.Menu).WithMany(c => c.RoleMenus).HasForeignKey(c => c.MenuId).WillCascadeOnDelete(false);
        }
    }
}
