using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.Account
{
    public class SimpleLoginModel
    {
        /// <summary>
        ///   Gets or sets the name of the user.
        /// </summary>
        /// <value> The name of the user. </value>
        [Display(ResourceType = typeof(Resource), Name = "UserName")]
        [Zcf.Globalization.DataAnnotations.Required]
        public string UserName { get; set; }

        /// <summary>
        ///   Gets or sets the password.
        /// </summary>
        /// <value> The password. </value>
        [Display(ResourceType = typeof(Resource), Name = "Password")]
        [Zcf.Globalization.DataAnnotations.Required]
        public string Password { get; set; }
    }
}
