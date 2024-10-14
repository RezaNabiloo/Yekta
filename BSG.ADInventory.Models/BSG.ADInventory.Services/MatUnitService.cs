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
        private readonly IMatUnitRepository matUnitRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatUnitService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="MatUnitRepository"></param>
        public MatUnitService(IUnitOfWork unitOfWork, IMatUnitRepository matUnitRepository)
            : base(unitOfWork, matUnitRepository)
        {
            this.matUnitRepository = matUnitRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<MatUnit> GetItems(PagedQueryable criteria)
        {
            var data = this.matUnitRepository.Query;
            return data.ToPagedQueryable(criteria);
        }

        public IEnumerable<MatUnitListModel> GetDataList()
        {
            var data = this.matUnitRepository.GetAllItems().Select(t => new MatUnitListModel
            {
                Id = t.Id,
                Title = t.Title,
                Abbreviation = t.Abbreviation
            }).ToList();

            return (data);
        }
    }
}
