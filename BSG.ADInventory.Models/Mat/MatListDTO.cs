using BSG.ADInventory.Models.BaseModel;
using BSG.ADInventory.Resources;
using System.ComponentModel.DataAnnotations;

namespace BSG.ADInventory.Models.Mat
{
    public class MatListDTO:BaseDTO
    {
        [Display(ResourceType = typeof(Resource), Name = "Code")]
        public string Code { get; set; }
        public long MatGroupId { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "MatGroup")]
        public string MatGroupTitle { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Brand")]
        public string BrandTitle { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string Title { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Model")]
        public string Model { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MatUnit")]
        public string MatUnitTitle { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Dimention")]
        public string Dimention { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "TechnicalSpec")]
        public string TechnicalSpec { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MinimumInventory")]
        public double MinimumInventory { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "OrderPoint")]
        public double OrderPoint { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MatAllocationType")]
        public string MatAllocationType { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MatStorageMethod")]
        public string MatStorageMethod { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "IsActive")]
        public string IsActive { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "HasExpirationDate")]
        public bool HasExpirationDate { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Description")]
        public string Description { get; set; }
    }
}



