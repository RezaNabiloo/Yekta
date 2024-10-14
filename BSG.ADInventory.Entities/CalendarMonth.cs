using BSG.ADInventory.Resources;
using System.ComponentModel.DataAnnotations;

namespace BSG.ADInventory.Entities
{
    public class CalendarMonth:BaseEntity
    {
        public long CalendarYearId { get; set; }
        public virtual CalendarYear CalendarYear { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "MonthNo")]
        public int MonthNo { get; set; }

        public string Title { get; set; }
    }
}
