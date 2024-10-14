namespace BSG.ADInventory.Models.Role
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public class PermissionCheckBoxItem
    {
        /// <summary>
        /// Gets or sets the permission id.
        /// </summary>
        /// <value>
        /// The permission id.
        /// </value>
        public string PermissionId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        
        [DisplayName(" ")]
        public bool IsSelected { get; set; }

        public string Category { get; set; }
        public int CategoryId { get; set; }
        public string HelpDescription { get; set; }
    }
}
