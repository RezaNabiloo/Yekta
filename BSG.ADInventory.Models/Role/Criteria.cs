namespace BSG.ADInventory.Models.Role
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using BSG.ADInventory.Resources;
    using Zcf.Paging;

    public class Criteria : PagedQueryable
    {
        [Display(ResourceType = typeof(Resource), Name = "RoleName")]
        public string Name { get; set; }
    }
}
