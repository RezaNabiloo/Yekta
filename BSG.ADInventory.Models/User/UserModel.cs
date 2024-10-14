namespace BSG.ADInventory.Models.User
{
    using BSG.ADInventory.Entities;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using System;
     
    using System.Security.AccessControl;
    using BSG.ADInventory.Resources;

    public class UserModel
    {
        private List<RoleCheckBoxItem> roles;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserModel"/> class.
        /// </summary>
        public UserModel()
        {
            this.IsActive = true;
            //this.PhoneVerified = false;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        [Display(ResourceType = typeof(Resource), Name = "UserName")]
        [Zcf.Globalization.DataAnnotations.Required]
        [StringLength(256)]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Display(ResourceType = typeof(Resource), Name = "Password")]
        [Zcf.Globalization.DataAnnotations.Required]
        [RegularExpression(@".{6,}", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ShortPasswordLength")]
        [StringLength(256)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Display(ResourceType = typeof(Resource), Name = "Email")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "InvalidEmailFormat")]
        [StringLength(256)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        //[Display(ResourceType = typeof(Resource), Name = "Mobile")]
        //[RegularExpression("^09[0-9]{9}$", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "InvalidMobileFormat")]
        //[StringLength(256)]
        //public string Mobile { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "IsActive")]
        public bool IsActive { get; set; }

        public string Token { get; set; }

      
        public DateTime? TokenExpireTime { get; set; }

        public long? PeopleId { get; set; }

        public virtual People People{ get; set; }
        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public List<RoleCheckBoxItem> Roles
        {
            get { return this.roles ?? (this.roles = new List<RoleCheckBoxItem>()); }
            set { this.roles = value; }
        }

        /// <summary>
        /// Gets or sets the create time.
        /// </summary>
        /// <value>
        /// The create time.
        /// </value>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Gets or sets the last update time.
        /// </summary>
        /// <value>
        /// The last update time.
        /// </value>
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// Gets or sets the last login time.
        /// </summary>
        /// <value>
        /// The last login time.
        /// </value>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// Gets or sets the last activity time.
        /// </summary>
        /// <value>
        /// The last activity time.
        /// </value>
        public DateTime? LastActivityTime { get; set; }
    }
}
