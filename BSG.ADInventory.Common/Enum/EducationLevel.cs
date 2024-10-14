using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSG.ADInventory.Resources;

namespace BSG.ADInventory.Common.Enum
{
    public enum EducationLevel
    {
        [Display(ResourceType = typeof(Resources.Resource), Name = "Illiterate")]
        Illiterate = 1,
        [Display(ResourceType = typeof(Resources.Resource), Name = "ElementarySchool")]
        ElementarySchool = 2,
        [Display(ResourceType = typeof(Resources.Resource), Name = "JuniorSchool")]
        JuniorSchool = 3,
        [Display(ResourceType = typeof(Resources.Resource), Name = "Diploma")]
        Diploma = 4,
        [Display(ResourceType = typeof(Resources.Resource), Name = "UpDiploma")]
        UpDiploma = 5,
        [Display(ResourceType = typeof(Resources.Resource), Name = "Bachelor")]
        Bachelor = 6,
        [Display(ResourceType = typeof(Resources.Resource), Name = "MA")]
        MA = 7,
        [Display(ResourceType = typeof(Resources.Resource), Name = "PHD")]
        PHD = 8
    }
}
