namespace BSG.ADInventory.Common.Enum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public enum PeopleType
    {
        
        [Display(ResourceType = typeof(Resources.Resource), Name = "Employee")]
        Employee = 1,

        [Display(ResourceType = typeof(Resources.Resource), Name = "Contractor")]
        Contractor = 2,

        [Display(ResourceType = typeof(Resources.Resource), Name = "Driver")]
        Driver = 3,

        
        [Display(ResourceType = typeof(Resources.Resource), Name = "OtherIOPeople")]
        OtherIOPeople = 100
    }
}
