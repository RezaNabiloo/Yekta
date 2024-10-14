using BSG.ADInventory.Common.Enum;
using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BSG.ADInventory.Entities
{
    public class Car : BaseEntity
    {
        private ICollection<InvDoc> invDocs;
        public Car()
        {
            this.IsActive = true;            
        }

        [Display(ResourceType = typeof(Resource), Name = "IsActive")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public bool IsActive { get; set; }
        
        [Display(ResourceType = typeof(Resource), Name = "CarType")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public long CarTypeId { get; set; }
        public virtual CarType CarType { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "CarNature")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public CarNature CarNature { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "CarModel")]
        public string ModelName { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "ProductDate")]
        
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode =true)]
        public DateTime? ProductDate { get; set; }
        
        [Display(ResourceType = typeof(Resource), Name = "Description")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "DriverOrTransferee")]        
        public long? OwnerDriverId { get; set; }
        public virtual People OwnerDriver { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [StringLength(2, MinimumLength =2)]
        public string PlateSeries { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public long PlateCharacterId { get; set; }
        public virtual PlateCharacter PlateCharacter { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [StringLength(2, MinimumLength = 2)]
        public string PlateNumberPart1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [StringLength(3, MinimumLength = 3)]
        public string PlateNumberPart2 { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "ImageUrl")]
        [UIHint("Upload")]
        public string ImageUrl { get; set; }

        public string CarInfo
        {
            get
            {
                return string.Format("{0} * {1}",this.CarType.Title, "ایران" +this.PlateSeries+" - "+this.PlateNumberPart2+" "+this.PlateCharacter.Title+" "+this.PlateNumberPart1);
            }
        }

        [Display(ResourceType = typeof(Resource), Name = "PlateNumber")]
        public string PlateNumber
        {
            get
            {
                return "ایران" + this.PlateSeries + " - " + this.PlateNumberPart2 + " " + this.PlateCharacter.Title + " " + this.PlateNumberPart1;
            }
        }

        public virtual ICollection<InvDoc> InvDocs
        {
            get { return this.invDocs ?? (this.invDocs = new HashSet<InvDoc>()); }
            set { this.invDocs = value; }
        }

    }
}
