namespace BSG.ADInventory.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using Zcf.Core.Models;
    using Zcf.Security;
    using BSG.ADInventory.Resources;

    public class User : IUser, ICreateTime, ILastUpdateTime
    {
        private ICollection<Role> roles;        
        private ICollection<UserMenu> userMenus;
        private ICollection<Project> projects;
        private ICollection<InvDoc> confirmUsers;
        private ICollection<MatOrder> matOrders;
        private ICollection<PurchaseDoc> agentPurchaseDocs;

        /// <summary>
        /// Initializes a new instance of the <see cref="User" /> class.
        /// </summary>
        public User()
        {
            this.IsActive = false;
            this.MonitoringPersonel = false;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(ResourceType = typeof(Resource), Name = "Id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(ResourceType = typeof(Resource), Name = "UserCode")]
        public long Code { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        [Zcf.Globalization.DataAnnotations.Required]
        [Display(ResourceType = typeof(Resource), Name = "UserName")]
        [StringLength(256)]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Zcf.Globalization.DataAnnotations.Required]
        [Display(ResourceType = typeof(Resource), Name = "Password")]
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
        /// Gets or sets the verify code.
        /// </summary>
        /// <value>
        /// The verify code.
        /// </value>
        //public string VerifyCode { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [Display(ResourceType = typeof(Resource), Name = "UserType")]
        public int Type { get; set; }

        

        /// <summary>
        /// Gets the roles.
        /// </summary>
        public virtual ICollection<Role> Roles
        {
            get
            {
                return this.roles ?? (this.roles = new HashSet<Role>());
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        [Display(ResourceType = typeof(Resource), Name = "IsActive")]
        public bool IsActive { get; set; }

        public string Token { get; set; }

        //public string Phone { get; set; }
        
        //public bool PhoneVerified { get; set; }

        //public string PhoneVerifyCode { get; set; }
        public DateTime? TokenExpireTime { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Person")]
        public long? PeopleId { get; set; }
        public virtual People People { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MonitoringPersonel")]
        public bool MonitoringPersonel { get; set; }
        /// <summary>
        /// Gets or sets the last login time.
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// Gets or sets the last activity time.
        /// </summary>
        public DateTime? LastActivityTime { get; set; }

        /// <summary>
        /// Gets or sets the create time.
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Gets or sets the last update time.
        /// </summary>
        public DateTime LastUpdateTime { get; set; }

        //public virtual string FullName
        //{
        //    get
        //    {
        //        return string.Format("{0} {1}", ((this.UserName.ToLower() == "admin") ? "مدیر" : (this.People == null ? "" : this.People.FirstName)), ((this.UserName.ToLower() == "admin") ? "سیستم" : (this.People == null ? "" : this.People.LastName)));
        //    }
        //}
        
        [Display(ResourceType = typeof(Resource), Name = "FullName")]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.People==null?string.Empty:this.People.FirstName, this.People == null ? string.Empty : this.People.LastName);
            }
        }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        IEnumerable<IRole> IUser.Roles
        {
            get { return this.Roles; }
        }
        public virtual ICollection<UserMenu> UserMenus
        {
            get { return this.userMenus ?? (this.userMenus = new HashSet<UserMenu>()); }

            set { this.userMenus = value; }
        }
        
        public virtual ICollection<Project> Projects
        {
            get
            {
                return this.projects ?? (this.projects = new HashSet<Project>());
            }
            set
            {
                this.projects = value;
            }
        }

        public virtual ICollection<InvDoc> ConfirmUsers
        {
            get { return this.confirmUsers ?? (this.confirmUsers = new HashSet<InvDoc>()); }
            set { this.confirmUsers = value; }
        }

        public virtual ICollection<MatOrder> MatOrders
        {
            get { return this.matOrders ?? (this.matOrders = new HashSet<MatOrder>()); }
            set { this.matOrders = value; }
        }


        public virtual ICollection<PurchaseDoc> AgentPurchaseDocs
        {
            get { return this.agentPurchaseDocs ?? (this.agentPurchaseDocs = new HashSet<PurchaseDoc>()); }
            set { this.agentPurchaseDocs = value; }
        }
    }
}
