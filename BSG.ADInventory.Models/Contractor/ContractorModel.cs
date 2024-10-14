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
    public class ContractorModel
    {
        public long Id { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "CharacterType")]
        public CharacterType CharacterType { get; set; }

        public string CharacterTypeTitle { get; set; }

        //[Display(Name = "نام پیمانکار")]
        [Display(ResourceType = typeof(Resource), Name = "CharacterType")]
        public string ContractorName { get; set; }

        //[Display(Name = "نام و نام خانوادگی مدیریت")]
        [Display(ResourceType = typeof(Resource), Name = "FirstName")]
        public string ManagerFirstName { get; set; }

        //[Display(Name = "نام و نام خانوادگی مدیریت")]
        [Display(ResourceType = typeof(Resource), Name = "LastName")]
        public string ManagerLastName { get; set; }

        //[Display(Name = "کد ملی / کد اقتصادی")]
        [Display(ResourceType = typeof(Resource), Name = "NationalCode")]
        public string NationalCode { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "EconomicCode")]
        public string EconomicCode { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Township")]
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


        [Display(ResourceType = typeof(Resource), Name = "Mobile")]
        public string Mobile { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "Active")]
        public bool IsActive { get; set; }
    }
}
