using BSG.ADInventory.Models.BaseModel;

namespace BSG.ADInventory.Models.Township
{
    public class TownshipListDTO: BaseModel.BaseDTO
    {
        public string Title { get; set; }
        public string Province { get; set; }
        public string IsCapital { get; set; }
        public string IsActive { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; }

    }
}
