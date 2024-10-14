namespace BSG.ADInventory.Common.Enum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public enum TimesheetCalcErrorCode
    {
        [Display(ResourceType = typeof(Resources.Resource), Name = "NoError")]
        NoError = 0,

        [Display(ResourceType = typeof(Resources.Resource), Name = "Absent")]
        Absent = 1,

        [Display(ResourceType = typeof(Resources.Resource), Name = "Holiday")]
        Holiday = 2,

        [Display(ResourceType = typeof(Resources.Resource), Name = "Weekend")]
        Weekend = 3,

        [Display(ResourceType = typeof(Resources.Resource), Name = "CardError")]
        CardError = 4
    }
}
