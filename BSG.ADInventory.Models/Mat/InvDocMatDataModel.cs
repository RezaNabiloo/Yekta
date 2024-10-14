using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.Mat
{
    public class InvDocMatDataModel
    {
        public long Id { get; set; }
        public long MatId { get; set; }        
        public string Title { get; set; }
        public string MatUnit { get; set; }
        public long? ProjectDetailId { get; set; }
        public string ProjectDetailTitle{ get; set; }        
        public float Amount { get; set; }
        public float RealAmount { get; set; }

    }
}



