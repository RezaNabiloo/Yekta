using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.User
{
    public class UserListModel
    {
        [Display(ResourceType = typeof(Resource), Name = "Id")]
        public Guid Id { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "NationalCode")]
        public string NationalCode { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "FullName")]
        public string FullName { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "UserName")]
        public string UserName { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "BranchTitle")]
        public string BranchTitle { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "BranchDepartmentTitle")]
        public string BranchDepartmentTitle { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Roles")]
        public string Roles { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "IsActive")]
        public bool IsActive { get; set; }
    }
}
