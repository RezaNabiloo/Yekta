using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace BSG.ADInventory.Models.User
{
    public class CustomPrincipal:ICustomPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role) { return false; }

        public CustomPrincipal(string email)
        {
            this.Identity = new GenericIdentity(email);
        }

        public Guid Id { get; set; }
        public long PeopleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public long BranchId { get; set; }
        public long BranchDepartmentId { get; set; }
        public string BranchInfo { get; set; }
        public string BranchDepartmentTitle { get; set; }
        public string TownshipTitle { get; set; }
        public string ProfileImage { get; set; }

    }
}
