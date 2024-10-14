
namespace BSG.ADInventory.WebUI.Controllers
{
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Models.Account;
    using BSG.ADInventory.Models.Message;
    using BSG.ADInventory.Models.User;
    using BSG.ADInventory.Services;
    using log4net;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Web.Mvc;
    using WebMarkupMin.AspNet4.Mvc;
    using Zcf.Data;
    using Zcf.Security;

    [MinifyHtml]
    public class AccountController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Services.IUserService userService;

        private readonly Data.ISqlCommandRepository sqlCommandRepository;
        private readonly ILog logger;

        public AccountController(
            IUserService userService,
            IUnitOfWork unitOfWork,
            ILog logger,
            ISqlCommandRepository sqlCommandRepository)
        {
            this.userService = userService;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.sqlCommandRepository = sqlCommandRepository;
        }

        /// <summary>
        ///   GET: /Account/Login
        /// </summary>
        /// <returns> </returns>
        [HttpGet]

        public ActionResult Login()
        {
            return this.View();
        }

        /// <summary>
        ///   POST: /Account/Login
        /// </summary>
        /// <param name="model"> The model. </param>
        /// <returns> </returns>
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            try
            {
                var isLoggedIn = Authentication.Default.Authenticate(model.LoginUserName, model.LoginPassword, model.RememberMe);
               // var isLoggedIn = Authentication.Default.AuthenticateByNameOrEmail(model.LoginUserName, model.LoginPassword, model.RememberMe);
                if (!isLoggedIn)
                {
                    this.ModelState.AddModelError("LoginUserName", Resource.InvalidUserNameOrPassword);
                    return this.View("Login");
                }

                if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                {
                    return this.Redirect(model.ReturnUrl);
                }
                var user = userService.GetCurrentUser();


                

                Session["UserId"] = user.Id;
                Session["PeopleId"] = user.PeopleId.Value;
                Session["FirstName"] = user.People.FirstName;
                Session["LastName"] = user.People.LastName;                
                
                return this.RedirectToAction("Index","Home");
            }
            catch (Exception exception)
            {
                return this.PartialView("Error", new MessageModel(Resources.Resource.Error, exception.Message));
            }
        }

        /// <summary>
        /// Logins the specified mobile.
        /// </summary>
        /// <param name="mobile">The mobile.</param>
        /// <returns></returns>
        public JsonResult SignIn(string mobile)
        {
            this.logger.Info("ResultData=" + "True");
            return this.Json("True", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// GET: /Account/Logout
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Authentication.Default.Logout();
            return this.RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// Sends the SMS.
        /// </summary>
        /// <param name="mobile">The mobile.</param>
        /// <returns></returns>
        public ActionResult SendSms(string mobile)
        {
            this.logger.Info("Param=" + mobile);         
            return this.Json("", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///   GET: /Account/ChangePassword
        /// </summary>
        /// <returns> </returns>
        public ActionResult ChangePassword()
        {
            return this.View();
        }

        /// <summary>
        ///   POST: /Account/ChangePassword
        /// </summary>
        /// <param name="model"> The model. </param>
        /// <returns> </returns>
        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            var currentUser = Authentication.Default.GetCurrentUserPrincipal();
            if (string.IsNullOrWhiteSpace(model.OldPassword))
            {
                this.ModelState.AddModelError("OldPassword", Resource.OldAndNewPasswordRequiredMessage);
                return this.View("ChangePasswordData", model);
            }

            if (model.NewPassword == null || model.ConfirmPassword == null || model.NewPassword != model.ConfirmPassword)
            {
                this.ModelState.AddModelError("NewPassword", Resource.PasswordsDoesNotMatch);
                this.ModelState.AddModelError("ConfirmPassword", Resource.PasswordsDoesNotMatch);
                return this.View("ChangePasswordData", model);
            }

            if (!string.IsNullOrWhiteSpace(model.OldPassword) && !string.IsNullOrWhiteSpace(model.NewPassword))
            {
                // Check if the user is correct
                if (!Authentication.Default.Authenticate(currentUser.Identity.Name, model.OldPassword))
                {
                    this.ModelState.AddModelError("OldPassword", Resource.InvalidPassword);
                    return this.View("ChangePasswordData",model);
                }

                this.userService.ChangePassword(currentUser.Identity.Id, model.NewPassword);
            }
            ViewBag.Result = true;
            this.ViewBag.ErrorMessage= Resource.ChangedPasswordMessage;

            return this.View("ChangePasswordData");
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteUser()
        {
            var user = this.userService.GetCurrentUser();
            return this.View();
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="deleteUserViewModel">The delete user view model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteUser(DeleteUserViewModel deleteUserViewModel)
        {
            var mobile = new SqlParameter();
            mobile.ParameterName = "@mobile";
            mobile.Direction = ParameterDirection.Input;
            mobile.Value = deleteUserViewModel.Mobile;
            mobile.DbType = DbType.String;

            List<Entities.SqlCommand> data =
                this.sqlCommandRepository.ExecuteStoredProcedureList<Entities.SqlCommand>("DeleteUserByMobile  @mobile", mobile).ToList();

            var result = data.FirstOrDefault();
            if (result != null && result.ReturnValue == 1)
            {
                return this.PartialView("Error", new MessageModel(Resources.Resource.Error, Resources.Resource.SuccessfullOperate));
            }

            return this.PartialView("Error", new MessageModel(Resources.Resource.Error, Resources.Resource.NotSuccessfullOperate));
        }

        [HttpPost]
        public ActionResult GetCurrentUserInfo() 
        {
            var user = this.userService.GetCurrentUser();

            UserInfoDataModel data = new UserInfoDataModel
            {
                Id = user.Id,
                FirstName = user.People.FirstName,
                LastName = user.People.LastName,
                Email = user.Email.ToString(),                
                ProfileImage = (user.People.ImageUrl ?? "") == "" ? "/Uploads/noimageuser.jpg" : user.People.ImageUrl
            };
            
            return Json(data);
        }


    }
}