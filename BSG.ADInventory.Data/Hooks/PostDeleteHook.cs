using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSG.ADInventory.Data.Enum;

namespace BSG.ADInventory.Data.Hooks
{
    /// <summary>
    /// Implements a hook that will run after an entity gets deleted from the database.
    /// </summary>
    public abstract class PostDeleteHook<TEntity> : PostActionHook<TEntity>
    {
        public override PspEntityState HookStates
        {
            get { return PspEntityState.Deleted; }
        }
    }
}
