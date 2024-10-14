namespace BSG.ADInventory.Entities.Mappings
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    public class CalendarMonthConfig : EntityTypeConfiguration<CalendarMonth>
    {
        public CalendarMonthConfig()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasRequired(c => c.CalendarYear)
                .WithMany(c => c.CalendarMonths)
                .HasForeignKey(c => c.CalendarYearId)
                .WillCascadeOnDelete(false);

        }
    }
}
