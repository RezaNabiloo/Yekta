

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.OrgChart
{
    
    public class OrgChartDataModel
    {
        public long id { get; set; }
        public string Title { get; set; }
        public long? parentId { get; set; }
        public bool IsGuardChart { get; set; }


    }
}
