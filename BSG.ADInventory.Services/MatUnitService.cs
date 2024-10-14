namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.Mat;
    using System.Collections.Generic;
    using System.Linq;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;
	
		  
    /// <summary>
    /// Create Service For MatUnit" />
    /// </summary>
    public class MatUnitService : CrudService<MatUnit>, IMatUnitService
    {
        private readonly IMatUnitRepository _matUnitRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatUnitService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="MatUnitRepository"></param>
        public MatUnitService(IUnitOfWork unitOfWork, IMatUnitRepository matUnitRepository)
            : base(unitOfWork, matUnitRepository)
        {
            _matUnitRepository = matUnitRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<MatUnit> GetItems(PagedQueryable criteria)
        {
            var data = _matUnitRepository.Query;
            return data.ToPagedQueryable(criteria);
        }

    }
}
