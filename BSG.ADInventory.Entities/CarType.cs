using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Entities
{
    public class CarType : BaseEntity
    {
        private ICollection<Car> cars;
        private ICollection<InvDoc> invDocs;
        
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string Title { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "Description")]        
        public string Description { get; set; }

        public virtual ICollection<Car> Cars
        {
            get { return this.cars ?? (this.cars = new HashSet<Car>()); }
            set { this.cars = value; }
        }
        public virtual ICollection<InvDoc> InvDocs
        {
            get { return this.invDocs ?? (this.invDocs = new HashSet<InvDoc>()); }
            set { this.invDocs = value; }
        }

    }
}
