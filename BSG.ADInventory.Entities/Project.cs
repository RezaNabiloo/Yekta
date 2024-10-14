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
    public class Project : BaseEntity
    {

        private ICollection<ProjectDetail> projectDetails;
        //private ICollection<Inventory> inventories;
        private ICollection<User> users;
        private ICollection<InvDoc> invDocs;
        private ICollection<InvDoc> sourceProjectInvDocs;
        private ICollection<InvDoc> destProjectInvDocs;
        private ICollection<MatOrder> matOrders;
        private ICollection<PurchaseDoc> purchaseDocs;
        private ICollection<InvYearCycle> invYearCycles;


        public Project()
        {
            this.IsActive = true;            
        }


        [Display(ResourceType = typeof(Resource), Name = "Title")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public string Title { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Township")]
        public long TownshipId { get; set; }
        public virtual Township Township { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "IsActive")]
        public bool IsActive { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Address")]
        public string Address { get; set; }

        
        [Display(ResourceType = typeof(Resource), Name = "Longitude")]
        public float? Lon { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Latitude")]
        public float? Lat { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "CentralInventory")]
        public bool IsCentralInventory { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "BranchInfo")]
        public string BranchInfo
        {
            get
            {
                return string.Format("{0} {1}", this.Title, this.Township.Title);
            }
        }
        public virtual ICollection<ProjectDetail> ProjectDetails
        {
            get { return this.projectDetails ?? (this.projectDetails = new HashSet<ProjectDetail>()); }

            set { this.projectDetails = value; }
        }

        //public virtual ICollection<Inventory> Inventories
        //{
        //    get { return this.inventories ?? (this.inventories = new HashSet<Inventory>()); }

        //    set { this.inventories = value; }
        //}

        public virtual ICollection<User> Users
        {
            get { return this.users ?? (this.users= new HashSet<User>()); }

            set { this.users = value; }
        }
        public virtual ICollection<InvDoc> InvDocs
        {
            get { return this.invDocs ?? (this.invDocs = new HashSet<InvDoc>()); }

            set { this.invDocs = value; }
        }

        public virtual ICollection<InvDoc> SourceProjectInvDocs
        {
            get { return this.sourceProjectInvDocs ?? (this.sourceProjectInvDocs = new HashSet<InvDoc>()); }

            set { this.sourceProjectInvDocs = value; }
        }

        public virtual ICollection<InvDoc> DestProjectInvDocs
        {
            get { return this.destProjectInvDocs ?? (this.destProjectInvDocs = new HashSet<InvDoc>()); }

            set { this.destProjectInvDocs = value; }
        }

        public virtual ICollection<MatOrder> MatOrders
        {
            get { return this.matOrders ?? (this.matOrders = new HashSet<MatOrder>()); }
            set { this.matOrders = value; }
        }
        public virtual ICollection<PurchaseDoc> PurchaseDocs
        {
            get { return this.purchaseDocs ?? (this.purchaseDocs= new HashSet<PurchaseDoc>()); }
            set { this.purchaseDocs = value; }
        }
        public virtual ICollection<InvYearCycle> InvYearCycles
        {
            get { return this.invYearCycles ?? (this.invYearCycles = new HashSet<InvYearCycle>()); }
            set { this.invYearCycles = value; }
        }


    }
}
