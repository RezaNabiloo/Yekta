using BSG.ADInventory.Resources;
using System.ComponentModel.DataAnnotations;

namespace BSG.ADInventory.Models.Brand
{
    public class BrandListDTO: BaseModel.BaseDTO
    {

        [Display(ResourceType = typeof(Resource), Name = "FaTitle")]
        public string FaTitle { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "EnTitle")]
        public string EnTitle { get; set; }
                
        
        [Display(ResourceType = typeof(Resource), Name = "Description")]        
        public string Description { get; set; }
    }
}
