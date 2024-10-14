namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.Country;
    using System.Collections.Generic;
    using System.Linq;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;
	
		  
    /// <summary>
    /// Create Service For Country" />
    /// </summary>
    public class CountryService : CrudService<Country>, ICountryService
    {
        private readonly ICountryRepository countryRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="CountryRepository"></param>
        public CountryService(IUnitOfWork unitOfWork, ICountryRepository countryRepository)
            : base(unitOfWork, countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<Country> GetItems(PagedQueryable criteria)
        {
            var data = this.countryRepository.Query;
            return data.ToPagedQueryable(criteria);
        }

        public IEnumerable<CountryListModel> GetDataList()
        {
            var data = this.countryRepository.GetAllItems().Select(t => new CountryListModel
            {
                Id = t.Id,
                Title = t.Title,
                TitleEn = t.TitleEn
            }).ToList();

            return (data);
        }
    }
}
