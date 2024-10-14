





using BSG.ADInventory.Common.Enum;
using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.MatGroupTechSpec
{
    public class MatGroupTechSpecModel
    {
        public long Id { get; set; }
        public long MatGroupId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Title")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public string FaTitle { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "DataType")]
        public TechSpecType TechSpecType { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "ViewOrder")]
        public int ViewOrder { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "ShowValueLTR")]
        public bool ShowValueLTR { get; set; }
    }
}

