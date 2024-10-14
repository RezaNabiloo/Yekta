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

    public class UserMenu : BaseEntity
    {
        public long MenuId { get; set; }
        public virtual Menu Menu { get; set; }

     
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        [Zcf.Globalization.DataAnnotations.Required]
        public virtual User User{ get; set; }

     

    }
}
