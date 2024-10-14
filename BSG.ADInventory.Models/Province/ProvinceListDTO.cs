using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.Province
{
    public class ProvinceListDTO
    {   
        public long Id { get; set; }     
        public string Title { get; set; }        
        public string Country { get; set; }        
        public string AreaCode { get; set; }        
        public string IsActive { get; set; }       
        public double? Lat { get; set; }        
        public double? Lon { get; set; }
    }
}
