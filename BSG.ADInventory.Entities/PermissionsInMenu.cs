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

    public class PermissionsInMenu : ICreateTime, ILastUpdateTime
    {
        /// <summary>
        /// Gets or sets the permission id.
        /// </summary>
        /// <value>
        /// The permission id.
        /// </value>
        [Key]
        [Column(Order = 1)]
        public string PermissionId { get; set; }

        /// <summary>
        /// Gets or sets the role id.
        /// </summary>
        /// <value>
        /// The role id.
        /// </value>
        [Key]
        [Column(Order = 2)]
        public long MenuId { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        [Zcf.Globalization.DataAnnotations.Required]
        public virtual Menu Menu { get; set; }

        /// <summary>
        /// Gets or sets the last update time.
        /// </summary>
        /// <value>
        /// The last update time.
        /// </value>
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// Gets or sets the create time.
        /// </summary>
        /// <value>
        /// The create time.
        /// </value>
        public DateTime CreateTime { get; set; }
    }
}
