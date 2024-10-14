using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Entities
{
    public class MatGroup : BaseEntity
    {
        private ICollection<MatGroup> childMatGroups;
        private ICollection<Mat> mats;        
        private ICollection<MatGroupTechSpec> matGroupTechSpecs;

        [Display(ResourceType = typeof(Resource), Name = "Title")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public string Title { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "ParentGroup")]
        public long? ParentMatGroupId { get; set; }
        public virtual MatGroup ParentMatGroup { get; set; }

        /// <summary>
        /// Gets or sets the child material groups.
        /// </summary>
        /// <value>
        /// The childs of material group.
        /// </value>
        public virtual ICollection<MatGroup> ChildMatGroups
        {
            get { return this.childMatGroups ?? (this.childMatGroups = new HashSet<MatGroup>()); }

            set { this.childMatGroups = value; }
        }

        /// <summary>
        /// Gets or sets the child mats.
        /// </summary>
        /// <value>
        /// The materials of material group.
        /// </value>
        public virtual ICollection<Mat> Mats
        {
            get { return this.mats ?? (this.mats = new HashSet<Mat>()); }

            set { this.mats = value; }
        }

        /// <summary>
        /// Gets or sets the child devices.
        /// </summary>
        /// <value>
        /// The devices of material group.
        /// </value>
        
        /// <summary>
        /// Gets or sets the tech specs.
        /// </summary>
        /// <value>
        /// The tech specs of material group.
        /// </value>
        public virtual ICollection<MatGroupTechSpec> MatGroupTechSpecs
        {
            get { return this.matGroupTechSpecs ?? (this.matGroupTechSpecs = new HashSet<MatGroupTechSpec>()); }

            set { this.matGroupTechSpecs = value; }
        }
    }
}
