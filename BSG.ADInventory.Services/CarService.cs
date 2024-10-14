namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Common.Enum;
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.Car;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;


    /// <summary>
    /// Create Service For OrgCar" />
    /// </summary>
    public class CarService : CrudService<Car>, ICarService
    {
        private readonly ICarRepository carRepository;
        private readonly ISqlCommandRepository sqlCommandRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CarService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="OrgCarRepository"></param>
        public CarService(IUnitOfWork unitOfWork, ICarRepository carRepository,
            ISqlCommandRepository sqlCommandRepository)
            : base(unitOfWork, carRepository)
        {
            this.carRepository = carRepository;
            this.sqlCommandRepository = sqlCommandRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<Car> GetItems(PagedQueryable criteria)
        {
            var data = this.carRepository.Query;
            return data.ToPagedQueryable(criteria);
        }

    }
}
