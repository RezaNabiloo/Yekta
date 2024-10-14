namespace BSG.ADInventory.Models.Message
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class MessageModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageModel" /> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="hasReturnCommand">if set to <c>true</c> [has return command].</param>
        public MessageModel(string title, string description, bool hasReturnCommand = false)
        {
            this.Title = title;
            this.Description = description;
            this.HasReturnCommand = hasReturnCommand;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has return command.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has return command; otherwise, <c>false</c>.
        /// </value>
        public bool HasReturnCommand { get; set; }
    }
}
