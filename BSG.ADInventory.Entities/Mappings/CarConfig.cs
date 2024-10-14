namespace BSG.ADInventory.Entities.Mappings
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    public class CarConfig : EntityTypeConfiguration<Car>
    {
        public CarConfig()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasRequired(c => c.CarType).WithMany(c => c.Cars).HasForeignKey(c => c.CarTypeId).WillCascadeOnDelete(false);
            this.HasOptional(c => c.OwnerDriver).WithMany(c => c.OwnerDriverCars).HasForeignKey(c => c.OwnerDriverId).WillCascadeOnDelete(false);
            this.HasRequired(c => c.PlateCharacter).WithMany(c => c.OrgCars).HasForeignKey(c => c.PlateCharacterId).WillCascadeOnDelete(false);
            
        }
    }
}
