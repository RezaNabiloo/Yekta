namespace BSG.ADInventory.Common.Enum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public enum ConfirmStatus
    {
        [Display(ResourceType = typeof(Resources.Resource), Name = "Pending")]
        Pending = 0,

        [Display(ResourceType = typeof(Resources.Resource), Name = "Confirm")]
        Confirmed = 1,
        [Display(ResourceType = typeof(Resources.Resource), Name = "Reject")]
        Rejected = 2

    }
}
