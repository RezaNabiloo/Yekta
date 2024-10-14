using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.User
{
    public class UpdateUserRoleModel
    {
        private List<RoleCheckBoxItem> roles;

        public Guid UserId { get; set; }
        public List<RoleCheckBoxItem> Roles
        {
            get { return this.roles ?? (this.roles = new List<RoleCheckBoxItem>()); }
            set { this.roles = value; }
        }
    }
}
