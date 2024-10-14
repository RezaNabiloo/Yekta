using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.Role
{
    public class RoleListModel
    {

        [Display(ResourceType = typeof(Resource), Name = "Id")]
        public Guid Id { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string Name { get; set; }
        
    }
}
