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
    public class ProviderModel
    {
        public long Id { get; set; }
        
        public string Title { get; set; }

        
        public long TownshipId { get; set; }
        public string TownshipTitle { get; set; }

        //[Display(Name = "آدرس")]
        [Display(ResourceType = typeof(Resource), Name = "Address")]
        public string Address { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "PostalCode")]
        public string PostalCode { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Tell")]
        public string Tell { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Fax")]
        public string Fax { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Active")]
        public bool IsActive { get; set; }
    }
}
