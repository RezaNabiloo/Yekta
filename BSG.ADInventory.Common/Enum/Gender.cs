namespace BSG.ADInventory.Common.Enum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public enum Gender
    {
        [Display(ResourceType = typeof(Resources.Resource), Name = "Male")]
        Male = 0,

        [Display(ResourceType = typeof(Resources.Resource), Name = "Female")]
        Female = 1
    }
}
