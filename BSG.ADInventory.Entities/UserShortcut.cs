namespace BSG.ADInventory.Entities{
    using System;    
    using System.ComponentModel.DataAnnotations;
    
    using BSG.ADInventory.Resources;

    public class UserShortcut 
    {

        [Display(ResourceType = typeof(Resource), Name = "Id")]
        public long Id { get; set; }
        
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public long MenuId { get; set; }
        public virtual Menu Menu { get; set; }



    }
}
