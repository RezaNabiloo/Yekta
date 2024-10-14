using BSG.ADInventory.Resources;
using System.ComponentModel.DataAnnotations;

namespace BSG.ADInventory.Models.BaseModel
{
    
    public class BaseTrnCEDTO:BaseDTO
    {
        
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public long ParentId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Language")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public long LanguageId { get; set; }
    }
}
