using BSG.ADInventory.Common.Enum;
using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.Provider
{
    public class ProviderDataModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long TownshipId { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Tell { get; set; }
        public string Fax { get; set; }        
        public bool IsActive { get; set; }
    }
}
