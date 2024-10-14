using BSG.ADInventory.Resources;
using System.ComponentModel.DataAnnotations;

namespace BSG.ADInventory.Models.BaseModel
{
    
    public abstract class BaseDTO
    {
        [Display(ResourceType = typeof(Resource), Name = "Id")]
        public long Id { get; set; }
    }
}
