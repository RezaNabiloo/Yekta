using BSG.ADInventory.Common.Enum;
using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.CarAttendance
{
    public class CarAttendanceListViewModel
    {
        public long Id { get; set; }
        public long CarId { get; set; }
        public string CarInfo { get; set; }        
        public string PlateSeries { get; set; }        
        public string PlateCharacter { get; set; }
        public string PlateNumberPart1 { get; set; }        
        public string PlateNumberPart2 { get; set; }
        public string ImageUrl { get; set; }
        public InOutType NextInOutType { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public string OwnerInfo { get; set; }
        public string OwnerImageUrl { get; set; }
    }
}