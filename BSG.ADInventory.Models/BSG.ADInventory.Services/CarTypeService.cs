namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.CarType;
    using System.Collections.Generic;
    using System.Linq;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;
	
		  
    /// <summary>
    /// Create Service For CarType" />
    /// </summary>
    public class CarTypeService : CrudService<CarType>, ICarTypeService
    {
        private readonly ICarTypeRepository carTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CarTypeService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="CarTypeRepository"></param>
        public CarTypeService(IUnitOfWork unitOfWork, ICarTypeRepository carTypeRepository)
            : base(unitOfWork, carTypeRepository)
        {
            this.carTypeRepository = carTypeRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<CarType> GetItems(PagedQueryable criteria)
        {
            var data = this.carTypeRepository.Query;
            return data.ToPagedQueryable(criteria);
        }

        public IEnumerable<CarTypeListModel> GetDataList() 
        {
            var data = this.carTypeRepository.GetAllItems().Select(c => new CarTypeListModel { Id = c.Id, Title = c.Title }).ToList();
            return data;
        }
    }
}
