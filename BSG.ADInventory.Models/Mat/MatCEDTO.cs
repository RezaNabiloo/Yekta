using BSG.ADInventory.Common.Enum;
using BSG.ADInventory.Resources;
using System.ComponentModel.DataAnnotations;

namespace BSG.ADInventory.Models.Mat
{
    public class MatCEDTO: BaseModel.BaseDTO
    {
        [Display(ResourceType = typeof(Resource), Name = "Code")]
        public string Code { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MatGroup")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public long MatGroupId { get; set; }        

        [Display(ResourceType = typeof(Resource), Name = "Brand")]
        public long? BrandId { get; set; }        

        [Display(ResourceType = typeof(Resource), Name = "Title")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public string Title { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MatUnit")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public long MatUnitId { get; set; }        

        [Display(ResourceType = typeof(Resource), Name = "Model")]
        public string Model { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Dimention")]
        public string Dimention { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "TechnicalSpec")]
        public string TechnicalSpec { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MinimumInventory")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public double MinimumInventory { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "OrderPoint")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public double OrderPoint { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MatAllocationType")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public MatAllocationType MatAllocationType { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MatStorageMethod")]
        public MatStorageMethod? MatStorageMethod { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Status")]
        public bool IsActive { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Description")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "HasExpirationDate")]
        public bool HasExpirationDate { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "ImageUrl")]
        [UIHint("Upload")]
        public string ImageUrl { get; set; }
    }
}
