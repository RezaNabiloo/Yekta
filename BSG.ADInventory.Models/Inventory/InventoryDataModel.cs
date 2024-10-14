namespace BSG.ADInventory.Models.Inventory
{
    using Entities;


    public class InventoryDataModel
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public string Title { get; set; }       

        public bool IsActive{ get; set; }

        public string Address { get; set; }
        public float? Lat { get; set; }
        public float? Lon { get; set; }
    }
}
