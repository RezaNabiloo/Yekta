using BSG.ADInventory.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.CarAttendance
{
    public class CarGateIODataModel
    {
        public long CarId { get; set; }
        public InOutType InOutType { get; set; }

        public string IPAddress { get; set; }
    }
}
