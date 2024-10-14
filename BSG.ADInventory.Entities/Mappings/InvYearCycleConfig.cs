namespace BSG.ADInventory.Entities.Mappings
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    public class InvYearCycleConfig : EntityTypeConfiguration<InvYearCycle>
    {
        public InvYearCycleConfig()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            

            this.HasRequired(c=> c.CalendarYear).WithMany(c=> c.InvYearCycles).HasForeignKey(c=> c.CalendarYearId).WillCascadeOnDelete(false);
            this.HasRequired(c => c.Project).WithMany(c => c.InvYearCycles).HasForeignKey(c => c.ProjectId).WillCascadeOnDelete(false);
            this.HasRequired(c => c.Mat).WithMany(c => c.InvYearCycles).HasForeignKey(c => c.MatId).WillCascadeOnDelete(false);
        }
    }
}
