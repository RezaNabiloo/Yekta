using BSG.ADInventory.Common.Enum;
using BSG.ADInventory.Models.BaseModel;
using BSG.ADInventory.Resources;
using System.ComponentModel.DataAnnotations;

namespace BSG.ADInventory.Models.People
{
    public class PeopleListDTO:BaseDTO
    {

        public string PeopleTypeTitle { get; set; }        
        public string IdNo { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "NationalCode")]
        public string NationalCode { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "EmployeeNumber")]
        public string EmployeeNumber { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "FullName")]        
        public string FullName { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "LastName")]        
        public string LastName { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "FatherName")]
        public string FatherName { get; set; }

        
        [Display(ResourceType = typeof(Resource), Name = "BirthDate")]
        public string BirthDate { get; set; }

        public string ImageUrl { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Address")]
        public string Address { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "IsActive")]
        public string IsActive { get; set; }
        
        [Display(ResourceType = typeof(Resource), Name = "Gender")]        
        public string GenderTitle { get; set; }

        public bool HaveSignature { get; set; }

    }
}
