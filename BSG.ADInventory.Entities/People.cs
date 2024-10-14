using BSG.ADInventory.Common.Enum;
using BSG.ADInventory.Entities;
using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BSG.ADInventory.Entities
{
    public class People : BaseEntity
    {     
        private ICollection<User> users;             
        private ICollection<Car> ownerDrivers;
        private ICollection<InvDoc> invDocs;

        public People()
        {
            this.IsActive = true;
            this.Gender = Common.Enum.Gender.Male;
            this.PeopleType = PeopleType.Employee;
        }

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

        [Display(ResourceType = typeof(Resource), Name = "SignatureImage")]
        public string SignatureImageUrl { get; set; }



        /// <summary>
        /// Gets the full name.
        /// </summary>
        [Display(ResourceType = typeof(Resource), Name = "FullName")]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }


        /// <summary>
        /// Gets or sets the Employee Attendances.
        /// </summary>
        /// <value>
        /// The tech specs of Employee Attendance.
        /// </value>
        public virtual ICollection<User> Users
        {
            get { return this.users ?? (this.users = new HashSet<User>()); }

            set { this.users = value; }
        }
        public virtual ICollection<Car> OwnerDriverCars
        {
            get { return this.ownerDrivers ?? (this.ownerDrivers = new HashSet<Car>()); }
            set { this.ownerDrivers = value; }
        }

        public virtual ICollection<InvDoc> InvDocs
        {
            get { return this.invDocs ?? (this.invDocs = new HashSet<InvDoc>()); }
            set { this.invDocs = value; }
        }


    }
}