namespace BSG.ADInventory.Entities.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;

    public class UserMenuConfig : EntityTypeConfiguration<UserMenu>
    {
        public UserMenuConfig()
        {
            this.HasKey(c => c.Id);
            
            this.HasRequired(c => c.Menu).WithMany(c => c.UserMenus).HasForeignKey(c => c.MenuId).WillCascadeOnDelete(false);
            this.HasRequired(c => c.User).WithMany(c => c.UserMenus).HasForeignKey(c => c.UserId).WillCascadeOnDelete(false);

        }
    }
}
