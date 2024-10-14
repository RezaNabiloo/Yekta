namespace BSG.ADInventory.Models.Account
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using BSG.ADInventory.Resources;

    public class ChangePasswordModel
    {
        [Zcf.Globalization.DataAnnotations.Required]
        [Display(ResourceType = typeof(Resource), Name = "OldPassword")]
        public string OldPassword { get; set; }

        [Zcf.Globalization.DataAnnotations.Required]
        [Display(ResourceType = typeof(Resource), Name = "NewPassword")]
        [RegularExpression(@".{6,}", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ShortPasswordLength")]
        public string NewPassword { get; set; }

        [Zcf.Globalization.DataAnnotations.Required]
        [Display(ResourceType = typeof(Resource), Name = "ConfirmPassword")]
        [RegularExpression(@".{6,}", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ShortPasswordLength")]
        //[Compare("NewPassword", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PasswordsDoesNotMatch")]
        public string ConfirmPassword { get; set; }
    }
}