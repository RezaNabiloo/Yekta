namespace BSG.ADInventory.Common.Enum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public enum ExitType
    {
        [Display(ResourceType = typeof(Resources.Resource), Name = "Certain")]
        Certain = 0,

        [Display(ResourceType = typeof(Resources.Resource), Name = "Temporary")]
        Temporary = 1
    }
}
