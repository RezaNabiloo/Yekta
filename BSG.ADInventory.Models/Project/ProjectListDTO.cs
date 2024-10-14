using BSG.ADInventory.Models.BaseModel;
using BSG.ADInventory.Resources;
using System.ComponentModel.DataAnnotations;

namespace BSG.ADInventory.Models.Project
{
    public class ProjectListDTO:BaseDTO
    {        
        
        public string Title { get; set; }                
        public string TownshipTitle { get; set; }
        public string ProvinceTitle { get; set; }
        
        [Display(ResourceType = typeof(Resource), Name = "Address")]
        public string Address { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Lat")]
        public float? Lat { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Lon")]
        public float? Lon { get; set; }
                
        [Display(ResourceType = typeof(Resource), Name = "Active")]
        public string IsActive { get; set; }
    }
}
