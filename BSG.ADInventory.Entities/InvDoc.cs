namespace BSG.ADInventory.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Common.Enum;
    using BSG.ADInventory.Resources;

    public class InvDoc : BaseEntity
    {
        private ICollection<InvDocDetail> invDocDetails;
        private ICollection<InvDocAttachment> invDocAttachments;
        private ICollection<MatPurchase> matPurchases;

        public InvDoc()
        {
            this.InvDocStatus = InvDocStatus.Temporary;
        }
        

        [Display(ResourceType = typeof(Resource), Name = "FinanceYear")]
        public int FinanceYear { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Project")]
        public long ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public long? CarId { get; set; }
        public virtual Car Car{ get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Provider")]
        public long? ProviderId { get; set; }
        //public virtual Provider Provider { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "DocNo")]
        public string DocNo { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "DocType")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public long InvDocTypeId { get; set; }
        public virtual InvDocType InvDocType { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Status")]        
        public InvDocStatus InvDocStatus { get; set; }

        public long? SourceProjectId { get; set; }
        public virtual Project SourceProject { get; set; }

        public long? DestProjectId { get; set; }
        public virtual Project DestProject { get; set; }

        public string ReferenceInternalDocNo { get; set; }


        public string QRCode { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "SendByOrgCar")]
        public bool SendByOrgCar { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "DriverName")]
        public string DriverName { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "DriverPhone")]
        public string DriverPhone { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "CarType")]        
        public long? CarTypeId { get; set; }
        public virtual CarType CarType { get; set; }

        [StringLength(2, MinimumLength = 2)]
        public string PlateSeries { get; set; }
        
        public long? PlateCharacterId { get; set; }
        public virtual PlateCharacter PlateCharacter { get; set; }
        
        [StringLength(2, MinimumLength = 2)]
        public string PlateNumberPart1 { get; set; }
        
        [StringLength(3, MinimumLength = 3)]
        public string PlateNumberPart2 { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "WeighbridgeNo")]
        public string WeighbridgeNo { get; set; }

        public long? PeopleId { get; set; }
        public virtual People People { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Description")]
        public string Description { get; set; }


        public Guid? ConfirmUserId { get; set; }
        public virtual User ConfirmUser { get; set; }


        public long?  PurchaseDocId { get; set; }
        public virtual PurchaseDoc PurchaseDoc{ get; set; }

        public bool IsAggregated { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "PlateNumber")]
        public string PlateNumber
        {
            get
            {
                return "ایران" + this.PlateSeries + " - " + this.PlateNumberPart2 + " " + this.PlateCharacter.Title + " " + this.PlateNumberPart1;
            }
        }

        public virtual ICollection<InvDocDetail> InvDocDetails
        {
            get { return this.invDocDetails ?? (this.invDocDetails = new HashSet<InvDocDetail>()); }
            set { this.invDocDetails = value; }
        }

        public virtual ICollection<InvDocAttachment> InvDocAttachments
        {
            get { return this.invDocAttachments ?? (this.invDocAttachments = new HashSet<InvDocAttachment>()); }
            set { this.invDocAttachments = value; }
        }

        public virtual ICollection<MatPurchase> MatPurchases
        {
            get { return this.matPurchases ?? (this.matPurchases = new HashSet<MatPurchase>()); }
            set { this.matPurchases = value; }
        }

    }
}
