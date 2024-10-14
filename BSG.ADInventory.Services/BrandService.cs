namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.Brand;
    using DocumentFormat.OpenXml.Wordprocessing;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Data.QueryConvention;
    using Zcf.Paging;
    using Zcf.Services;


    /// <summary>
    /// Create Service For Brand" />
    /// </summary>
    public class BrandService : CrudService<Brand>, IBrandService
    {
        private readonly IBrandRepository _brandRepository;        

        /// <summary>
        /// Initializes a new instance of the <see cref="BrandService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="BrandRepository"></param>
        public BrandService(IUnitOfWork unitOfWork, IBrandRepository brandRepository)
            : base(unitOfWork, brandRepository)
        {
            _brandRepository = brandRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<Brand> GetItems(PagedQueryable criteria)
        {
            var data = _brandRepository.Query;
            return data.ToPagedQueryable(criteria);
        }
    }
}
