namespace BSG.ADInventory.Common.Enum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public enum CallContactType
    {
        [Display(ResourceType = typeof(Resources.Resource), Name = "Phone")]
        Phone = 1,
        [Display(ResourceType = typeof(Resources.Resource), Name = "Fax")]
        Fax = 2,
        [Display(ResourceType = typeof(Resources.Resource), Name = "Mobile")]
        Mobile = 3


    }
}

