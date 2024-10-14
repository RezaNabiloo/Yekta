using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.User
{
    public class CustomPrincipalSerializeModel
    {
        public Guid Id { get; set; }
        public long PeopleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public long BranchId { get; set; }
        public long BranchDepartmentId { get; set; }
        public string BranchInfo { get; set; }
        public string BranchDepartmentTitle { get; set; }
        public string TownshipTitle { get; set; }
        public string ProfileImage { get; set; }
        //public List<string> Permissions { get; set; }
    }
}
