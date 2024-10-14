namespace BSG.ADInventory.Entities
{
    using BSG.ADInventory.Resources;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class InvYearCycle : BaseEntity
    {
        [Display(ResourceType = typeof(Resource), Name = "FinanceYear")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public long CalendarYearId { get; set; }
        public virtual CalendarYear CalendarYear { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Project")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public long ProjectId { get; set; }
        public virtual Project Project { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Mat")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public long MatId { get; set; }
        public virtual Mat Mat { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "FirstCountQty")]
        public float FirstCountQty { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "SecondCountQty")]
        public float SecondCountQty { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "ThirdCountQty")]
        public float ThirdCountQty { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "ConfirmedCountQty")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public float ConfirmedCountQty { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Description")]
        public string Description { get; set; }
    }
}
