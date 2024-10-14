namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using System.Linq;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Data.QueryConvention;
    using Zcf.Paging;
    using Zcf.Services;


    /// <summary>
    /// Create Service For Country" />
    /// </summary>
    public class CountryService : CrudService<Country>, ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryTrnService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="CountryRepository"></param>
        public CountryService(IUnitOfWork unitOfWork, ICountryRepository countryRepository)
            : base(unitOfWork, countryRepository)
        {
            _countryRepository = countryRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<Country> GetItems(PagedQueryable criteria)
        {
            var data = _countryRepository.Query;
            return data.ToPagedQueryable(criteria);
        }

    }
}
