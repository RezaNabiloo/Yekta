namespace BSG.ADInventory.Models.People
{
    using BSG.ADInventory.Common.Enum;
    using BSG.ADInventory.Models.BaseModel;
    using BSG.ADInventory.Resources;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PeopleCEDTO:BaseDTO
    {

        [Display(ResourceType = typeof(Resource), Name = "Status")]
        public bool IsActive { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "EmployeeNumber")]
        public string EmployeeNumber { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "IdNo")]
        public string IdNo { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "NationalCode")]
        public string NationalCode { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "FirstName")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public string FirstName { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "LastName")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public string LastName { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "FatherName")]
        public string FatherName { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "BirthDate")]
        public DateTime? BirthDate { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "EmployeeDate")]
        public DateTime? EmploymentDate { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "EmployeeType")]
        public EmploymentType? EmploymentType { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "EducationLevel")]
        public EducationLevel? EducationLevel { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "ImageUrl")]
        [UIHint("Upload")]
        public string ImageUrl { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "SignatureImage")]
        [UIHint("Upload")]
        public string SignatureImageUrl { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "Gender")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public Gender? Gender { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "PeopleType")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        public PeopleType PeopleType { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Address")]
        public string Address { get; set; }



    }
}
