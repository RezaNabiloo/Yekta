using BSG.ADInventory.Resources;
using System.ComponentModel.DataAnnotations;

namespace BSG.ADInventory.Models.Township
{
    public class TownshipCEDTO: BaseModel.BaseDTO
    {
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "Province")]
        public long ProvinceId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string Title { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "IsCapital")]
        public bool IsCapital { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "IsActive")]
        public bool IsActive { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Lat")]
        public double? Lat { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Lon")]
        public double? Lon { get; set; }

    }
}
