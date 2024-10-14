using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.Device
{
    public class DeviceListModel
    {
        [Display(ResourceType = typeof(Resource), Name = "Id")]
        public long Id { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string Title { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Model")]
        public string DeviceModel { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "AssetNo")]
        public string AssetNo { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "IPAddress")]
        public string IPAddress { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "MatGroup")]
        public string MatGroupTitle { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Branch")]
        public string BranchInfo { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Brand")]
        public string BrandTitle { get; set; }        
        public string ProductDateString { get; set; }
        public string EstablishDateString { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}



