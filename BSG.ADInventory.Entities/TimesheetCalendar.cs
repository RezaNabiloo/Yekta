namespace BSG.ADInventory.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Resources;
    using BSG.ADInventory.Common;
    using BSG.ADInventory.Common.Enum;

    public class TimesheetCalendar : BaseEntity
    {
        public string CalendarDate { get; set; }
        public long TimesheetWeekDayId { get; set; }
        public virtual TimesheetWeekDay TimesheetWeekDay { get; set; }
        public TimesheetDayType TimesheetDayType { get; set; }

        public string Description { get; set; }
    }
}
