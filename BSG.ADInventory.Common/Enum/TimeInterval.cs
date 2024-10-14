namespace BSG.ADInventory.Common.Enum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public enum TimeInterval
    {
        [Display(ResourceType = typeof(Resources.Resource), Name = "Daily")]
        Day = 1,
        [Display(ResourceType = typeof(Resources.Resource), Name = "Weekly")]
        Week = 2,
        [Display(ResourceType = typeof(Resources.Resource), Name = "Monthly")]
        Month = 3,
        [Display(ResourceType = typeof(Resources.Resource), Name = "Yearly")]
        Year = 4
    }
}