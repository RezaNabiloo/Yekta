namespace BSG.ADInventory.Common.Enum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public enum SchedulerType
    {
        [Display(ResourceType = typeof(Resources.Resource), Name = "Dialy")]
        Dialy = 1,

        [Display(ResourceType = typeof(Resources.Resource), Name = "Weekly")]
        Weekly = 2,

        [Display(ResourceType = typeof(Resources.Resource), Name = "Monthly")]
        Monthly = 3

    }
}
