using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.Menu
{
    public class MenuDataModel
    {
        public long Id { get; set; }
        public long? ParentMenuId { get; set; }        
        public string Title { get; set; }        
        public string SpecifiedUrl { get; set; }
        public string IconClass { get; set; }

    }
}
