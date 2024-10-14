namespace BSG.ADInventory.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Entities;
    using Zcf.Services;
    using Models.MatGroup;

    /// <summary>
    /// Create Interface for MatGroupService
    /// </summary>
    public interface IMatGroupService : ICrudService<MatGroup>
    {
        MatGroupModel AddMatGroup(MatGroupModel dataModel);

        MatGroupModel UpdateMatGroup(MatGroupModel dataModel);

        long RemoveMatGroup(long id);
    }
}
