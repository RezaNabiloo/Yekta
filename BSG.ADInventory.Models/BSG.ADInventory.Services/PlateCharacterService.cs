namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;
	
		  
    /// <summary>
    /// Create Service For PlateCharacter" />
    /// </summary>
    public class PlateCharacterService : CrudService<PlateCharacter>, IPlateCharacterService
    {
        private readonly IPlateCharacterRepository plateCharacterRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlateCharacterService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="PlateCharacterRepository"></param>
        public PlateCharacterService(IUnitOfWork unitOfWork, IPlateCharacterRepository plateCharacterRepository)
            : base(unitOfWork, plateCharacterRepository)
        {
            this.plateCharacterRepository = plateCharacterRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<PlateCharacter> GetItems(PagedQueryable criteria)
        {
            var data = this.plateCharacterRepository.Query;
            return data.ToPagedQueryable(criteria);
        }
    }
}
