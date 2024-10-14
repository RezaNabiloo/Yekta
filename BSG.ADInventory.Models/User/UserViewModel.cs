namespace BSG.ADInventory.Models.User
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using System;
     
    using System.Security.AccessControl;
    using BSG.ADInventory.Resources;
    using BSG.ADInventory.Models.Menu;

    public class UserViewModel
    {
        private List<RoleCheckBoxItem> roles;
        private List<MenuModel> menus;

        public Guid Id { get; set; }        
        public string UserName { get; set; }        
        public bool IsActive { get; set; }        
        public string ContractorInfo { get; set; }
        public string BranchInfo { get; set; }
        public string DepartmentInfo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public string ImageUrl { get; set; }
        public List<MenuModel> Menus {
            get { return this.menus ?? (this.menus = new List<MenuModel>()); }
            set { this.menus = value; }
        }
        public List<RoleCheckBoxItem> Roles
        {
            get { return this.roles ?? (this.roles = new List<RoleCheckBoxItem>()); }
            set { this.roles = value; }
        }


    }
}
