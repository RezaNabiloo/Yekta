namespace BSG.ADInventory.Services
{
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Entities;    
    using BSG.ADInventory.Models.UserShortcut;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Zcf.Common;
    using Zcf.Data;
    using Zcf.Data.Extensions;
    using Zcf.Paging;
    using Zcf.Services;
	
		  
    /// <summary>
    /// Create Service For UserShortcut" />
    /// </summary>
    public class UserShortcutService : CrudService<UserShortcut>, IUserShortcutService
    {
        private readonly IUserShortcutRepository userShortcutRepository;
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserShortcutService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="UserShortcutRepository"></param>
        public UserShortcutService(IUnitOfWork unitOfWork, IUserShortcutRepository userShortcutRepository,
            IUserRepository userRepository)
            : base(unitOfWork, userShortcutRepository)
        {
            this.userShortcutRepository = userShortcutRepository;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Gets an <see cref="IPagedQueryable" /> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPagedQueryable<UserShortcut> GetItems(PagedQueryable criteria)
        {
            var data = this.userShortcutRepository.Query;
            return data.ToPagedQueryable(criteria);
        }


        public bool ExistShortcut(Guid userId, long id)
        {
            if (this.userShortcutRepository.Query.Where(s => s.UserId == userId && s.MenuId == id).Any())
            {
                return true;
            }

            else { return false; }
        }

        public List<UserShortcutListModel> GetUserShortcuts() {
            var user = this.userRepository.GetCurrentUser();

            var data = this.userShortcutRepository.Query.Where(u => u.UserId == user.Id).
                Select(s => new UserShortcutListModel
                {
                    Id=s.Id,
                    MenuId = s.MenuId,
                    ParentMenuId = s.Menu.ParentMenuId,
                    Title = s.Menu.Title,
                    ParentTitle = s.Menu.ParentMenu.Title,
                    IconClass = s.Menu.IconClass,
                    ParentIconClass = s.Menu.ParentMenu.IconClass,
                    UrlLink = s.Menu.SpecifiedUrl
                }).ToList();

            return data;
            }
    }
}
