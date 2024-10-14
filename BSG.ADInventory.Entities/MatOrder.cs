namespace BSG.ADInventory.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Common.Enum;
    using BSG.ADInventory.Resources;

    public class MatOrder : BaseEntity
    {
        private ICollection<MatOrderDetail> matOrderDetails;

        public MatOrder()
        {
            ConfirmStatus = ConfirmStatus.Pending;
        }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "ReuiredDate")]
        public DateTime RequiredDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "Project")]
        public long ProjectId { get; set; }
        public virtual Project Project { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Description")]
        public string RequestDescription { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "ConfirmDescription")]
        public string ConfirmDescription { get; set; }


        public Guid? ConfirmUserId { get; set; }
        public virtual User ConfirmUser { get; set; }

        public DateTime? ConfirmTime { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Status")]
        public ConfirmStatus ConfirmStatus { get; set; }

        public bool Archived { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Priority")]
        public Priority Priority { get; set; }

        public virtual ICollection<MatOrderDetail> MatOrderDetails
        {
            get{return this.matOrderDetails ?? (this.matOrderDetails = new HashSet<MatOrderDetail>());}
            set{this.matOrderDetails= value;}
        }


    }
}
