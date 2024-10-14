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
    public class UseDocDataModel
    {
        private ICollection<InvDocMatDataModel> mats;

        public long Id { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "FinanceYear")]
        public int FinanceYear { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Project")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public long ProjectId { get; set; }

        
        [Display(ResourceType = typeof(Resource), Name = "DocNo")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]        
        public string DocNo { get; set; }

        
        [Display(ResourceType = typeof(Resource), Name = "DocType")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]        
        public long InvDocTypeId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Status")]
        public InvDocStatus InvDocStatus { get; set; }

        public string QRCode { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Reciver")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public long PeopleId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Description")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "WeighbridgeNo")]
        public string WeighbridgeNo { get; set; }
        public virtual ICollection<InvDocMatDataModel> Mats
        {
            get { return this.mats ?? (this.mats = new HashSet<InvDocMatDataModel>()); }
            set { this.mats = value; }
        }

    }
}