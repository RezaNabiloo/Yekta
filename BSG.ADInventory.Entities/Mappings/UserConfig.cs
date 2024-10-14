using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Entities.Mappings
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
           
            this.HasOptional(s => s.People)
                .WithMany(s => s.Users)
                .HasForeignKey(s => s.PeopleId)
                .WillCascadeOnDelete(false);
            
        }
        
    }
}
