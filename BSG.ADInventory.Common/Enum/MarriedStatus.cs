namespace BSG.ADInventory.Common.Enum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public enum MarriedStatus
    {
        [Display(ResourceType = typeof(Resources.Resource), Name = "Single")]
        Sigle = 0,

        [Display(ResourceType = typeof(Resources.Resource), Name = "Married")]
        Married = 1
    }
}
