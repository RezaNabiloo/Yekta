namespace BSG.ADInventory.Common.Enum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public enum CharacterType
    {

        [Display(ResourceType = typeof(Resources.Resource), Name = "PersonalCharacter")]
        Personal = 1,

        [Display(ResourceType = typeof(Resources.Resource), Name = "LegalCharacter")]
        Legal = 2
    }
}