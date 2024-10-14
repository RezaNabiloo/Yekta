using BSG.ADInventory.Models.BaseModel;
using BSG.ADInventory.Resources;
using System.ComponentModel.DataAnnotations;

namespace BSG.ADInventory.Models.Country
{
    public class CountryCEDTO: BaseModel.BaseDTO
    {
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string Title { get; set; }
        
        [Display(ResourceType = typeof(Resource), Name = "EnTitle")]
        public string TitleEn { get; set; }

        
    }
}
