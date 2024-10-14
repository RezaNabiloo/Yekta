namespace BSG.ADInventory.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Resources;

    public class Country : BaseEntity
    {
        private ICollection<Province> provinces;
        

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string Title { get; set; }


        //[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "TitleEn")]
        public string TitleEn { get; set; }
        /// <summary>
        /// Gets or sets the provinces.
        /// </summary>
        /// <value>
        /// The provinces.
        /// </value>
        public virtual ICollection<Province> Provinces {
            get
            {
                return this.provinces ?? (this.provinces = new HashSet<Province>());
            }
            set
            {
                this.provinces = value;
            }
        }

       
    }
}
