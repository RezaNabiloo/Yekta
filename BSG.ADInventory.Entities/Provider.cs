using BSG.ADInventory.Resources;
using System.ComponentModel.DataAnnotations;

namespace BSG.ADInventory.Entities
{
    public class Provider 
    {        
        public Provider()
        {
            this.IsActive = true;
        }

        [Display(ResourceType = typeof(Resource), Name = "Id")]
        public long Id { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Code")]
        public string Code { get; set; } 

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string Title { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Township")]
        public string Province { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Township")]        
        public string Township { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Tell")]
        public string Tell { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "Fax")]
        public string Fax { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "PostalCode")]
        public string PostalCode { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "Address")]
        public string Address { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Active")]
        public bool IsActive { get; set; }


        
    }
}
