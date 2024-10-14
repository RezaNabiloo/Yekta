using BSG.ADInventory.Common.Enum;
using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BSG.ADInventory.Entities
{
    public class MatGroupTechSpec : BaseEntity
    {
        
        public MatGroupTechSpec()
        {
            this.TechSpecType = TechSpecType.String;
            
        }
        //private ICollection<MatTechSpec> productTechSpecs;

        public long MatGroupId { get; set; }
        public virtual MatGroup MatGroup { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "Title")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public string FaTitle { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "DataType")]
        public TechSpecType TechSpecType { get; set; }

        

        /// <summary>
        /// Gets or sets the devices tech spec.
        /// </summary>
        /// <value>
        /// The devices of tech spec.
        /// </value>
        
    }
}