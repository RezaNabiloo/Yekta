using BSG.ADInventory.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.CarAttendance
{
    public class CarAttendanceModel
    {
        public long Id { get; set; }
        public long CarId { get; set; }
        public string CarInfo { get; set; }
        public InOutType InOutType { get; set; }
        public DateTime InOutTime { get; set; }
        public bool SmsSended { get; set; }
        public InOutType NextInOutType { get; set; }
    }
}