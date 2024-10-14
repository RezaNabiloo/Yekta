using BSG.ADInventory.Common.Enum;
using BSG.ADInventory.Entities;
using BSG.ADInventory.Models.Mat;
using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.InvDoc
{
    public class InternalEntranceDocDataModel
    {
                private ICollection<InvDocMatDataModel> mats;
        public long Id { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "ReferenceInternalDocNo")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [UIHint("InputGroupSearch")]
        public string ReferenceInternalDocNo { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "FinanceYear")]
        public int FinanceYear { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Project")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public long ProjectId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Car")]        
        public long? CarId { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "DocNo")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]        
        public string DocNo { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Source")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        //[NotEqual("ProjectId")]
        public long? SourceProjectId { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "CarType")]        
        public long? CarTypeId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "DocType")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]        
        public long InvDocTypeId { get; set; }

       

        [Display(ResourceType = typeof(Resource), Name = "Status")]
        public InvDocStatus InvDocStatus { get; set; }

        
        public string QRCode { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "SendByOrgCar")]
        public bool SendByOrgCar { get; set; }

        //[Display(ResourceType = typeof(Resource), Name = "CarInfo")]
        //public long? CarId { get; set; }

        //[Display(ResourceType = typeof(Resource), Name = "Driver")]
        //public long? DriverId { get; set; }
        

        [Display(ResourceType = typeof(Resource), Name = "DriverName")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public string DriverName { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "DriverPhone")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [RegularExpression("^09[0-9]{9}$", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "InvalidMobileFormat")]
        public string DriverPhone { get; set; }

        [StringLength(2, MinimumLength = 2)]
        [RegularExpression("^[1-9][0-9]{1}$", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "InvalidPlateFormat")]
        public string PlateSeries { get; set; }

        public long? PlateCharacterId { get; set; }        

        [StringLength(2, MinimumLength = 2)]
        [RegularExpression("^[1-9][1-9]{1}$", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "InvalidPlateFormat")]
        public string PlateNumberPart1 { get; set; }

        [StringLength(3, MinimumLength = 3)]
        [RegularExpression("^[1-9][1-9]{2}$", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "InvalidPlateFormat")]
        public string PlateNumberPart2 { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "WeighbridgeNo")]
        public string WeighbridgeNo { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Description")]
        public string Description { get; set; }

        public virtual ICollection<InvDocMatDataModel> Mats
        {
            get { return this.mats ?? (this.mats = new HashSet<InvDocMatDataModel>()); }
            set { this.mats = value; }
        }

    }
}