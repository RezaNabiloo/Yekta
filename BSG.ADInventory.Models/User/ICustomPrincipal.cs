using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.User
{
    interface ICustomPrincipal:IPrincipal
    {
        Guid Id { get; set; }
        long PeopleId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        long BranchId { get; set; }
        long BranchDepartmentId { get; set; }
        string BranchInfo { get; set; }
        string BranchDepartmentTitle { get; set; }
        string TownshipTitle { get; set; }
        string ProfileImage { get; set; }
    }
}
