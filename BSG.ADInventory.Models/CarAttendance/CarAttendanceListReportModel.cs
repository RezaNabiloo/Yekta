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
   public  class CarAttendanceListReportModel
    {
		[Display(ResourceType = typeof(Resource), Name = "Id")]
		public long Id { get; set; }

		[Display(ResourceType = typeof(Resource), Name = "PeopleId")]
		public long CarId { get; set; }

		[Display(ResourceType = typeof(Resource), Name = "OrgInfo")]
		public string OrgInfo { get; set; }

		[Display(ResourceType = typeof(Resource), Name = "Owner")]
		public string Owner { get; set; }

		[Display(ResourceType = typeof(Resource), Name = "InOutType")]
		public int InOutType { get; set; }

		[Display(ResourceType = typeof(Resource), Name = "InOutType")]
		public string InOutTypeTitle { get; set; }

		[Display(ResourceType = typeof(Resource), Name = "InOutTime")]
		public DateTime InOutTime { get; set; }

		[Display(ResourceType = typeof(Resource), Name = "SmsSended")]
		public bool SmsSended { get; set; }

		[Display(ResourceType = typeof(Resource), Name = "Operator")]
		public string Operator { get; set; }		
	}
}
