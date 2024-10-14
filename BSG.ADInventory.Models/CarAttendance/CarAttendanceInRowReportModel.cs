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
    public class CarAttendanceInRowReportModel
    {
        
        [Display(ResourceType = typeof(Resource), Name = "PeopleId")]
        public long CarId { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "Date")]
        public string InOutDate { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "IO1")]
        public string IO1 { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "IO2")]
        public string IO2 { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "IO3")]
        public string IO3 { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "IO4")]
        public string IO4{ get; set; }
        [Display(ResourceType = typeof(Resource), Name = "IO5")]
        public string IO5 { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "IO6")]
        public string IO6{ get; set; }
        [Display(ResourceType = typeof(Resource), Name = "IO7")]
        public string IO7 { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "IO8")]
        public string IO8{ get; set; }
        [Display(ResourceType = typeof(Resource), Name = "IO9")]
        public string IO9 { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "IO10")]
        public string IO10{ get; set; }

        [Display(ResourceType = typeof(Resource), Name = "OwnerInfo")]
        public string OwnerInfo { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "CarInfo")]
        public string CarInfo { get; set; }
        
    }
}
