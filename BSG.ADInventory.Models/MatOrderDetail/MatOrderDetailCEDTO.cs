using BSG.ADInventory.Models.BaseModel;

namespace BSG.ADInventory.Models.MatOrderDetail
{
    public class MatOrderDetailCEDTO:BaseDTO
    {   
        public long MatOrderId { get; set; }
        public long MatId { get; set; }
        public string Title { get; set; }
        public float RequiredAmount { get; set; }
        public float ConfirmedAmount { get; set; }
        public string Description { get; set; }

    }
}
