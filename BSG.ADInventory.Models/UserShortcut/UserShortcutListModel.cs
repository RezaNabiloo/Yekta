using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.UserShortcut
{
    public class UserShortcutListModel
    {
        public long Id { get; set; }
        public long MenuId { get; set; }
        public long? ParentMenuId { get; set; }

        public string Title { get; set; }
        public string ParentTitle { get; set; }

        public string IconClass { get; set; }
        public string ParentIconClass { get; set; }

        public string UrlLink { get; set; }


    }
}
