using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.Device
{
    public class DeviceViewModel
    {
        public long Id { get; set; }
        public long MatGroupId { get; set; }
        public string MatGroupTitle { get; set; }
        public long BranchId { get; set; }

        public long? BrandId { get; set; }
        public string BrandTitle { get; set; }
        public string Title { get; set; }
        public string DeviceModel { get; set; }
        public string AssetNo { get; set; }
        public string IPAddress { get; set; }
        public DateTime? ProductDate { get; set; }
        public DateTime? EstablishDate { get; set; }

        public string ProductDateString { get; set; }
        public string EstablishDateString { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}



