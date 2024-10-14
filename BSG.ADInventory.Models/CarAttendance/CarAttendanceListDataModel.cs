using BSG.ADInventory.Common.Enum;
using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.CarAttendance
{
    public class CarAttendanceListDataModel
    {

        [Display(ResourceType = typeof(Resource), Name = "Id")]
        public long Id { get; set; }

        public long CarId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "CarInfo")]
        public string CarInfo { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "InOutType")]
        public string InOutType { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "InOutTime")]
        public DateTime InOutTime { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Operator")]
        public string Operator { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Gate")]
        public string GateDeviceTitle { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "IpAddress")]
        public string GateDeviceIp { get; set; }





    }
}
