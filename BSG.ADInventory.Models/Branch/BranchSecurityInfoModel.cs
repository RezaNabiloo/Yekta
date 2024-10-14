using BSG.ADInventory.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BSG.ADInventory.Models.Branch
{
    public class BranchSecurityInfoModel
    {
        public long BranchId { get; set; }        
        public string PanelId { get; set; }        
        public string PCPassword { get; set; }        
        public string PanelTell { get; set; }        
        public string ParavoxTell { get; set; }        
        public string DuressTell { get; set; }        
        public string SecuritySimCardNo { get; set; }        
        public string InstallerCode { get; set; }        
        public string MasterCode { get; set; }        
        public string IP100 { get; set; }        
        public string IpAddress { get; set; }        
        public string MasterId { get; set; }        
        public string MasterPassword { get; set; }

    }
}
