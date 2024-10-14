using BSG.ADInventory.Common.Enum;
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
    public class BranchModel
    {
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "Township")]
        public long TownshipId { get; set; }

        public long? ContractorId { get; set; }
        public CharacterType? CharacterType { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "BranchCode")]
        public string BranchCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "BranchName")]
        public string BranchName { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "IsActive")]
        public bool IsActive { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Address")]
        public string Address { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "PostalCode")]
        public string PostalCode { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "Longitude")]
        public float? Lon { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Latitude")]
        public float? Lat { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "DataLineDocNo")]
        public string DataLineDocNo { get; set; }



        [Display(ResourceType = typeof(Resource), Name = "PanelId")]
        public string PanelId { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "PCPassword")]
        public string PCPassword { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "PanelTell")]
        public string PanelTell { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "ParavoxTell")]
        public string ParavoxTell { get; set; }



        [Display(ResourceType = typeof(Resource), Name = "DuressTell")]
        public string DuressTell { get; set; }
        
        [Display(ResourceType = typeof(Resource), Name = "SecuritySimCardNo")]
        public string SecuritySimCardNo { get; set; }

        
        [Display(ResourceType = typeof(Resource), Name = "InstallerCode")]
        public string InstallerCode { get; set; }
        
        [Display(ResourceType = typeof(Resource), Name = "MasterCode")]
        public string MasterCode { get; set; }
        
        [Display(ResourceType = typeof(Resource), Name = "IP100")]
        public string IP100 { get; set; }
        
        [Display(ResourceType = typeof(Resource), Name = "IpAddress")]
        public string IpAddress { get; set; }
        
        [Display(ResourceType = typeof(Resource), Name = "MasterId")]
        public string MasterId { get; set; }
        
        [Display(ResourceType = typeof(Resource), Name = "MasterPassword")]
        public string MasterPassword { get; set; }

    }
}
