namespace BSG.ADInventory.Entities
{
    using BSG.ADInventory.Resources;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CalendarYear : BaseEntity
    {
        private ICollection<CalendarMonth> calendarMonths;
        private ICollection<InvYearCycle> invYearCycles;

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "YearNo")]        
        public int YearNo { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Description")]
        public string Description { get; set; }

        public virtual ICollection<CalendarMonth> CalendarMonths
        {
            get { return this.calendarMonths ?? (this.calendarMonths = new HashSet<CalendarMonth>());}
            set { this.calendarMonths = value; }
        }

        public virtual ICollection<InvYearCycle> InvYearCycles
        {
            get { return this.invYearCycles ?? (this.invYearCycles = new HashSet<InvYearCycle>()); }
            set { this.invYearCycles = value; }
        }
    }
}
