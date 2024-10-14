namespace BSG.ADInventory.Common.Enum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public enum ScoreType
    {
        [Display(ResourceType = typeof(Resources.Resource), Name = "Positive")]
        Positive = 1,

        [Display(ResourceType = typeof(Resources.Resource), Name = "Negative")]
        Negative = -1
    }
}
