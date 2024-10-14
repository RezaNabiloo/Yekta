using BSG.ADInventory.Common.Enum;
using BSG.ADInventory.Models.Mat;
using BSG.ADInventory.Models.MatOrderDetail;
using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.MatOrder
{
    public class MatOrderConfirmModel
    {
        private List<MatOrderDetailConfirmModel> mats;

        public long Id { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Project")]        
        public string Project { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "RequiredDate")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [UIHint("DatePicker")]  
        public DateTime RequiredDate { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Description")]
        public string RequestUser { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Description")]        
        public string RequestDescription { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "ConfirmDescription")]
        public string ConfirmDescription { get; set; }

        public virtual List<MatOrderDetailConfirmModel> Mats
        {
            get { return this.mats ?? (this.mats = new List<MatOrderDetailConfirmModel>()); }
            set { this.mats = value; }
        }

    }
}
