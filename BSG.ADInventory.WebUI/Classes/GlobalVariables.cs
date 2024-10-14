using BSG.ADInventory.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSG.ADInventory.WebUI.Classes
{
    public static class GlobalVariables
    {
        public static CustomPrincipal CurrentUser { get; set; }
    }
}