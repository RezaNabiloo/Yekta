namespace BSG.ADInventory.Entities
{
    using BSG.ADInventory.Resources;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class MatUnit : BaseEntity
    {
        private ICollection<Mat> mats;
        

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "Abbreviation")]
        public string Abbreviation { get; set; }


        /// <summary>

        public virtual ICollection<Mat> Mats {
            get { return this.mats ?? (this.mats = new HashSet<Mat>()); }
            set { this.mats = value; }
        }
        
    }
}
