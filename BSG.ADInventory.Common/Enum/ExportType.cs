namespace BSG.ADInventory.Common.Enum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public enum ExportType
    {
        [Display(ResourceType = typeof(Resources.Resource), Name = "WithOrgCar")]
        WithOrgCar = 1,

        [Display(ResourceType = typeof(Resources.Resource), Name = "WithForeignCar")]
        WithForeignCar = 2,

        [Display(ResourceType = typeof(Resources.Resource), Name = "WithMessenger")]
        WithMessenger = 3,

        [Display(ResourceType = typeof(Resources.Resource), Name = "WithHand")]
        WithHand = 4,

    }
}
