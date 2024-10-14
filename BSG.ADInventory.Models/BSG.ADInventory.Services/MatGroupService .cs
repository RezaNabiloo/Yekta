namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Entities;
    using Data;
    using Models.MatGroup;
    using System;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;


    /// <summary>
    /// Create Service For Country" />
    /// </summary>
    public class MatGroupService : CrudService<MatGroup>, IMatGroupService
    {
        private readonly IMatGroupRepository matGroupRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatGroupService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="matGroupRepository"></param>
        public MatGroupService(IUnitOfWork unitOfWork, IMatGroupRepository matGroupRepository)
            : base(unitOfWork, matGroupRepository)
        {
            this.matGroupRepository = matGroupRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<MatGroup> GetItems(PagedQueryable criteria)
        {
            var data = this.matGroupRepository.Query;
            return data.ToPagedQueryable(criteria);
        }

        public MatGroupModel AddMatGroup(MatGroupModel dataModel)
        {
            MatGroup model = new MatGroup
            {

                Title = dataModel.Title,
                ParentMatGroupId = dataModel.parentId
            };

            this.matGroupRepository.Add(model);
            this.UnitOfWork.Commit();
            return GenerateModel(model);

        }

        public MatGroupModel UpdateMatGroup(MatGroupModel dataModel)
        {
            try
            {
                var model = this.matGroupRepository.GetItemByKey(dataModel.id);
                model.Title = dataModel.Title;
                model.ParentMatGroupId = dataModel.parentId;
                this.matGroupRepository.Update(model);
                this.UnitOfWork.Commit();
                return dataModel;

            }
            catch (Exception ex)
            {

                // return null;
            }

            return null;

        }

        public long RemoveMatGroup(long id)
        {
            try
            {
                var model = this.matGroupRepository.GetItemByKey(id);
                this.matGroupRepository.Remove(model);
                this.UnitOfWork.Commit();
                return id;
            }
            catch (Exception ex)
            {

                // return null;
            }

            return 0;

        }


        private MatGroupModel GenerateModel(MatGroup model)
        {
            MatGroupModel data = new MatGroupModel
            {
                id = model.Id,
                Title = model.Title,
                parentId = model.ParentMatGroupId,
                icon = (model.ChildMatGroups.Count > 0 ? "activefolder" : "file"),
                isDirectory = (model.ChildMatGroups.Count > 0 ? true : false)
            };
            return data;
        }
        
    }
}
