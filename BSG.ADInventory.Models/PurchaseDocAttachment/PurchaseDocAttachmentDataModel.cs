using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.PurchaseDocAttachment
{
    public class PurchaseDocAttachmentDataModel
    {

        [Display(ResourceType = typeof(Resource), Name = "Id")]
        public long Id { get; set; }

        public long PurchaseDocId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string Title { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "File")]
        public string FilePath { get; set; }

    }
}
