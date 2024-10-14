namespace BSG.ADInventory.Common.Enum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public enum AttendanceRegType
    {
        [Display(ResourceType = typeof(Resources.Resource), Name = "RegByCard")]
        RegByCard = 1,

        [Display(ResourceType = typeof(Resources.Resource), Name = "RegByOperator")]
        RegByOperator = 2,

        [Display(ResourceType = typeof(Resources.Resource), Name = "RegByCar")]
        RegByCar = 3
    }
}
