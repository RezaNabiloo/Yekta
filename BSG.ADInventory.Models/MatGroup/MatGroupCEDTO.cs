using BSG.ADInventory.Resources;
using System.ComponentModel.DataAnnotations;

namespace BSG.ADInventory.Models.MatGroup
{
    public class MatGroupCEDTO: BaseModel.BaseDTO
    {   
        [Display(ResourceType = typeof(Resource), Name = "ParentGroup")]
        public long? ParentMatGroupId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Title")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public string Title { get; set; }

        

    }
}
