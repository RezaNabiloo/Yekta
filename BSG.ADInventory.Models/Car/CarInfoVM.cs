namespace BSG.ADInventory.Models.Car
{
    using BSG.ADInventory.Models.BaseModel;

    public class CarInfoVM:BaseDTO
    {
        

        public long CarTypeId { get; set; }
        
        public string OwnerDriverName{ get; set; }

        public string OwnerDriverPhoneNumber { get; set; }

        public string PlateSeries { get; set; }

        public long PlateCharacterId { get; set; }

        public string PlateNumberPart1 { get; set; }

        public string PlateNumberPart2 { get; set; }
    }
}
