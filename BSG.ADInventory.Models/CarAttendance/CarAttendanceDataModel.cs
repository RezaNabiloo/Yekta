using BSG.ADInventory.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.CarAttendance
{
    public class CarAttendanceDataModel
    {
        public CarAttendanceDataModel()
        {
            this.SendSMS = true;
        }
        public long Id { get; set; }
        public long CarId { get; set; }
        public string InOutTime { get; set; }
        public int InOutType { get; set; }
        public bool SendSMS { get; set; }        
    }
}
