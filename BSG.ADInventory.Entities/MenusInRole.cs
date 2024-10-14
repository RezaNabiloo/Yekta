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

    public class MenusInRole : ICreateTime, ICreateUser
    {
        /// <summary>
        /// Gets or sets the permission id.
        /// </summary>
        /// <value>
        /// The permission id.
        /// </value>
        [Key]
        [Column(Order = 1)]
        public long MenuId { get; set; }
        public virtual Menu Menu { get; set; }

        /// <summary>
        /// Gets or sets the role id.
        /// </summary>
        /// <value>
        /// The role id.
        /// </value>
        [Key]
        [Column(Order = 2)]
        public Guid RoleId { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        [Zcf.Globalization.DataAnnotations.Required]
        public virtual Role Role { get; set; }

        /// <summary>
        /// Gets or sets the last update time.
        /// </summary>
        /// <value>
        /// The last update time.
        /// </value>
        public DateTime CreateTime { get; set; }      

        public Guid? CreateUserId { get; set; }

        public virtual User CreateUser { get; set; }

    }
}
