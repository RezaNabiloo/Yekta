namespace BSG.ADInventory.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Resources;

    public class Township : BaseEntity
    {
        private ICollection<Project> projects;
        //private ICollection<Provider> providers;
        public Township()
        {
            this.IsActive = true;
            this.IsCapital = false;
            this.Lat = 0;
            this.Lon = 0;
        }

        /// <summary>
        /// Gets or sets the province id.
        /// </summary>
        /// <value>
        /// The province id.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "Province")]
        public long ProvinceId { get; set; }

        /// <summary>
        /// Gets or sets the province.
        /// </summary>
        /// <value>
        /// The province.
        /// </value>
        public virtual Province Province { get; set; }

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
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "IsCapital")]
        public bool IsCapital { get; set; }

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
        
        public virtual ICollection<Project> Projects
        {
            get
            {
                return this.projects ?? (this.projects = new HashSet<Project>());
            }
            set { this.projects = value; }
        }
        //public virtual ICollection<Provider> Providers
        //{
        //    get
        //    {
        //        return this.providers ?? (this.providers = new HashSet<Provider>());
        //    }
        //    set { this.providers = value; }
        //}


    }
}
