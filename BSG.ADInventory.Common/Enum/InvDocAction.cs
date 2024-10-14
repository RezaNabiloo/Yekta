namespace BSG.ADInventory.Common.Enum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public enum InvDocAction
    {
        [Display(ResourceType = typeof(Resources.Resource), Name = "Effectless")]
        None = 0,

        [Display(ResourceType = typeof(Resources.Resource), Name = "IncreaseInventory")]
        Add = 1,

        [Display(ResourceType = typeof(Resources.Resource), Name = "InventoryReduction")]
        Sub = -1
    }
}
