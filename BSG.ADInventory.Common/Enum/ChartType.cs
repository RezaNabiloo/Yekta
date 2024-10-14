namespace BSG.ADInventory.Common.Enum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public enum ChartType
    {

        [Display(ResourceType = typeof(Resources.Resource), Name = "LineChart")]
        LineChart = 1,
        [Display(ResourceType = typeof(Resources.Resource), Name = "BarChart")]
        BarChart = 2,
        [Display(ResourceType = typeof(Resources.Resource), Name = "PieChart")]
        PieChart = 3,

    }
}