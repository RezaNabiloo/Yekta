namespace BSG.ADInventory.Services
{ 	
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.Menu;
    using Zcf.Services;	
		
    /// <summary>
    /// Create Interface for MenuService
    /// </summary>
	public interface IMenuService : ICrudService<Menu>
	{
        List<MenuDataModel> GetMenuById(long id);
        MenuDataModel AddMenu(MenuDataModel dataModel);
        MenuDataModel UpdateMenu(long id, MenuDataModel dataModel);
        long RemoveMenu(long id);
    }
}