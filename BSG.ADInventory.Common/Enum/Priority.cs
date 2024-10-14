namespace BSG.ADInventory.Common.Enum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public enum Priority
    {
        [Display(ResourceType = typeof(Resources.Resource), Name = "Normal")]
        Normal = 1,

        [Display(ResourceType = typeof(Resources.Resource), Name = "Urgent")]
        Urgent = 2,

        [Display(ResourceType = typeof(Resources.Resource), Name = "VeryUrgent")]
        VeryUrgent = 3
    }
}
