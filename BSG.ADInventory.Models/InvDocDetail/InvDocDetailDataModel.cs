using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.InvDocDetail
{
    public class InvDocDetailDataModel
    {
        public long Id { get; set; }
        public long InvDocId { get; set; }
        
        [Display(ResourceType = typeof(Resource), Name = "Material")]
        public long? MatId { get; set; }        

        [Display(ResourceType = typeof(Resource), Name = "Qty")]
        public float Qty { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Description")]
        public string Description { get; set; }

    }
}
