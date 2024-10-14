using BSG.ADInventory.Common.Enum;
using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.Report
{
    public class MatStockRepParam
    {
        public long? MatId { get; set; }
        public long? ProjectId { get; set; }        
        public string MatGroupIds { get; set; }

    }
}
