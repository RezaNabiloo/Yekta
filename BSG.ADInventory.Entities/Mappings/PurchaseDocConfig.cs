namespace BSG.ADInventory.Entities.Mappings
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    public class PurchaseDocConfig : EntityTypeConfiguration<PurchaseDoc>
    {
        public PurchaseDocConfig()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);                        
           // this.HasRequired(c => c.Provider).WithMany(c => c.PurchaseDocs).HasForeignKey(c => c.ProviderId).WillCascadeOnDelete(false);
            this.HasRequired(c => c.Project).WithMany(c => c.PurchaseDocs).HasForeignKey(c => c.ProjectId).WillCascadeOnDelete(false);
            this.HasRequired(c => c.AgentUser).WithMany(c => c.AgentPurchaseDocs).HasForeignKey(c => c.AgentUserId).WillCascadeOnDelete(false);

        }
    }
}