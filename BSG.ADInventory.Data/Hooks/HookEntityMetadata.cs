using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BSG.ADInventory.Data.Enum;

namespace BSG.ADInventory.Data.Hooks
{
    public class HookEntityMetadata
    {
        public HookEntityMetadata(PspEntityState state)
        {
            _state = state;
        }

        private PspEntityState _state;
        public PspEntityState State
        {
            get { return this._state; }
            set
            {
                if (_state != value)
                {
                    this._state = value;
                    HasStateChanged = true;
                }
            }
        }

        public bool HasStateChanged { get; private set; }
    }
}
