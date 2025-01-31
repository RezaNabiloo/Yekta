using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BSG.ADInventory.Data.Enum;

namespace BSG.ADInventory.Data.Hooks
{
    /// <summary>
    /// Implements a hook that will run after an entity gets inserted into the database.
    /// </summary>
    public abstract class PostInsertHook<TEntity> : PostActionHook<TEntity>
    {
        /// <summary>
        /// Returns <see cref="EntityState.Added"/> as the hookstate to listen for.
        /// </summary>
        public override PspEntityState HookStates
        {
            get { return PspEntityState.Added; }
        }
    }
}
