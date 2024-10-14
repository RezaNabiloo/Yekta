using BSG.ADInventory.Common.Enum;
using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.Mat
{
    public class MatStockViewModel
    {
        [Display(ResourceType = typeof(Resource), Name = "Id")]
        public long Id { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MatCode")]
        public string MatCode { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MatGroup")]
        public string MatGroup { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string Title { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Project")]
        public string Project { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Qty")]
        public double Qty { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MatUnit")]
        public string MatUnit { get; set; }        
    }
}



