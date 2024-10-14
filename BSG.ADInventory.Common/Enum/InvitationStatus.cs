namespace BSG.ADInventory.Common.Enum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public enum InvitationStatus
    {
        [Display(ResourceType = typeof(Resources.Resource), Name = "Requested")]
        Requested = 0,
        [Display(ResourceType = typeof(Resources.Resource), Name = "Registered")]
        Registered = 1,
        [Display(ResourceType = typeof(Resources.Resource), Name = "ConfirmedByHost")]
        ConfirmedByHost = 2,
        [Display(ResourceType = typeof(Resources.Resource), Name = "Finished")]
        Finished = 3,
        [Display(ResourceType = typeof(Resources.Resource), Name = "Exited")]
        Exited = 4,
        [Display(ResourceType = typeof(Resources.Resource), Name = "Canceled")]
        Canceled = 5
    }
}
