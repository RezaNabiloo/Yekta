namespace BSG.ADInventory.Common.Enum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public enum InOutType
    {
        [Display(ResourceType = typeof(Resources.Resource), Name = "Out")]
        Out = 0,

        [Display(ResourceType = typeof(Resources.Resource), Name = "In")]
        In = 1
    }
}

