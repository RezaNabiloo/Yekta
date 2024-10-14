using BSG.ADInventory.Models.BaseModel;
using BSG.ADInventory.Resources;
using System.ComponentModel.DataAnnotations;

namespace BSG.ADInventory.Models.Project
{
    public class ProjectCEDTO:BaseDTO
    {
        [Display(ResourceType = typeof(Resource), Name = "Title")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public string Title { get; set; }

        //[Display(ResourceType = typeof(Resource), Name = "Province")]
        //[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        //public long ProvinceId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Township")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public long TownshipId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Address")]
        public string Address { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Lat")]
        public float? Lat { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Lon")]
        public float? Lon{ get; set; }

        [Display(ResourceType = typeof(Resource), Name = "IsActive")]
        public bool IsActive { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "CentralInventory")]
        public bool IsCentralInventory { get; set; }
    }
}
