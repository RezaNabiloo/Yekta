namespace BSG.ADInventory.Entities.Mappings
{
    using BSG.ADInventory.Entities;
    using System.Data.Entity.ModelConfiguration;

    public class ProviderConfig : EntityTypeConfiguration<Provider>
    {
        public ProviderConfig()
        {
            this.HasKey(c => c.Id);           

        }
    }
}