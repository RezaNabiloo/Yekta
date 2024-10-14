namespace BSG.ADInventory.Common.Enum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public enum InvDocStatus
    {
        [Display(ResourceType = typeof(Resources.Resource), Name = "Temporary")]
        Temporary = 1,

        [Display(ResourceType = typeof(Resources.Resource), Name = "Definitive")]
        Definitive = 2,

        //[Display(ResourceType = typeof(Resources.Resource), Name = "Exited")]
        //Exited = 3,

        //[Display(ResourceType = typeof(Resources.Resource), Name = "Recived")]
        //DestRecived = 4,
        
        //[Display(ResourceType = typeof(Resources.Resource), Name = "Delivered")]
        //DestDelivered = 5
    }
}
