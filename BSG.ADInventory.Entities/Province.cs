namespace BSG.ADInventory.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Resources;

    public class Province : BaseEntity
    {
        public Province()
        {
            this.IsActive = true;
            this.Lat = 0;
            this.Lon = 0;
        }
        private ICollection<Township> townships;

        /// <summary>
        /// Gets or sets the Country id.
        /// </summary>
        /// <value>
        /// The Country id.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "Country")]
        public long CountryId { get; set; }

        /// <summary>
        /// Gets or sets the Country.
        /// </summary>
        /// <value>
        /// The Country.
        /// </value>
        public virtual Country Country { get; set; }

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
        /// Gets or sets the area code.
        /// </summary>
        /// <value>
        /// The area code.
        /// </value>
        //[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "AreaCode")]
        public string AreaCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "IsActive")]
        public bool IsActive { get; set; }


        /// <summary>
        /// Gets or sets the latitudes.
        /// </summary>

        [Display(ResourceType = typeof(Resource), Name = "Lat")]
        public double? Lat { get; set; }


        /// <summary>
        /// Gets or sets the longitudes.
        /// </summary>
        [Display(ResourceType = typeof(Resource), Name = "Lon")]        
        public double? Lon { get; set; }

        /// <summary>
        /// Gets or sets the townships.
        /// </summary>
        /// <value>
        /// The townships.
        /// </value>
        public virtual ICollection<Township> Townships
        {
            get
            {
                return this.townships ?? (this.townships = new HashSet<Township>());
            }

            set
            {
                this.townships = value;
            }
        }
    }
}
