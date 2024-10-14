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

    public class ProjectDetail : BaseEntity       
    {
        public ProjectDetail()
        {
            this.IsActive = true;
        }
        private ICollection<InvDocDetail> invDocDetails;
        public long ProjectId { get; set; }
        public virtual Project Project { get; set; }

        
        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string Title { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "Description")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Status")]
        public bool IsActive { get; set; }


        public ICollection<InvDocDetail> InvDocDetails 
        {
            get
            {
                return this.invDocDetails?? (this.invDocDetails= new HashSet<InvDocDetail>());
            }
            set
            {
                this.invDocDetails = value;
            }
        }

    }
}
