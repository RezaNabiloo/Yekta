
using BSG.ADInventory.Models.BaseModel;
using BSG.ADInventory.Resources;
using System.ComponentModel.DataAnnotations;

namespace BSG.ADInventory.Models.CarType
{
    public class CarTypeListDTO:BaseDTO
    {


        [Display(ResourceType = typeof(Resource), Name = "Title")]
        public string Title { get; set; }

        public string Description { get; set; }


    }
}
