using BSG.ADInventory.Resources;
using BSG.ADInventory.Services;
using BSG.ADInventory.Entities;
using System;
using System.Web.Mvc;
using WebMarkupMin.AspNet4.Mvc;
using Zcf.Data;
using Zcf.Models;
using Zcf.Paging;
using Zcf.Web.Mvc.Controllers;
using Zcf.Web.Mvc.Security;

namespace BSG.ADInventory.WebUI.Controllers
{
    [MinifyHtml]
    [Authorize]        
    public class UserShortcutController : CrudController<UserShortcut>
    {
        #region ctor
        private IUnitOfWork unitOfWork;
        private readonly IUserShortcutService userShortcutService;
        private readonly IUserService userService;


        public UserShortcutController(IUnitOfWork unitOfWork, IUserShortcutService userShortcutService,
            IUserService userService)
            : base(userShortcutService)
        {
            this.unitOfWork = unitOfWork;
            this.userShortcutService = userShortcutService;
            this.userService = userService;
        }

        [NonAction]
        public override ActionResult Index(PagedQueryable criteria)
        {
            return base.Index(criteria);
        }
        #endregion


        [HttpPost]
        public ActionResult AddShortcut(long id)
        {
            CustomResult<bool> data = new CustomResult<bool>();
            var user = this.userService.GetCurrentUser();
            var exists = userShortcutService.ExistShortcut(user.Id, id);
            if (exists)
            {
                data.Result = false;
                data.ErrorMessage = "میانبر مورد نظر قبلا افزوده شده.";
            }
            else
            {
                try
                {
                    UserShortcut sh = new UserShortcut { UserId = user.Id, MenuId = id };
                    this.userShortcutService.Add(sh);
                    data.Result = true;
                    data.ErrorMessage = Resource.DataAddSuccessfully;
                }
                catch (Exception)
                {
                    data.Result = false;
                    data.ErrorMessage = Resource.ErrorCallAdmin;
                    throw;
                }
            }

            return Json(data);
        }

        [HttpPost]
        public ActionResult RemoveShortcut(long id)
        {
            CustomResult<bool> data = new CustomResult<bool>();
            var shortcut = this.userShortcutService.GetItemByKey(id);            
            if (shortcut!=null)
            {
                this.userShortcutService.Remove(shortcut);
                data.Result = true;
                data.ErrorMessage = "میانبر مورد نظر حذف شد.";
            }
            else
            {
                data.Result = false;
                data.ErrorMessage = Resource.ItemNotFound;
            }

            return Json(data);
        }

        [HttpPost]
        public ActionResult GetUserShortcuts()
        {
            var data = this.userShortcutService.GetUserShortcuts();
            return Json(data);
        }


    }
}