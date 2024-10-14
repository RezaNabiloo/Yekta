namespace BSG.ADInventory.Services
{
    using AutoMapper;
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.MatGroup;
    using System.Collections.Generic;
    using System.Linq;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;


    /// <summary>
    /// Create Service For MatGroup" />
    /// </summary>
    public class MatGroupService : CrudService<MatGroup>, IMatGroupService
    {
        private readonly IMatGroupRepository _matGroupRepository;
        private readonly IMatRepository _matRepository;
        
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatGroupService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="MatGroupRepository"></param>
        public MatGroupService(IUnitOfWork unitOfWork, IMatGroupRepository matGroupRepository, 
            IMatRepository matRepository, 
            IMapper mapper)
            : base(unitOfWork, matGroupRepository)
        {
            _matGroupRepository = matGroupRepository;
            _matRepository = matRepository;            
            _mapper = mapper;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<MatGroup> GetItems(PagedQueryable criteria)
        {
            var data = _matGroupRepository.Query;
            return data.ToPagedQueryable(criteria);
        }

        public List<MatGroupVM> GetChildMatGroups(long id)
        {
            var query = _matGroupRepository.Query.Where(pg=>pg.ParentMatGroupId==id);
            return _mapper.Map<List<MatGroupVM>>(query);

        }

    }
}
