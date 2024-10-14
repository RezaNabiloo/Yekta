using BSG.ADInventory.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.CarAttendance
{
    public class CarAttendanceResponseModel
    {
        public CarAttendanceResponseModel()
        {
            
        }
        public long Id { get; set; }
        public long CarId { get; set; }
        public string LastInOutTime { get; set; }
        public InOutType NextInOutType { get; set; }
        public string Message { get; set; }        
    }
}
