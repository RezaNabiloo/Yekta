namespace BSG.ADInventory.Common.Enum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public enum VIPAttendanceType
    {
        [Display(ResourceType = typeof(Resources.Resource), Name = "Employee")]
        Employee = 1,
        [Display(ResourceType = typeof(Resources.Resource), Name = "PlaceAndOrgan")]
        PlaceAndOrgan = 2

        //[Display(ResourceType = typeof(Resources.Resource), Name = "InvitationIn")]
        //InvitationIn = 2,

        //[Display(ResourceType = typeof(Resources.Resource), Name = "InvitationOut")]
        //InvitationOut = 3
    }
}
