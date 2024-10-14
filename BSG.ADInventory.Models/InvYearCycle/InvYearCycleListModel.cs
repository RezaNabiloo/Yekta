using BSG.ADInventory.Resources;
using System.ComponentModel.DataAnnotations;

namespace BSG.ADInventory.Models.InvYearCycle
{
    public class InvYearCycleListModel
    {

        [Display(ResourceType = typeof(Resource), Name = "Id")]
        public long Id { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string Year { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Province")]
        public string Project { get; set; }

        public string MatId { get; set; }

        public string MatTitle { get; set; }

        
        [Display(ResourceType = typeof(Resource), Name = "IsCapital")]
        public string ConfirmedCountQty { get; set; }


        

    }
}
