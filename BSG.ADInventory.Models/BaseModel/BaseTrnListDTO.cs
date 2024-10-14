using BSG.ADInventory.Resources;
using System.ComponentModel.DataAnnotations;

namespace BSG.ADInventory.Models.BaseModel
{
    
    public class BaseTrnListDTO:BaseDTO
    {
        [Display(ResourceType = typeof(Resource), Name = "Language")]        
        public string Language { get; set; }
    }
}
