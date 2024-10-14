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
    public class Mat : BaseEntity
    {
        
        private ICollection<InvDocDetail> invDocDetails;
        private ICollection<MatPurchase> matPurchases;
        private ICollection<MatOrderDetail> matOrderDetails;
        private ICollection<PurchaseDocDetail> purchaseDocDetails;
        private ICollection<InvYearCycle> invYearCycles;

        
        public Mat()
        {
            this.MinimumInventory = 0;
            this.OrderPoint = 0;
            this.MatAllocationType = MatAllocationType.Public;
            this.IsActive = true;
            this.HasExpirationDate = false;
            this.OrderPoint = 0;
            this.MinimumInventory = 0;
        }

        [Display(ResourceType = typeof(Resource), Name = "Code")]
        public string Code { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MatGroup")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public long MatGroupId { get; set; }
        public virtual MatGroup MatGroup { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Brand")]        
        public long? BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Title")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public string Title { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MatUnit")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public long MatUnitId { get; set; }
        public virtual MatUnit MatUnit { get; set; }

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
        public string Description{ get; set; }

        [Display(ResourceType = typeof(Resource), Name = "HasExpirationDate")]
        public bool HasExpirationDate { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "ImageUrl")]
        [UIHint("Upload")]
        public string ImageUrl { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Mat")]
        public string MatInfo
        {
            get
            {
                return string.Format("{0} {1}", this.Title, this.Model);
            }
        }
     
        public virtual ICollection<InvDocDetail> InvDocDetails
        {
            get { return this.invDocDetails ?? (this.invDocDetails = new HashSet<InvDocDetail>()); }
            set { this.invDocDetails = value; }
        }

        public virtual ICollection<MatPurchase> MatPurchases
        {
            get { return this.matPurchases ?? (this.matPurchases = new HashSet<MatPurchase>()); }
            set { this.matPurchases = value; }
        }

        public virtual ICollection<MatOrderDetail> MatOrderDetails
        {
            get { return this.matOrderDetails ?? (this.matOrderDetails = new HashSet<MatOrderDetail>()); }
            set { this.matOrderDetails = value; }
        }

        public virtual ICollection<PurchaseDocDetail> PurchaseDocDetails
        {
            get { return this.purchaseDocDetails ?? (this.purchaseDocDetails = new HashSet<PurchaseDocDetail>()); }
            set { this.purchaseDocDetails = value; }
        }

        public virtual ICollection<InvYearCycle> InvYearCycles
        {
            get { return this.invYearCycles ?? (this.invYearCycles = new HashSet<InvYearCycle>()); }
            set { this.invYearCycles = value; }
        }
    }
}
