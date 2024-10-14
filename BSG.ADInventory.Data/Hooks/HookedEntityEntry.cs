namespace BSG.ADInventory.Data.Hooks
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;

    public class HookedEntityEntry
    {
        public object Entity { get; set; }
        public BSG.ADInventory.Data.Enum.PspEntityState PreSaveState { get; set; }
    }
}
