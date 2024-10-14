using BSG.ADInventory.Common.Enum;
using BSG.ADInventory.Models.BaseModel;
using BSG.ADInventory.Models.MatOrderDetail;
using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BSG.ADInventory.Models.MatOrder
{
    public class MatOrderCEDTO:BaseDTO
    {
        private ICollection<MatOrderDetailCEDTO> matOrderDetails;

        

        [Display(ResourceType = typeof(Resource), Name = "Project")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public long ProjectId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "RequiredDate")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [UIHint("DatePicker")]  
        public DateTime RequiredDate { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "Description")]        
        public string RequestDescription { get; set; }

        public bool Archived { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Priority")]
        public Priority Priority { get; set; }

        public virtual ICollection<MatOrderDetailCEDTO> MatOrderDetails
        {
            get { return this.matOrderDetails ?? (this.matOrderDetails = new HashSet<MatOrderDetailCEDTO>()); }
            set { this.matOrderDetails = value; }
        }

    }
}
