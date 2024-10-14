using BSG.ADInventory.Common.Enum;
using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.Report
{
    public class PlannedPMAgendaModel
    {
        public long Id { get; set; }
        public long BranchId { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Branch")]
        public string BranchInfo { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "Device")]
        public long DeviceId { get; set; }
        
        public string DeviceInfo { get; set; }
        public long ContractorId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Contractor")]
        public string ContractorInfo { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "TimeInterval")]
        public TimeInterval TimeInterval { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Value")]
        public int TimeIntervalValue { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "StartTime")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "DueDate")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "AgendaDate")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? AgendaDate { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "PMPeriod")]
        public int? PMPeriod { get; set; }

        public bool IsDone { get; set; }

    }
}
