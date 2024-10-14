namespace BSG.ADInventory.Models.Township
{
    using BSG.ADInventory.Entities;


    public class TownshipModel
    {
        public TownshipModel()
        {

        }
        public TownshipModel(Township township)
        {
            Id = township.Id;
            ProvinceId = township.ProvinceId;
            Title = township.Title;            
            IsCapital = township.IsCapital;
            IsActive = township.IsActive;
            Lat = township.Lat;
            Lon = township.Lon;
        }

        public long Id { get; set; }
        public long ProvinceId { get; set; }
        public string Title { get; set; }       

        public bool IsCapital { get; set; }

        public bool IsActive { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; }
    }
}
