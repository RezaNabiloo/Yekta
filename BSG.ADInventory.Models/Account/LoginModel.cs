namespace BSG.ADInventory.Models.Account
{
    using System.ComponentModel.DataAnnotations;
    using BSG.ADInventory.Resources;

    public class LoginModel
    {
        /// <summary>
        ///   Gets or sets the name of the user.
        /// </summary>
        /// <value> The name of the user. </value>
        [Display(ResourceType = typeof(Resource), Name = "UserName")]
        [Zcf.Globalization.DataAnnotations.Required]
        public string LoginUserName { get; set; }

        /// <summary>
        ///   Gets or sets the password.
        /// </summary>
        /// <value> The password. </value>
        [Display(ResourceType = typeof(Resource), Name = "Password")]
        [Zcf.Globalization.DataAnnotations.Required]
        public string LoginPassword { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether remember me.
        /// </summary>
        /// <value> <c>true</c> if remember me; otherwise, <c>false</c> . </value>
        [Display(ResourceType = typeof(Resource), Name = "RememberMe")]
        public bool RememberMe { get; set; }

        /// <summary>
        ///   Gets or sets the return URL.
        /// </summary>
        /// <value> The return URL. </value>
        public string ReturnUrl { get; set; }
    }
}