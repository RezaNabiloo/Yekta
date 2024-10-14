namespace BSG.ADInventory.Models.User
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using System;
     
    using System.Security.AccessControl;
    using BSG.ADInventory.Resources;

    public class UserInfoSimpleViewModel
    {

        public Guid Id { get; set; }        
        [Display(ResourceType = typeof(Resource), Name = "UserName")]        
        public string UserName { get; set; }     
        public string FullName { get; set; }

    }
}
