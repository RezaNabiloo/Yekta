using BSG.ADInventory.Models.BaseModel;
using BSG.ADInventory.Resources;
using System.ComponentModel.DataAnnotations;

namespace BSG.ADInventory.Models.ProjectDetail
{
    public class ProjectDetailListDTO:BaseDTO
    {
        //public long ProjectId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string Title { get; set; }        
        
        [Display(ResourceType = typeof(Resource), Name = "Description")]
        public string Description { get; set; }        
                
        [Display(ResourceType = typeof(Resource), Name = "Active")]
        public string IsActive { get; set; }

        public long ProjectId{ get; set; }
    }
}
