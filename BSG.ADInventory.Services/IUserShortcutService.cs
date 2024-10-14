namespace BSG.ADInventory.Services
{ 	
    using System;
    using System.Collections.Generic;
    using BSG.ADInventory.Entities;
    using BSG.ADInventory.Models.UserShortcut;
    using Zcf.Services;	
		
    /// <summary>
    /// Create Interface for UserShortcutService
    /// </summary>
	public interface IUserShortcutService : ICrudService<UserShortcut>
	{
        bool ExistShortcut(Guid userId, long id);

        List<UserShortcutListModel> GetUserShortcuts();
    }
}