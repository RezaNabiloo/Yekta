using BSG.ADInventory.Common.Enum;
using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.Contractor
{
    public class ContractorDataModel
    {
        public long Id { get; set; }
        
        public CharacterType CharacterType { get; set; }      
        
        public string ContractorName { get; set; }

        public string ManagerFirstName { get; set; }

        public string ManagerLastName { get; set; }
        public string NationalCode { get; set; }

        public string EconomicCode { get; set; }
        public long TownshipId { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Tell { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public bool IsActive { get; set; }
    }
}
