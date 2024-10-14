using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Common.Enum
{
    public enum TechSpecType
    {
        [Display(ResourceType = typeof(Resources.Resource), Name = "String")]
        String = 1,
      //  [Display(ResourceType = typeof(Resources.Resource), Name = "Number")]
       // Number = 2,
        [Display(ResourceType = typeof(Resources.Resource), Name = "Boolean")]
        Boolean = 3,
    }
}
