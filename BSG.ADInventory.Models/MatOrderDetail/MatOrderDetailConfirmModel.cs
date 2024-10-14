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
    public class MatOrderDetailConfirmModel
    {

        [Display(ResourceType = typeof(Resource), Name = "Id")]
        public long Id { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Material")]
        public long MatId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string Title { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MatUnit")]
        public string MatUnit { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "RequiredAmount")]
        public float RequiredAmount { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "ConfirmedAmount")]
        [RegularExpression("^([0-9]*[0-9][0-9]*(.[0-9]+)?|[0]+.[0-9]*[1-9][0-9]*)$", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "CanNotInsertNegativeNumber")]
        public float ConfirmedAmount { get; set; }

        public string ConfirmDescription { get; set; }


    }
}
