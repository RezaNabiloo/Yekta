using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Entities
{
    public class Brand : BaseEntity
    {
        private ICollection<Mat> mats;

        public Brand()
        {
            IsActive = true;
            this.EnTitle = string.Empty;
        }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "FaTitle")]
        public string FaTitle { get; set; }
        
        [Display(ResourceType = typeof(Resource), Name = "EnTitle")]
        public string EnTitle { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Picture")]
        public string ImageUrl { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Status")]
        public bool IsActive { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "Description")]        
        public string Description { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Brand")]
        public string BrandInfo
        {
            get
            {
                return string.Format("{0} {1}", this.FaTitle, this.EnTitle??string.Empty);
            }
        }

        public virtual ICollection<Mat> Mats
        {
            get { return this.mats ?? (this.mats = new HashSet<Mat>()); }
            set { this.mats = value; }
        }

        
    }
}
