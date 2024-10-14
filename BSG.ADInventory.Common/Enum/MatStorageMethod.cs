namespace BSG.ADInventory.Common.Enum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public enum MatStorageMethod
    {
        [Display(ResourceType = typeof(Resources.Resource), Name = "FIFO")]
        FIFO = 1,

        [Display(ResourceType = typeof(Resources.Resource), Name = "LIFO")]
        LIFO = 2
    }
}
