using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSG.ADInventory.Resources;
using System.ComponentModel.DataAnnotations;

namespace BSG.ADInventory.Models.Report
{
    public class MatInventoryModel
    {
		[Display(ResourceType = typeof(Resource), Name = "Province")]
		public long ProvinceId { get; set; }
		[Display(ResourceType = typeof(Resource), Name = "Province")]
		public string ProvinceTitle { get; set; }
		[Display(ResourceType = typeof(Resource), Name = "Township")]
		public long TownshipId { get; set; }
		[Display(ResourceType = typeof(Resource), Name = "Township")]
		public string TownshipTitle { get; set; }
		[Display(ResourceType = typeof(Resource), Name = "Branch")]
		public long BranchId { get; set; }
		[Display(ResourceType = typeof(Resource), Name = "Branch")]
		public string BranchTitle { get; set; }
		[Display(ResourceType = typeof(Resource), Name = "Mat")]
		public long MatId { get; set; }
		[Display(ResourceType = typeof(Resource), Name = "Mat")]
		public string MatTitle { get; set; }
		[Display(ResourceType = typeof(Resource), Name = "MatUnit")]
		public string MatUnitTitle { get; set; }
		[Display(ResourceType = typeof(Resource), Name = "MatQtyInv")]
		public double MatQty { get; set; }
	}
}
