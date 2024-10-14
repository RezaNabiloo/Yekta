
namespace BSG.ADInventory.Common.Enum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public enum WorkshiftDayType
    {

        [Display(ResourceType = typeof(Resources.Resource), Name = "Workday")]
        WorkDay = 1,

        [Display(ResourceType = typeof(Resources.Resource), Name = "Rest")]
        RestDay = 2,
    }
}
