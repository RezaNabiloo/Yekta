namespace BSG.ADInventory.Models.Car
{
    using BSG.ADInventory.Models.BaseModel;
    using BSG.ADInventory.Resources;
    using System.ComponentModel.DataAnnotations;

    public class CarListDTO:BaseDTO
    {

        [Display(ResourceType = typeof(Resource), Name = "CarNature")]
        public string CarNatureTitle { get; set; }
        
        [Display(ResourceType = typeof(Resource), Name = "CarType")]
        public string CarTypeTitle { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "PlateNumber")]
        public string PlateNumber { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Driver")]
        public string DriverName { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "CarModel")]
        public string ModelName { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "ProductDate")]        
        public string ProductDate { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Description")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "IsActive")]        
        public bool IsActive { get; set; }

    }
}
