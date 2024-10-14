using BSG.ADInventory.Resources;
using System.ComponentModel.DataAnnotations;

namespace BSG.ADInventory.Models.MatUnit
{
    public class MatUnitCEDTO: BaseModel.BaseDTO
    {
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "Abbreviation")]
        public string Abbreviation { get; set; }

    }
}
