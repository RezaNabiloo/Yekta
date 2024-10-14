namespace BSG.ADInventory.Entities.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;

    public class MenusInRoleConfig : EntityTypeConfiguration<MenusInRole>
    {
        public MenusInRoleConfig()
        {
            //this.HasKey(c => c.MenuId);
            //this.HasKey(c => c.RoleId);


            this.HasRequired(c => c.Role)
                .WithMany(c => c.MenusInRole)
                .HasForeignKey(c => c.RoleId)
                .WillCascadeOnDelete(false);
            this.HasRequired(c => c.Menu)
                .WithMany(c => c.MenusInRole)
                .HasForeignKey(c => c.MenuId)
                .WillCascadeOnDelete(false);
        }
    }
}