namespace BSG.ADInventory.Models.Car
{
    using BSG.ADInventory.Common.Enum;
    using BSG.ADInventory.Models.BaseModel;
    using BSG.ADInventory.Resources;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CarCEDTO:BaseDTO
    {        

        [Display(ResourceType = typeof(Resource), Name = "CarNature")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public CarNature CarNature { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Status")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public bool IsActive { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "CarType")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public long CarTypeId { get; set; }



        [Display(ResourceType = typeof(Resource), Name = "CarModel")]
        public string ModelName { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "ProductDate")]

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [UIHint("DatePicker")]
        public DateTime? ProductDate { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Description")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "DriverOrTransferee")]
        public long? OwnerDriverId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [StringLength(2, MinimumLength = 2)]
        public string PlateSeries { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public long PlateCharacterId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [StringLength(2, MinimumLength = 2)]
        public string PlateNumberPart1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [StringLength(3, MinimumLength = 3)]
        public string PlateNumberPart2 { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "ImageUrl")]
        [UIHint("Upload")]
        public string ImageUrl { get; set; }

    }
}
