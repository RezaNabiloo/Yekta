namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Entities;
    using Data;
    using Models.Brand;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;


    /// <summary>
    /// Create Service For Brand" />
    /// </summary>
    public class BrandService : CrudService<Brand>, IBrandService
    {
        private readonly IBrandRepository brandRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="CountryRepository"></param>
        public BrandService(IUnitOfWork unitOfWork, IBrandRepository brandRepository)
            : base(unitOfWork, brandRepository)
        {
            this.brandRepository = brandRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<Brand> GetItems(PagedQueryable criteria)
        {
            var data = this.brandRepository.Query;
            return data.ToPagedQueryable(criteria);
        }
        public List<BrandDataModel> GetBrandAll()
        {
            var data = (from b in this.brandRepository.GetAllItems()
                        select new BrandDataModel
                        {
                            Id = b.Id,
                            FaTitle = b.FaTitle,
                            EnTitle = b.EnTitle,
                            ImageUrl = b.ImageUrl,
                            IsActive = b.IsActive,
                            Description = b.Description

                        }
                        ).ToList();

            return data;
        }
        public BrandDataModel AddBrand(BrandDataModel dataModel)
        {
            Brand model = new Brand
            {
                FaTitle = dataModel.FaTitle,
                EnTitle = dataModel.EnTitle,
                ImageUrl = dataModel.ImageUrl,
                IsActive = dataModel.IsActive,
                Description = dataModel.Description
            };

            this.brandRepository.Add(model);
            this.UnitOfWork.Commit();
            return GenerateModel(model);

        }

        public BrandDataModel UpdateBrand(BrandDataModel dataModel)
        {
            try
            {
                var model = this.brandRepository.GetItemByKey(dataModel.Id);
                model.FaTitle = dataModel.FaTitle;
                model.EnTitle = dataModel.EnTitle;
                model.ImageUrl = dataModel.ImageUrl;
                model.IsActive = dataModel.IsActive;
                model.Description = dataModel.Description;
                this.brandRepository.Update(model);
                this.UnitOfWork.Commit();
                return dataModel;

            }
            catch (Exception ex)
            {

                // return null;
            }

            return null;

        }

        public long RemoveBrand(long id)
        {
            try
            {
                var model = this.brandRepository.GetItemByKey(id);
                this.brandRepository.Remove(model);
                this.UnitOfWork.Commit();
                return id;
            }
            catch (Exception ex)
            {

                // return null;
            }

            return 0;

        }


        private BrandDataModel GenerateModel(Brand model)
        {
            BrandDataModel data = new BrandDataModel
            {
                Id = model.Id,
                FaTitle = model.FaTitle,
                EnTitle = model.EnTitle,
                ImageUrl = model.ImageUrl,
                IsActive = model.IsActive,
                Description = model.Description
            };
            return data;
        }

        public IEnumerable<BrandListModel> GetDataList()
        {
            var data = this.brandRepository.GetAllItems().Select(t => new BrandListModel
            {
                Id = t.Id,
                FaTitle = t.FaTitle,
                EnTitle = t.EnTitle,
                ImageUrl = t.ImageUrl,
                IsActive= t.IsActive,
                Description = t.Description
            }).ToList();

            return (data);
        }
    }
}

