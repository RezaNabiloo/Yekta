namespace BSG.ADInventory.Services
{
    using System;
    using System.Collections.Generic;
    using BSG.ADInventory.Entities;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;
    using Data;
    using System.Linq;
    using Models.Township;

    /// <summary>
    /// Create Service For township" />
    /// </summary>
    public class TownshipService : CrudService<Township>, ITownshipService
    {

        private readonly ITownshipRepository townshipRepository;        
        private readonly ISqlCommandRepository sqlCommandRepository;
        private readonly IProjectRepository projectRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TownshipService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="townshipRepository"></param>
        public TownshipService(IUnitOfWork unitOfWork, ITownshipRepository townshipRepository,ISqlCommandRepository sqlCommandRepository,
            IProjectRepository projectRepository)
            : base(unitOfWork, townshipRepository)
        {

            this.townshipRepository = townshipRepository;            
            this.sqlCommandRepository = sqlCommandRepository;
            this.projectRepository = projectRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<Township> GetItems(PagedQueryable criteria)
        {
            var data = this.townshipRepository.Query;
            return data.ToPagedQueryable(criteria);
        }


        public long? GetProvinceId(long? id)
        {
            if (id == null)
            {
                return null;
            }

            var township = this.townshipRepository.Query.Where(t => t.Id == id).FirstOrDefault();
            if (township != null)
            {
                return township.ProvinceId;
            }
            return null;
        }

        public Province GetProvince(long? id)
        {
            if (id == null)
            {
                return null;
            }

            Province data = this.townshipRepository.Query.Where(t => t.Id == id).FirstOrDefault().Province;
            return data;
        }

        public IEnumerable<Project> GetProjects(long id)
        {
            var data = this.projectRepository.Query.Where(t => t.TownshipId == id || id == 0).ToList();
            return (data);
        }
        public IEnumerable<TownshipListModel> GetDataList()
        {
            var data = this.townshipRepository.GetAllItems().Select(t => new TownshipListModel
            {
                Id = t.Id,
                Title = t.Title,
                Province = t.Province.Title,
                IsCapital = t.IsCapital,
                IsActive = t.IsActive,
                Lat = t.Lat,
                Lon = t.Lon
            }).ToList();

            return (data);
        }

      
    }
}
