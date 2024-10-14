namespace BSG.ADInventory.Entities.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;

    public class TimesheetCalendarConfig : EntityTypeConfiguration<TimesheetCalendar>
    {
        public TimesheetCalendarConfig()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.CalendarDate).HasMaxLength(10).IsRequired();
            this.HasRequired(c => c.TimesheetWeekDay)
                .WithMany(c => c.TimesheetCalendars)
                .HasForeignKey(c => c.TimesheetWeekDayId)
                .WillCascadeOnDelete(false);
        }
    }
}