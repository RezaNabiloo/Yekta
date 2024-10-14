namespace BSG.ADInventory.Common.Enum
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public enum NotificationProcessResult
    {
        /// <summary>
        /// Process has been done.
        /// </summary>
        Successful = 1,

        /// <summary>
        /// Process couldn't be done at this time so left in the queue to be processed again.
        /// </summary>
        Unsuccessful = 2,

        /// <summary>
        /// Process couldn't be done at all so removed from the queue.
        /// </summary>
        NotApplicable = 3
    }
}
