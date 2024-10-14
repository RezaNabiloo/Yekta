namespace BSG.ADInventory.Common.Enum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public enum ResidentalStatus
    {
        [Display(ResourceType = typeof(Resources.Resource), Name = "Resident")]
        Resident = 0,

        [Display(ResourceType = typeof(Resources.Resource), Name = "NonResident")]
        NonResident = 1
    }
}
