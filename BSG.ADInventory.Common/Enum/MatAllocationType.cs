namespace BSG.ADInventory.Common.Enum
{
    using System.ComponentModel.DataAnnotations;

    public enum MatAllocationType
    {
        [Display(ResourceType = typeof(Resources.Resource), Name = "Public")]
        Public = 1,

        [Display(ResourceType = typeof(Resources.Resource), Name = "AssignToAddress")]
        AssignToAddress = 2
    }
}
