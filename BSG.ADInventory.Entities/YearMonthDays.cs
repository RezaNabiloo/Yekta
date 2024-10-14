namespace BSG.ADInventory.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Resources;

    public class YearMonthDays : BaseEntity
    {
        public string Month { get; set; }
        public int Days { get; set; }
    }
}
