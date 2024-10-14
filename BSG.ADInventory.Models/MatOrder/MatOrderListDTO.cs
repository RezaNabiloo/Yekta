using BSG.ADInventory.Common.Enum;
using BSG.ADInventory.Models.BaseModel;
using System;

namespace BSG.ADInventory.Models.MatOrder
{
    public class MatOrderListDTO:BaseDTO
    {
        public string PriorityTitle { get; set; }
        public string ProjectTitle { get; set; }                
        public DateTime RequiredDate { get; set; }        
        public int MatItemCount { get; set; }        
        public string ConfirmUserInfo { get; set; }
        public DateTime? ConfirmTime { get; set; }
        public ConfirmStatus ConfirmStatus { get; set; }
        public DateTime CreateTime { get; set; }
        public Guid? CreateUserId{ get; set; }
        public string CreateUserInfo { get; set; }
        public int DaysToExpiration { get; set; }
    }
}
