namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.Menu;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;


    /// <summary>
    /// Create Service For Menu" />
    /// </summary>
    public class MenuService : CrudService<Menu>, IMenuService
    {
        private readonly BSG.ADInventory.Data.IMenuRepository menuRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="MenuRepository"></param>
        public MenuService(IUnitOfWork unitOfWork, IMenuRepository menuRepository)
            : base(unitOfWork, menuRepository)
        {
            this.menuRepository = menuRepository;
        }


        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<Menu> GetItems(PagedQueryable criteria)
        {
            var data = this.menuRepository.Query;
            return data.ToPagedQueryable(criteria);
        }



        public List<MenuDataModel> GetMenuById(long id)
        {

            var data = (from m in this.menuRepository.GetAllItems()
                             where m.Id == id || id == 0
                             select new MenuDataModel
                             {
                                 Id = m.Id,
                                 ParentMenuId = m.ParentMenuId,
                                 Title = m.Title,
                                 SpecifiedUrl = m.SpecifiedUrl,
                                 IconClass = m.IconClass
                             }).ToList();

            return data;
        }

        public MenuDataModel AddMenu(MenuDataModel dataModel)
        {
            Menu model = new Menu
            {
                ParentMenuId = dataModel.ParentMenuId,
                Title = dataModel.Title,
                SpecifiedUrl = dataModel.SpecifiedUrl,
                IconClass = dataModel.IconClass
            };

            this.menuRepository.Add(model);
            this.UnitOfWork.Commit();
            dataModel.Id = model.Id;
            return dataModel;
        }

        public MenuDataModel UpdateMenu(long id,MenuDataModel dataModel)
        {
            try
            {
                var model = this.menuRepository.GetItemByKey(id);
                model.ParentMenuId = dataModel.ParentMenuId;
                model.Title = dataModel.Title;

                model.SpecifiedUrl = dataModel.SpecifiedUrl;
                model.IconClass = dataModel.IconClass;
                this.menuRepository.Update(model);
                this.UnitOfWork.Commit();
                return dataModel;

            }
            catch (Exception ex)
            {

                // return null;
            }

            return null;
        }
        public long RemoveMenu(long id)
        {
            try
            {
                var model = this.menuRepository.GetItemByKey(id);
                this.menuRepository.Remove(model);
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
