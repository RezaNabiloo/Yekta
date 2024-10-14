namespace BSG.ADInventory.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Entities;
    using Zcf.Services;
    using Models.Menu;

    /// <summary>
    /// Create Interface for MenuService
    /// </summary>
    public interface IMenuService : ICrudService<Menu>
    {
        MenuModel AddMenu(MenuModel dataModel);

        void UpdateMenu(MenuModel dataModel);

        long RemoveMenu(long id);

        MenuModel GetMenuModelForCreate();
        MenuModel GetMenuModelForEdit(long id);

        List<MenuListModel> UserAccessMenu(Guid id);


        
        
    }
}
