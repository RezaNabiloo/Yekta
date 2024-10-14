using BSG.ADInventory.Common.Enum;
using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.MatInv
{
    public class MatInvViewModel
    {
        [Display(ResourceType = typeof(Resource), Name = "Id")]
        public long Id { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Mat")]
        public long MatId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Branch")]
        public string BranchInfo { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "DocType")]
        public InOutType InOutType { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Qty")]
        public long Qty { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "AssetNo")]
        public string AssetNo { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "SerialNumber")]
        public string SerialNumber { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Model")]
        public string Model { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Description")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "CreateTime")]
        public DateTime CreateTime { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "CreateUser")]
        public string CreateUserInfo { get; set; }

    }
}
