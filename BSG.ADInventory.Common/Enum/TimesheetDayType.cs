namespace BSG.ADInventory.Common.Enum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public enum TimesheetDayType
    {
        [Display(ResourceType = typeof(Resources.Resource), Name = "Workday")]
        Workday = 1,
        [Display(ResourceType = typeof(Resources.Resource), Name = "Holiday")]
        Holiday = 2,
        [Display(ResourceType = typeof(Resources.Resource), Name = "Weekend")]
        Weekend = 3,
        
    }
}
            