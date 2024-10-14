namespace BSG.ADInventory.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BSG.ADInventory.Entities;
    using Zcf.Services;
    using Models.User;
    using Models.Account;
    using BSG.ADInventory.Models.Menu;

    /// <summary>
    /// 
    /// </summary>
    public interface IUserService : ICrudService<BSG.ADInventory.Models.User.UserModel, BSG.ADInventory.Models.User.Criteria>
    {
        /// <summary>
        /// Gets the current user.  
        /// </summary>
        /// <returns></returns>
        User GetCurrentUser();

        bool ChangePass_Force(string userName, string password);
       
        /// <summary>
        /// Users the name exists.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="excludedUserId">The excluded user id.</param>
        /// <returns></returns>
        bool UserNameExists(string userName, Guid? excludedUserId = null);

        /// <summary>
        /// Gets the user model for create.
        /// </summary>
        /// <returns></returns>
        UserModel GetUserModelForCreate();
        UserMenuModel GetUserMenuModelForCreate();

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="password">The password.</param>
        void Update(BSG.ADInventory.Models.User.UserModel model, string password);

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="newPassword">The new password.</param>
        void ChangePassword(Guid userId, string newPassword);

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="isActive">if set to <c>true</c> is active.</param>
        /// <returns></returns>
        IEnumerable<User> GetUsers(bool isActive);

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="top">The top.</param>
        /// <returns></returns>
        IEnumerable<User> GetUsers(string search, int top = 10);

        /// <summary>
        /// Gets the user by key.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        UserViewModel GetUserByKey(Guid id);


        /// <summary>
        /// Gets the user by key.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        User GetUserByEmail(string email);

        /// <summary>
        /// Gets the user by username or phone number.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        User GetUserByUserName(string username);

        string Login(string username, string password);

        bool CheckToken();

        User GetTokenUser();
        UserViewModel Login(SimpleLoginModel model);

        List<UserViewModel> GetUserAll();

        UserViewModel AddUser(UserDataModel userModel);
        void AddUserMenuModel(UserMenuModel userModel);
        bool CheckUserNameDuplicate(string roleName, Guid? excludedRoleId = null);
        UserViewModel UpdateUser(UserDataModel model);

        void UpdateUser(UserModel model);

        void UpdateUserMenu(UserMenuModel model);

        UserInfoViewModel GetUserInfo(Guid id);

        UserModel GetUserById(Guid id);

        UserMenuModel GetUserMenuById(Guid id);

        List<MenuModel> UserAccessMenu(Guid id);

        List<RoleCheckBoxItem> UpdateUserRole(UpdateUserRoleModel dataModel);

        string ChangeProfilePassword(Guid userId, ChangePasswordModel dataModel);

        //  List<MenuModel> GetUserAccess();

        List<UserInfoSimpleViewModel> DoneUsers();

        UserRoleModel GetUserRoleById(Guid id);

        void UpdateUserRole(UserRoleModel model);
        UserRoleModel GetUserRoleModelForCreate();

        void AddUserRoleModel(UserRoleModel userModel);

        IEnumerable<UserListModel> GetDataList();

        List<string> GetUserPermissions();
    }

    
}