namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Common.Enum;
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.Provider;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;


    /// <summary>
    /// Create Service For Country" />
    /// </summary>
    public class ProviderService : CrudService<Provider>, IProviderService
    {
        private readonly IProviderRepository providerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="CountryRepository"></param>
        public ProviderService(IUnitOfWork unitOfWork, BSG.ADInventory.Data.IProviderRepository providerRepository)
            : base(unitOfWork, providerRepository)
        {
            this.providerRepository = providerRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<Provider> GetItems(PagedQueryable criteria)
        {
            var data = this.providerRepository.Query;
            return data.ToPagedQueryable(criteria);
        }

        public IEnumerable<ProviderListModel> GetDataList()
        {
            var data = (from dataModel in this.providerRepository.GetAllItems()
                        select new ProviderListModel
                        {
                            Id = dataModel.Id,
                            Title = dataModel.Title,                            
                            TownshipId = dataModel.TownshipId,
                            Township= dataModel.Township.Title,
                            Province = dataModel.Township.Province.Title,
                            Address = dataModel.Address,
                            PostalCode = dataModel.PostalCode,
                            Tell = dataModel.Tell,
                            Fax = dataModel.Fax,
                            IsActive = dataModel.IsActive
                        }).ToList();

            return data;
        }

        public ProviderListModel GetProviderById(long id)
        {
            var dataModel = this.providerRepository.GetItemByKey(id);
            if (dataModel == null)
            {
                return null;
            }
            ProviderListModel data = new ProviderListModel
            {
                Id = dataModel.Id,
                Title = dataModel.Title,                
                TownshipId = dataModel.TownshipId,
                Township = "",//dataModel.Township.Title,
                Address = dataModel.Address,
                PostalCode = dataModel.PostalCode,
                Tell = dataModel.Tell,
                Fax = dataModel.Fax,
                IsActive = dataModel.IsActive
            };

            return data;
        }
        public long AddProvider(ProviderDataModel dataModel)
        {
            Provider model = new Provider
            {
               
                Title = dataModel.Title,                
                TownshipId = dataModel.TownshipId,               
                Address = dataModel.Address,
                PostalCode = dataModel.PostalCode,
                Tell = dataModel.Tell,
                Fax = dataModel.Fax,                
                IsActive = dataModel.IsActive
            };

            this.providerRepository.Add(model);
            this.UnitOfWork.Commit();
            dataModel.Id = model.Id;
            return dataModel.Id;

        }
        public long UpdateProvider(ProviderDataModel dataModel)
        {
            var model = this.providerRepository.GetItemByKey(dataModel.Id);
            if (model==null)
            {
                return 0;
            }

            try
            {               
                model.Title= dataModel.Title;                
                model.TownshipId = dataModel.TownshipId;
                model.Address = dataModel.Address;
                model.PostalCode = dataModel.PostalCode;
                model.Tell = dataModel.Tell;
                model.Fax = dataModel.Fax;
                model.IsActive = dataModel.IsActive;
                this.providerRepository.Update(model);
                this.UnitOfWork.Commit();
                return dataModel.Id;

            }
            catch (Exception ex)
            {

                // return null;
            }

            return 0;

        }
        public long RemoveProvider(long id)
        {
            try
            {
                var model = this.providerRepository.GetItemByKey(id);
                this.providerRepository.Remove(model);
                this.UnitOfWork.Commit();
                return id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        

    }
}