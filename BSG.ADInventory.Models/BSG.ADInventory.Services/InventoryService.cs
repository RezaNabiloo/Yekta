namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.Inventory;
    using System.Collections.Generic;
    using System.Linq;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;
	
		  
    /// <summary>
    /// Create Service For Inventory" />
    /// </summary>
    public class InventoryService : CrudService<Inventory>, IInventoryService
    {
        private readonly IInventoryRepository inventoryRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="InventoryRepository"></param>
        public InventoryService(IUnitOfWork unitOfWork, IInventoryRepository inventoryRepository)
            : base(unitOfWork, inventoryRepository)
        {
            this.inventoryRepository = inventoryRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<Inventory> GetItems(PagedQueryable criteria)
        {
            var data = this.inventoryRepository.Query;
            return data.ToPagedQueryable(criteria);
        }

        public IEnumerable<InventoryListModel> GetDataList()
        {
            var data = this.inventoryRepository.GetAllItems().Select(t => new InventoryListModel {
                Id = t.Id,
                Title = t.Title,
                Project = t.Project.Title,
                Province = t.Project.Township.Province.Title,                
                Township = t.Project.Township.Title,
                IsActive= t.IsActive,
                Address = t.Address,
                Lat = t.Lat,
                Lon = t.Lon
            }).ToList();

            return data;
        }
    }
}
