using BSG.ADInventory.Common.Enum;
using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Entities
{
    public class MatPurchase : BaseEntity
    {
        
        //private ICollection<InvDocDetail> invDocDetails;

        public MatPurchase()
        {
            this.IsFreight = true;
        }

        [Display(ResourceType = typeof(Resource), Name = "FinanceYear")]
        public int FinanceYear { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "VDocNo")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public long VDocId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "RDocNo")]
        public long InvDocId { get; set; }
        public virtual InvDoc InvDoc { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Material")]
        public long? MatId { get; set; }
        public virtual Mat Mat{ get; set; }


        public float Qty { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "IsFreight")]

        public bool IsFreight { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "PurchasePrice")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public long PurchasePrice { get; set; }



        [Display(ResourceType = typeof(Resource), Name = "MatGroup")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public long FreightPrice { get; set; }


        //[Display(ResourceType = typeof(Resource), Name = "Mat")]
        //public string MatInfo
        //{
        //    get
        //    {
        //        return string.Format("{0} {1}", this.Title, this.Model);
        //    }
        //}

        //public virtual ICollection<InvDocDetail> InvDocDetails
        //{
        //    get { return this.invDocDetails ?? (this.invDocDetails = new HashSet<InvDocDetail>()); }
        //    set { this.invDocDetails = value; }
        //}


    }
}
