namespace BSG.ADInventory.Entities.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;

    public class InvDocDetailConfig : EntityTypeConfiguration<InvDocDetail>
    {
        public InvDocDetailConfig()
        {
            this.HasKey(c => c.Id);            

            this.HasRequired(c => c.InvDoc).WithMany(c => c.InvDocDetails).HasForeignKey(c => c.InvDocId).WillCascadeOnDelete(true);
            this.HasRequired(c => c.Mat).WithMany(c => c.InvDocDetails).HasForeignKey(c => c.MatId).WillCascadeOnDelete(false);
            this.HasOptional(c => c.ProjectDetail).WithMany(c => c.InvDocDetails).HasForeignKey(c => c.ProjectDetailId).WillCascadeOnDelete(false);


        }
    }
}