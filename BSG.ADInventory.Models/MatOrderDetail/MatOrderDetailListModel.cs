using BSG.ADInventory.Common.Enum;
using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.MatOrderDetail
{
    public class MatOrderDetailListModel
    {

        [Display(ResourceType = typeof(Resource), Name = "Id")]
        public long Id { get; set; }

        public long ProjectId { get; set; }

        public string ProjectTitle { get; set; }
        //public string ProjectDescription { get; set; } = string.Empty;

        public string RequestDescription { get; set; }

        public DateTime RequiredDate { get; set; }

        public int MatCount { get; set; }

        public string RequestMatDescription { get; set; }

        public string ConfirmUser { get; set; }

        public DateTime? ConfirmTime { get; set; }
        public ConfirmStatus ConfirmStatus { get; set; }

    }
}
