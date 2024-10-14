namespace BSG.ADInventory.Models.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BSG.ADInventory.Resources;

    public class DeleteUserViewModel
    {
        [Display(ResourceType = typeof(Resource), Name = "Mobile")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public string Mobile { get; set; }
    }
}
