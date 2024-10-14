namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.Province;
    using System.Collections.Generic;
    using System.Linq;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;	
		  
    /// <summary>
    /// Create Service For province" />
    /// </summary>
    public class ProvinceService : CrudService<Province>, IProvinceService
    {
        private readonly IProvinceRepository provinceRepository;
        private readonly ITownshipRepository townshipRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProvinceService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="provinceRepository"></param>
        public ProvinceService(IUnitOfWork unitOfWork, IProvinceRepository provinceRepository, ITownshipRepository townshipRepository)
            : base(unitOfWork, provinceRepository)
        {
            this.provinceRepository = provinceRepository;
            this.townshipRepository = townshipRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<Province> GetItems(PagedQueryable criteria)
        {
            var data = this.provinceRepository.Query;
            return data.ToPagedQueryable(criteria);
        }
        public IEnumerable<Township> GetTownships(long id)
        {
            var data = this.townshipRepository.Query.Where(t => t.ProvinceId == id || id==0).ToList();
            return (data);
        }
        public IEnumerable<ProvinceListModel> GetDataList()
        {
            var data = this.provinceRepository.GetAllItems().Select(t => new ProvinceListModel
            {
                Id = t.Id,
                Title = t.Title,
                Country = t.Country.Title,
                AreaCode = t.AreaCode,
                IsActive = t.IsActive,
                Lat = t.Lat,
                Lon = t.Lon
            }).ToList();

            return (data);
        }
    }
}
