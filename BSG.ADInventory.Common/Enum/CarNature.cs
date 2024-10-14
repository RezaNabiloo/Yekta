using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BSG.ADInventory.Common.Enum
{
    
    public enum CarNature
    {
        [Display(ResourceType = typeof(Resources.Resource), Name = "Organizational")]
        Organizational = 1,

        [Display(ResourceType = typeof(Resources.Resource), Name = "Personal")]
        Personal = 2,

        [Display(ResourceType = typeof(Resources.Resource), Name = "Personnel")]
        Personnel = 3,
    }
}
