namespace BSG.ADInventory.Entities.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Entities;

    public class InvDocTypeConfig : EntityTypeConfiguration<InvDocType>
    {
        public InvDocTypeConfig()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);            
            this.Property(c => c.DocPrefix).HasMaxLength(1).IsOptional();
        }
    }
}
