namespace BSG.ADInventory.Common.Enum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;    
    using System.Text;

    public enum RequestStatus
    {
        [Display(ResourceType = typeof(Resources.Resource), Name = "Waiting")]
        Waiting = 1,

        [Display(ResourceType = typeof(Resources.Resource), Name = "Pending")]
        Pending = 2,
        [Display(ResourceType = typeof(Resources.Resource), Name = "Done")]
        Done = 3,
        [Display(ResourceType = typeof(Resources.Resource), Name = "Refered")]
        Refered = 4,
        [Display(ResourceType = typeof(Resources.Resource), Name = "Cancellation")]
        Cancel = 5
    }

   
}
