namespace BSG.ADInventory.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Resources;    

    public class RoleMenu : BaseEntity
    {
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }

        public long MenuId { get; set; }
        public virtual Menu Menu { get; set; }

    }
}
