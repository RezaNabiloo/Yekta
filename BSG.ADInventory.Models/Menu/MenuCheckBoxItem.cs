using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.Menu
{
    public class MenuCheckBoxItem
    {
        /// <summary>
        /// Gets or sets the menu id.
        /// </summary>
        /// <value>
        /// The menu id.
        /// </value>
        public long MenuId { get; set; }
        /// <summary>
        /// Gets or sets the parent menu id.
        /// </summary>
        /// <value>
        /// The parent menu id.
        /// </value>
        public long? ParentMenuId { get; set; }
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelected { get; set; }
    }
}
