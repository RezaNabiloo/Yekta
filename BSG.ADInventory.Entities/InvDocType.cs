using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Entities
{
    public class InvDocType
    {
        private ICollection<InvDoc> invDocs;
        public InvDocType()
        {
            this.IsActive = true;
        }

        [Display(ResourceType = typeof(Resource), Name = "Id")]
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string Title { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "DocSign")]
        public Int16 Sign { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "DocPrefix")]
        //[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public string DocPrefix { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Status")]
        public bool IsActive { get; set; }


        public virtual ICollection<InvDoc> InvDocs 
        {
            get { return this.invDocs ?? (this.invDocs = new HashSet<InvDoc>()); }
            set { this.invDocs = value; }
        }

    }
}
