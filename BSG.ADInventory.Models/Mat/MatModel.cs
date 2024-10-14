using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.Mat
{
    public class MatModel
    {
        public long Id { get; set; }
        public long MatGroupId { get; set; }
        public string MatGroupTitle { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string Title { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Model")]
        public string Model { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Dimention")]
        public string Dimention { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "TechnicalSpecs")]
        public string TechnicalSpecs { get; set; }
    }
}



