using BSG.ADInventory.Models.BaseModel;
using BSG.ADInventory.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace BSG.ADInventory.Models.Province
{
    public class ProvinceCEDTO: BaseModel.BaseDTO
    {
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "Country")]
        public long CountryId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string Title { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "AreaCode")]
        public string AreaCode { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "IsActive")]
        public bool IsActive { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Lat")]
        public double? Lat { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Lon")]
        public double? Lon { get; set; }

    }
}
