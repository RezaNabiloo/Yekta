namespace BSG.ADInventory.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Resources;

    public class PlateCharacter : BaseEntity
    {
        private ICollection<Car> cars;
        private ICollection<InvDoc> invDocs;
        
        
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string Title { get; set; }

        /// <summary>
        
        public virtual ICollection<Car> OrgCars {
            get
            {
                return this.cars ?? (this.cars = new HashSet<Car>());
            }
            set
            {
                this.cars = value;
            }
        }

        public virtual ICollection<InvDoc> InvDocs
        {
            get
            {
                return this.invDocs ?? (this.invDocs= new HashSet<InvDoc>());
            }
            set
            {
                this.invDocs = value;
            }
        }

    }
}
