namespace BSG.ADInventory.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Resources;

    public class TimesheetWeekDay : BaseEntity
    {
        private ICollection<TimesheetCalendar> timesheetCalendars;
        

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "DayNumber")]
        public int WeekDay { get; set; }
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the provinces.
        /// </summary>
        /// <value>
        /// The TimesheetCalendar.
        /// </value>
        public virtual ICollection<TimesheetCalendar> TimesheetCalendars
        {
            get
            {
                return this.timesheetCalendars ?? (this.timesheetCalendars = new HashSet<TimesheetCalendar>());
            }
            set
            {
                this.timesheetCalendars = value;
            }
        }

        

    }
}
