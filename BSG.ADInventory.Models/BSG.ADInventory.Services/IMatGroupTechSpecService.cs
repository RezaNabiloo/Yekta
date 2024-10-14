namespace BSG.ADInventory.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.MatGroupTechSpec;
    using Zcf.Services;

    /// <summary>
    /// Create Interface for MatGroupTechSpecService
    /// </summary>
    public interface IMatGroupTechSpecService : ICrudService<MatGroupTechSpec>
    {

        IEnumerable<MatGroupTechSpec> GetMatGroupTechSpecs(long id);
        void AddMatGroupTechSpec(MatGroupTechSpecModel tecSpec);
        MatGroupTechSpecModel GetMatGroupTechSpec(long id);
        bool UpdateMatGroupTechSpec(MatGroupTechSpecModel techSpec);

    }
}