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
    public class MatOrderDetailInfoListModel
    {

        [Display(ResourceType = typeof(Resource), Name = "Id")]
        public long Id { get; set; }

        public long MatOrderId { get; set; }
        

        public long ProjectId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Project")]
        public string ProjectTitle { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "RequiredDate")]
        public DateTime RequiredDate { get; set; }

        public long MatId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "RequiredAmount")]
        public float Amount { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MatDescription")]
        public string MatTitle { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "MatUnit")]
        public string MatUnit { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Model")]
        public string Model { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Dimention")]
        public string Dimention { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "TechnicalSpec")]
        public string TechnicalSpec { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Project")]
        public string Project { get; set; }


    }
}
