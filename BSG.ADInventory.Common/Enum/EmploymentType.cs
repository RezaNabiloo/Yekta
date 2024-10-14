namespace BSG.ADInventory.Common.Enum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public enum EmploymentType
    {
        [Display(ResourceType = typeof(Resources.Resource), Name = "OfficialStaff")]
        Rasmi = 1,
        [Display(ResourceType = typeof(Resources.Resource), Name = "ContractStaff")]
        Gharardadi = 2,
        [Display(ResourceType = typeof(Resources.Resource), Name = "AgreementStaff")]
        Peymani = 3,
        [Display(ResourceType = typeof(Resources.Resource), Name = "TimeStaff")]
        Saati = 4,
        [Display(ResourceType = typeof(Resources.Resource), Name = "Corporative")]
        Corporative = 5
    }
}