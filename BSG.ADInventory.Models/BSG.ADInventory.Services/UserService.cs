namespace BSG.ADInventory.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using Zcf.Data;
    using Zcf.Security;
    using Zcf.Paging;
    using Zcf.Data.Extensions;
    using BSG.ADInventory.Entities;
    using System.Text;
    using BSG.ADInventory.Models.User;
    using BSG.ADInventory.Models.Account;
    using Zcf.Data.CustomFiltering;
    using System.Web;
    using BSG.ADInventory.Models.Menu;
    using System.Data;
    using BSG.ADInventory.Resources;
    using BSG.ADInventory.Data;
    using System.Reflection;
    using System.Diagnostics;

    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Data.IUserRepository userRepository;
        private readonly Data.IRoleRepository roleRepository;
        private readonly Data.ISqlCommandRepository sqlCommandRepository;
        private readonly Data.IMenuRepository menuRepository;
        private readonly Data.IUserMenuRepository userMenuRepository;
        private readonly Data.IProjectRepository projectRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="roleRepository">The role repository.</param>
        public UserService(
            IUnitOfWork unitOfWork,
            Data.IUserRepository userRepository,
            Data.IRoleRepository roleRepository,
            Data.ISqlCommandRepository sqlCommandRepository,
            Data.IMenuRepository menuRepository, IUserMenuRepository userMenuRepository,
            IProjectRepository projectRepository)
        {
            this.unitOfWork = unitOfWork;
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.sqlCommandRepository = sqlCommandRepository;
            this.menuRepository = menuRepository;
            this.userMenuRepository = userMenuRepository;
            this.projectRepository = projectRepository;
        }

        /// <summary>
        /// Adds the specified user model.
        /// </summary>
        /// <param name="userModel">The user model.</param>
        public void Add(UserModel userModel)
        {
            var userEntity = new BSG.ADInventory.Entities.User
            {
                // FirstName = userModel.FirstName,
                //LastName = userModel.LastName,
                IsActive = userModel.IsActive,
                UserName = userModel.UserName,
                Password = userModel.Password,
                //Mobile = userModel.Mobile,

                Token = Guid.NewGuid().ToString(),// userModel.Token,
                TokenExpireTime = DateTime.Now.AddMonths(12),
                Email = userModel.Email,

            };

            userEntity.SetHashedPassword(userModel.Password);

            foreach (var role in userModel.Roles.Where(p => p.IsSelected))
            {
                userEntity.Roles.Add(this.roleRepository.GetItemByKey(role.Id));
            }

            this.userRepository.Add(userEntity);
            this.unitOfWork.Commit();
        }

        public bool ChangePass_Force(string userName, string password)
        {
            try
            {
                var user = this.userRepository.GetAllItems().Where(u => u.UserName.ToLower() == userName.ToLower()).FirstOrDefault();
                user.SetHashedPassword(password);
                // this.userRepository.Update(user);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                var reza = ex.Message;
                //throw;
                return false;
            }
        }

        /// <summary>
        /// Removes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void Remove(IEnumerable<UserModel> entities)
        {
            try
            {
                this.userRepository.Remove(entities);
                this.unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException.InnerException as SqlException;
                if (innerException != null && innerException.Number == 547)
                {
                    this.unitOfWork.RollBack();
                }

                throw;
            }
        }

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Remove(UserModel entity)
        {
            try
            {
                this.userRepository.Remove(r => r.Id == entity.Id);
                this.unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException.InnerException as SqlException;
                if (innerException != null && innerException.Number == 547)
                {
                    this.unitOfWork.RollBack();
                }

                throw;
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="password">The password.</param>
        public void Update(UserModel model, string password)
        {
            User entity = this.userRepository.GetItemByKey(model.Id);
            if (entity != null)
            {
                if (!string.IsNullOrWhiteSpace(password))
                {
                    entity.SetHashedPassword(password);
                }

                //entity.FirstName = model.FirstName;
                //entity.LastName = model.LastName;
                entity.IsActive = model.IsActive;
                //entity.Mobile = model.Mobile;
                entity.Email = model.Email;
                entity.Roles.Clear();

                foreach (var role in model.Roles.Where(r => r.IsSelected))
                {
                    entity.Roles.Add(this.roleRepository.GetItemByKey(role.Id));
                }

                this.userRepository.Update(entity);
                this.unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(UserModel model)
        {
            var entity = this.userRepository.GetItemByKey(model.Id);
            if (entity == null)
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                entity.SetHashedPassword(model.Password);
            }

            //entity.FirstName = model.FirstName;
            //entity.LastName = model.LastName;
            entity.IsActive = model.IsActive;
            //entity.Mobile = model.Mobile;
            entity.Email = model.Email;
            entity.Roles.Clear();

            foreach (var role in model.Roles.Where(r => r.IsSelected))
            {
                entity.Roles.Add(this.roleRepository.GetItemByKey(role.Id));
            }

            this.userRepository.Update(entity);
            this.unitOfWork.Commit();
        }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserModel> GetAllItems()
        {
            return this.userRepository.GetAllItems()
                .Select(r => this.ConvertToUserModel(r));
        }

        /// <summary>
        /// Gets the item count.
        /// </summary>
        /// <returns></returns>
        public int GetItemCount()
        {
            return this.userRepository.GetItemCount();
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        public IPagedQueryable<UserModel> GetItems(BSG.ADInventory.Models.User.Criteria criteria)
        {
            ////var query = this.GetQuery(criteria);
            ////var paginatedEntities = query.ToPagedEnumerable(criteria);

            ////var paginatedModels = new PagedEnumerable<UserModel>(
            ////    paginatedEntities.Data.Select(u => this.ConvertToUserModel(u)));

            ////return paginatedModels;

            var query = this.GetQuery(criteria);
            var paginatedEntities = query.ToPagedEnumerable(criteria);

            var paginatedModels = new PagedQueryable<BSG.ADInventory.Models.User.UserModel>(
                paginatedEntities.Data.Select(u => this.ConvertToUserModel(u)).ToList().AsQueryable(),
                0,
                paginatedEntities.TotalRowcount);

            return paginatedModels;
        }

        public IPagedQueryable<UserModel> GetItems(int? page, int? count, DataServiceFilter filters, IList<DataServiceSort> sort)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="itemCount">The item count.</param>
        /// <param name="sortDescriptions">The sort descriptions.</param>
        /// <returns></returns>
        //public IEnumerable<UserModel> GetItems(int startIndex, int itemCount, SortDescription[] sortDescriptions = null)
        //{
        //    return this.userRepository.Query
        //        .ToPage(startIndex, itemCount, sortDescriptions)
        //        .AsEnumerable()
        //        .Select(r => this.ConvertToUserModel(r)).ToList();
        //}

        /// <summary>
        /// Gets the item by key.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <returns></returns>
        public UserModel GetItemByKey(params object[] keys)
        {
            var user = this.userRepository.GetItemByKey(keys);
            if (user == null)
            {
                return null;
            }

            return this.ConvertToUserModel(user);
        }

        /// <summary>
        /// Reloads the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public UserModel Reload(UserModel entity)
        {
            return this.ConvertToUserModel(this.userRepository.GetItemByKey(entity.Id), entity);
        }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <returns></returns>
        public User GetCurrentUser()
        {
            return this.userRepository.GetCurrentUser();
        }

        /// <summary>
        /// Determines whether the specified user name exists.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="excludedUserId">The excluded user id.</param>
        /// <returns></returns>
        public bool UserNameExists(string userName, Guid? excludedUserId = null)
        {
            return this.userRepository.UserNameExists(userName, excludedUserId);
        }

        /// <summary>
        /// Creates a user model for create action.
        /// </summary>
        /// <returns></returns>
        public UserModel GetUserModelForCreate()
        {
            var userModel = new UserModel();

            var roles = this.roleRepository.GetAllItems();
            foreach (var role in roles)
            {
                userModel.Roles.Add(
                    new RoleCheckBoxItem
                    {
                        Id = role.Id,
                        IsSelected = false,
                        Value = role.Name
                    });
            }

            return userModel;
        }
        public UserMenuModel GetUserMenuModelForCreate()
        {
            var userModel = new UserMenuModel();

            var menus = this.menuRepository.GetAllItems().OrderBy(m => m.ViewOrder);
            foreach (var menu in menus.Where(m1 => m1.ParentMenuId == null))
            {
                userModel.Menus.Add(
                    new BSG.ADInventory.Models.Menu.MenuCheckBoxItem
                    {
                        MenuId = menu.Id,
                        ParentMenuId = menu.ParentMenuId,
                        IsSelected = false,
                        Title = menu.Title
                    });

                foreach (var subMenu in menu.ChildMenus.OrderBy(m => m.ViewOrder))
                {
                    userModel.Menus.Add(
                    new BSG.ADInventory.Models.Menu.MenuCheckBoxItem
                    {
                        MenuId = subMenu.Id,
                        ParentMenuId = subMenu.ParentMenuId,
                        IsSelected = false,
                        Title = subMenu.Title
                    });
                }
            }

            return userModel;
        }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="newPassword">The new password.</param>
        public void ChangePassword(Guid userId, string newPassword)
        {
            var fetchedUser = this.userRepository.GetUser(userId);
            fetchedUser.SetHashedPassword(newPassword);
            this.unitOfWork.Commit();
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="isActive">if set to <c>true</c> is active.</param>
        /// <returns></returns>
        public IEnumerable<User> GetUsers(bool isActive)
        {
            return this.userRepository.GetUsers(isActive);
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="top">The top.</param>
        /// <returns></returns>
        public IEnumerable<User> GetUsers(string search, int top = 10)
        {
            return this.userRepository.Query.Where(c => c.UserName.Contains(search)).Take(top);
        }

        /// <summary>
        /// Gets the user by key.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public UserViewModel GetUserByKey(Guid id)
        {
            var user = this.userRepository.GetItemByKey(id);
            if (user == null)
            {
                return null;
            }
            UserViewModel data = new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                IsActive = user.IsActive,                
                DepartmentInfo = "",
                FirstName = (user.UserName.ToLower() == "admin" ? "مدیر" : ((user.People == null) ? "" : user.People.FirstName)),
                LastName = (user.UserName.ToLower() == "admin" ? "سیستم" : ((user.People == null) ? "" : user.People.LastName)),
                Token = user.Token
            };

            return data;
        }

        /// <summary>
        /// Converts the user entity to user model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        private UserModel ConvertToUserModel(User entity, UserModel model = null)
        {
            if (model == null)
            {
                model = new UserModel();
            }

            // model.FirstName = "کاربر";// entity.FirstName;
            //   model.LastName = "آزمایشی";// entity.LastName;
            // model.Code = entity.Code;
            model.Id = entity.Id;
            model.IsActive = entity.IsActive;
            model.UserName = entity.UserName;
            //   model.Type = entity.Type;
            //  model.Mobile = entity.Phone;
            model.People = entity.People;
            model.Email = entity.Email;
            model.CreateTime = entity.CreateTime;
            model.LastActivityTime = entity.LastActivityTime;
            model.LastLoginTime = entity.LastLoginTime;
            model.LastUpdateTime = entity.LastUpdateTime;

            var roles = this.roleRepository.GetAllItems();
            foreach (var role in roles)
            {
                model.Roles.Add(
                    new BSG.ADInventory.Models.User.RoleCheckBoxItem
                    {
                        Id = role.Id,
                        IsSelected = entity.Roles.Any(r => r.Id == role.Id),
                        Value = role.Name
                    });
            }

            return model;
        }

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        private IQueryable<User> GetQuery(BSG.ADInventory.Models.User.Criteria criteria)
        {
            var query = this.userRepository.Query;
            return query;
        }

        public User GetUserByEmail(string email)
        {
            return this.userRepository.GetUserByEmail(email);
        }
        public User GetUserByUserName(string username)
        {
            var data = this.userRepository.AllItems().Where(u => u.UserName == username).FirstOrDefault();
            return data;
        }

        public string Login(string username, string password)
        {
            var data = string.Empty;
            var pss = PasswordHasherFactory.Default.Hash(password);

            var user = this.userRepository.GetAllItems().Where(u => u.UserName == username).FirstOrDefault();
            //var user = this.userRepository.GetAllItems().Where(u => u.UserName == username && u.Password== PasswordHasherFactory.Default.Hash(password)).FirstOrDefault();

            if (user != null && user.Password == pss)
            {
                user.Token = Guid.NewGuid().ToString();
                user.TokenExpireTime = DateTime.Now.AddMinutes(10);
                this.userRepository.Update(user);
                this.unitOfWork.Commit();
                data = user.Token;
            }
            return data;
        }

        public bool CheckToken()
        {
            var token = HttpContext.Current.Request.Headers["token"];
            var user = this.userRepository.GetAllItems().Where(u => u.Token == token && u.TokenExpireTime >= DateTime.Now).FirstOrDefault();


            if (user != null)
            {
                user.TokenExpireTime = DateTime.Now.AddDays(100);
                this.userRepository.Update(user);
                this.unitOfWork.Commit();
                return true;
            }
            return false;
        }

        public User GetTokenUser()
        {
            var token = HttpContext.Current.Request.Headers["token"];
            return this.userRepository.GetAllItems().Where(u => u.Token == token && u.TokenExpireTime >= DateTime.Now).FirstOrDefault();
        }

        public UserViewModel Login(SimpleLoginModel model)
        {
            var user = this.userRepository.GetAllItems().Where(u => u.UserName.ToLower() == model.UserName.ToLower() && u.Password == PasswordHasherFactory.Default.Hash(model.Password) && u.IsActive == true).FirstOrDefault();
            if (user != null)
            {
                user.Token = Guid.NewGuid().ToString();
                user.TokenExpireTime = DateTime.Now.AddMinutes(10);
                this.userRepository.Update(user);
                this.unitOfWork.Commit();
                UserViewModel data = new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = (model.UserName.ToLower() == "admin" ? "مدیر" : ((user.People == null) ? "" : user.People.FirstName)),
                    LastName = (model.UserName.ToLower() == "admin" ? "سیستم" : ((user.People == null) ? "" : user.People.LastName)),
                    Token = user.Token,
                    IsActive = user.IsActive,
                    ImageUrl = (user.PeopleId == null ? "/media/users/noimage.jpg" : (user.People.ImageUrl == null ? "/media/users/noimage.jpg" : user.People.ImageUrl)),
                    Menus = this.UserAccessMenu(user.Id),
                    Roles = this.GetUserRoles(user)
                };

                return data;
            }
            return null;
        }

        public List<UserViewModel> GetUserAll()
        {
            var data = (from u in this.userRepository.GetAllItems()
                        select new UserViewModel
                        {
                            Id = u.Id,
                            UserName = u.UserName,
                            IsActive = u.IsActive,
                            //ContractorInfo = u.People == null ? "" : u.People.Contractor.ContractorName,
                            FirstName = (u.UserName.ToLower() == "admin" ? "مدیر" : ((u.People == null) ? "" : u.People.FirstName)),
                            LastName = (u.UserName.ToLower() == "admin" ? "سیستم" : ((u.People == null) ? "" : u.People.LastName)),
                            Roles = this.GetUserRoles(u)
                        }
                        ).ToList();
            return data;
        }

        public UserViewModel AddUser(UserDataModel userModel)
        {
            var userEntity = new User
            {
                IsActive = userModel.IsActive,
                UserName = userModel.UserName,
                Password = userModel.Password,
                PeopleId = userModel.PeopleId,

                Token = Guid.NewGuid().ToString(),// userModel.Token,
                TokenExpireTime = DateTime.Now.AddMonths(12),
                Email = userModel.Email,

            };

            userEntity.SetHashedPassword(userModel.Password);

            foreach (var role in userModel.Roles.Where(p => p.IsSelected))
            {
                userEntity.Roles.Add(this.roleRepository.GetItemByKey(role.Id));
            }

            this.userRepository.Add(userEntity);
            this.unitOfWork.Commit();

            userModel.Id = userEntity.Id;
            return GetUserByKey(userModel.Id);
        }

        public void AddUserMenuModel(UserMenuModel userModel)
        {
            var userEntity = new User
            {
                IsActive = userModel.IsActive,
                UserName = userModel.UserName,
                Password = userModel.Password,
                PeopleId = userModel.PeopleId,
                MonitoringPersonel=userModel.MonitoringPersonel,


        Token = Guid.NewGuid().ToString(),// userModel.Token,
                TokenExpireTime = DateTime.Now.AddMonths(12),
                Email = userModel.Email,

            };

            userEntity.SetHashedPassword(userModel.Password);

            this.userRepository.Add(userEntity);
            this.unitOfWork.Commit();
            foreach (var menu in userModel.Menus.Where(p => p.IsSelected))
            {
                UserMenu d = new UserMenu { UserId = userEntity.Id, MenuId = menu.MenuId };
                this.userMenuRepository.Add(d);
                this.unitOfWork.Commit();
            }
            userModel.Id = userEntity.Id;

        }

        public bool CheckUserNameDuplicate(string userName, Guid? excludedUserId = null)
        {
            var data = this.userRepository.GetAllItems().Where(u => u.UserName.ToLower() == userName.ToLower() && (u.Id != excludedUserId)).FirstOrDefault();

            if (data != null)
            {
                return true;
            }
            else
                return false;
        }


        public UserViewModel UpdateUser(UserDataModel model)
        {
            User entity = this.userRepository.GetItemByKey(model.Id);
            if (entity != null)
            {
                if (!string.IsNullOrWhiteSpace(model.Password))
                {
                    entity.SetHashedPassword(model.Password);
                }
                entity.IsActive = model.IsActive;
                entity.Email = model.Email;
                entity.PeopleId = model.PeopleId;
                

                entity.Roles.Clear();

                foreach (var role in model.Roles.Where(r => r.IsSelected))
                {
                    entity.Roles.Add(this.roleRepository.GetItemByKey(role.Id));
                }

                this.userRepository.Update(entity);
                this.unitOfWork.Commit();
                return this.GetUserByKey(model.Id);
            }
            else return null;
        }

        public void UpdateUser(UserModel model)
        {
            User entity = this.userRepository.GetItemByKey(model.Id);
            if (entity != null)
            {
                if (!string.IsNullOrWhiteSpace(model.Password))
                {
                    entity.SetHashedPassword(model.Password);
                }
                entity.IsActive = model.IsActive;
                entity.Email = model.Email;
                entity.PeopleId = model.PeopleId;

                entity.Roles.Clear();

                foreach (var role in model.Roles.Where(r => r.IsSelected))
                {
                    entity.Roles.Add(this.roleRepository.GetItemByKey(role.Id));
                }

                this.userRepository.Update(entity);
                this.unitOfWork.Commit();

            }

        }

        public void UpdateUserMenu(UserMenuModel model)
        {
            User entity = this.userRepository.GetItemByKey(model.Id);
            if (entity != null)
            {
                if (!string.IsNullOrWhiteSpace(model.Password))
                {
                    entity.SetHashedPassword(model.Password);
                }
                entity.IsActive = model.IsActive;
                entity.Email = model.Email;
                entity.PeopleId = model.PeopleId;
                entity.UserName = model.UserName;
                entity.MonitoringPersonel = model.MonitoringPersonel;
                var mm = entity.UserMenus.ToList();

                //foreach (var menu in model.Menus.Where(p => p.IsSelected))
                //{
                //    UserMenu d = new UserMenu { UserId = entity.Id, MenuId = menu.MenuId };
                //    entity.UserMenus.Add(d);

                //}
                this.userRepository.Update(entity);
                try {
                    this.unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    //var reza = ex;
                    //throw;
                }

                foreach (var item in mm)
                {
                    var selitem = this.userMenuRepository.GetItemByKey(item.Id);
                    if (selitem != null)
                    {
                        this.userMenuRepository.Remove(selitem);
                        this.unitOfWork.Commit();
                    }
                }


                foreach (var menu in model.Menus.Where(p => p.IsSelected))
                {
                    UserMenu d = new UserMenu { UserId = entity.Id, MenuId = menu.MenuId };
                    this.userMenuRepository.Add(d);
                    this.unitOfWork.Commit();
                }




            }

        }
        public UserInfoViewModel GetUserInfo(Guid id)
        {
            var user = this.userRepository.GetItemByKey(id);
            if (user == null)
            {
                return null;
            }
            UserInfoViewModel data = new UserInfoViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                IsActive = user.IsActive,
                PeopleId = user.PeopleId,
                CreateTime = user.CreateTime,
                LastUpdateTime = user.LastUpdateTime,
                LastLoginTime = user.LastLoginTime,
                LastActivityTime = user.LastActivityTime
            };

            var roles = this.roleRepository.GetAllItems();
            foreach (var role in roles)
            {
                data.Roles.Add(
                    new RoleCheckBoxItem
                    {
                        Id = role.Id,
                        IsSelected = false,
                        Value = role.Name
                    });
            }

            return data;
        }

        public UserModel GetUserById(Guid id)
        {
            var user = this.userRepository.GetItemByKey(id);
            if (user == null)
            {
                return null;
            }
            UserModel data = new UserModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                IsActive = user.IsActive,
                Token = user.Token,
                PeopleId = user.PeopleId
            };

            var roles = this.roleRepository.GetAllItems();
            foreach (var role in roles)
            {
                data.Roles.Add(
                    new BSG.ADInventory.Models.User.RoleCheckBoxItem
                    {
                        Id = role.Id,
                        IsSelected = user.Roles.Any(r => r.Id == role.Id),
                        Value = role.Name
                    });
            }

            return data;

        }

        public UserMenuModel GetUserMenuById(Guid id)
        {
            var user = this.userRepository.GetItemByKey(id);
            if (user == null)
            {
                return null;
            }
            UserMenuModel data = new UserMenuModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                IsActive = user.IsActive,
                Token = user.Token,
                PeopleId = user.PeopleId,
                MonitoringPersonel=user.MonitoringPersonel
            };

            var menus = this.menuRepository.GetAllItems().OrderBy(m => m.ViewOrder);
            foreach (var menu in menus.Where(m1 => m1.ParentMenuId == null))
            {
                data.Menus.Add(
                    new BSG.ADInventory.Models.Menu.MenuCheckBoxItem
                    {
                        MenuId = menu.Id,
                        ParentMenuId = menu.ParentMenuId,
                        IsSelected = user.UserMenus.Any(r => r.MenuId == menu.Id),
                        Title = menu.Title
                    });

                foreach (var subMenu in menu.ChildMenus.OrderBy(m => m.ViewOrder))
                {
                    data.Menus.Add(
                    new BSG.ADInventory.Models.Menu.MenuCheckBoxItem
                    {
                        MenuId = subMenu.Id,
                        ParentMenuId = subMenu.ParentMenuId,
                        IsSelected = user.UserMenus.Any(r => r.MenuId == subMenu.Id),
                        Title = subMenu.Title
                    });
                }
            }

            return data;

        }

        public List<MenuModel> UserAccessMenu(Guid userId)
        {
            var UserId = new SqlParameter();
            UserId.ParameterName = "@UserId";
            UserId.Direction = ParameterDirection.Input;
            UserId.Value = userId.ToString();
            UserId.DbType = DbType.String;
            List<MenuModel> data = (from t in sqlCommandRepository.ExecuteStoredProcedureList<MenuModel>("Sp_GetUserAccessMenus @UserId", UserId)
                                    select new MenuModel
                                    {
                                        Id = t.Id,
                                        ParentMenuId = t.ParentMenuId,
                                        Title = t.Title,
                                        SpecifiedUrl = t.SpecifiedUrl,
                                        IconClass = t.IconClass,
                                        ViewOrder = t.ViewOrder
                                    }
                                          ).ToList();


            //var data = (from m in this.menuRepository
            //            join t in (
            //                ((from u in this.userRepository
            //                  join ur in this.userrol on new { User_Id = u.Id } equals new { User_Id = ur.User_Id } into ur_join
            //                  from ur in ur_join.DefaultIfEmpty()
            //                  join mr in db.MenusInRoles on new { RoleId = ur.Role_Id } equals new { RoleId = mr.RoleId } into mr_join
            //                  from mr in mr_join.DefaultIfEmpty()
            //                  where
            //        Convert.ToString(u.Id) == "123456"
            //                  select new
            //                  {
            //                      MenuId = mr.MenuId
            //                  }).Distinct())) on new { MenuId = m.Id } equals new { MenuId = Convert.Tolong(t.MenuId) } into t_join
            //            from t in t_join.DefaultIfEmpty()
            //            select new
            //            {
            //                m.Id,
            //                m.Title
            //            }).ToList();


            return data;
        }
        public List<RoleCheckBoxItem> UpdateUserRole(UpdateUserRoleModel dataModel)
        {
            var user = this.userRepository.GetItemByKey(dataModel.UserId);
            if (user == null)
            {
                return null;
            }

            user.Roles.Clear();
            foreach (var item in dataModel.Roles.Where(r => r.IsSelected == true))
            {
                user.Roles.Add(this.roleRepository.GetItemByKey(item.Id));
            }

            this.unitOfWork.Commit();
            return this.GetUserRoles(user);
        }

        private List<RoleCheckBoxItem> GetUserRoles(User user)
        {
            List<RoleCheckBoxItem> data = new List<RoleCheckBoxItem>();

            var roles = this.roleRepository.GetAllItems();
            foreach (var r in roles)
            {

                RoleCheckBoxItem role = new RoleCheckBoxItem();
                role.Id = r.Id;
                role.IsSelected = (user.IsInRole(r.Name) ? true : false);
                role.Value = r.Name;

                data.Add(role);
            };

            return data;
        }


        public string ChangeProfilePassword(Guid userId, ChangePasswordModel dataModel)
        {

            var user = this.userRepository.GetItemByKey(userId);

            if (string.IsNullOrWhiteSpace(dataModel.OldPassword))
            {
                return Resource.OldAndNewPasswordRequiredMessage;

            }

            if (dataModel.NewPassword == null || dataModel.ConfirmPassword == null || dataModel.NewPassword != dataModel.ConfirmPassword)
            {
                return Resource.PasswordsDoesNotMatch;
            }

            if (!string.IsNullOrWhiteSpace(dataModel.OldPassword) && !string.IsNullOrWhiteSpace(dataModel.NewPassword))
            {
                // Check if the user is correct
                if (!Authentication.Default.Authenticate(user.UserName, dataModel.OldPassword))
                {
                    return Resource.InvalidPassword;
                }

                this.ChangePassword(userId, dataModel.NewPassword);
            }

            return Resource.ChangedPasswordMessage;
        }

        public List<UserInfoSimpleViewModel> DoneUsers()
        {
            var data = (from u in this.userRepository.GetAllItems()
                        where u.UserName.ToLower()!="admin" && u.MonitoringPersonel==true
                        select new UserInfoSimpleViewModel
                        {
                            Id = u.Id,
                            UserName = u.UserName,
                            FullName = u.People == null ? u.UserName : u.People.FullName
                        }
                        ).ToList();
            return data;
        }


        public UserRoleModel GetUserRoleById(Guid id)
        {
            var user = this.userRepository.GetItemByKey(id);
            if (user == null)
            {
                return null;
            }
            UserRoleModel data = new UserRoleModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                IsActive = user.IsActive,                
                PeopleId = user.PeopleId.Value
            };

            var roles = this.roleRepository.GetAllItems();
            foreach (var role in roles)
            {
                data.Roles.Add(
                    new BSG.ADInventory.Models.User.RoleCheckBoxItem
                    {
                        Id=role.Id,
                        Value=role.Name,
                        IsSelected = (user.IsInRole(role.Name) ? true : false)
                    });               
            }

            var projects = this.projectRepository.GetAllItems();
            foreach (var p in projects)
            {
                data.Projects.Add(
                    new ListCheckBoxItem
                    {
                        Id = p.Id,
                        Value = p.Title,
                        IsSelected = (user.Projects.Where(a => a.Id == p.Id).FirstOrDefault() == null ? false : true)
                    });
            }

            return data;

        }

        public void UpdateUserRole(UserRoleModel model)
        {
            User entity = this.userRepository.GetItemByKey(model.Id);
            if (entity != null)
            {
                if (!string.IsNullOrWhiteSpace(model.Password))
                {
                    entity.SetHashedPassword(model.Password);
                }
                entity.IsActive = model.IsActive;
                entity.Email = model.Email;
                entity.PeopleId = model.PeopleId;
                entity.UserName = model.UserName;               

                entity.Roles.Clear();
                foreach (var role in model.Roles.Where(r => r.IsSelected))
                {
                    entity.Roles.Add(this.roleRepository.GetItemByKey(role.Id));
                }

                entity.Projects.Clear();
                foreach (var project in model.Projects.Where(r => r.IsSelected))
                {
                    entity.Projects.Add(this.projectRepository.GetItemByKey(project.Id));
                }

                this.userRepository.Update(entity);
                this.unitOfWork.Commit();

            }

        }

        public UserRoleModel GetUserRoleModelForCreate()
        {
            var data = new UserRoleModel();

            var roles = this.roleRepository.GetAllItems();
            foreach (var role in roles)
            {
                data.Roles.Add(
                    new BSG.ADInventory.Models.User.RoleCheckBoxItem
                    {
                        Id = role.Id,
                        Value = role.Name,
                        IsSelected =false
                    });
            }

            var projects = this.projectRepository.GetAllItems();
            foreach (var p in projects)
            {
                data.Projects.Add(
                    new BSG.ADInventory.Models.User.ListCheckBoxItem
                    {
                        Id = p.Id,
                        Value = p.Title,
                        IsSelected = false
                    });
            }

            return data;
        }

        public void AddUserRoleModel(UserRoleModel userModel)
        {
            var userEntity = new User
            {
                Id=Guid.Empty,            
                IsActive = userModel.IsActive,
                UserName = userModel.UserName,
                Password = userModel.Password,
                PeopleId = userModel.PeopleId,
                Token = Guid.NewGuid().ToString(),
                TokenExpireTime = DateTime.Now.AddMonths(12),
                Email = userModel.Email,

            };

            foreach (var role in userModel.Roles.Where(p => p.IsSelected))
            {
                userEntity.Roles.Add(this.roleRepository.GetItemByKey(role.Id));                
            }

            foreach (var project in userModel.Projects.Where(p => p.IsSelected))
            {
                userEntity.Projects.Add(this.projectRepository.GetItemByKey(project.Id));
            }

            userEntity.SetHashedPassword(userModel.Password);

            this.userRepository.Add(userEntity);
            this.unitOfWork.Commit();
            

        }

        public IEnumerable<UserListModel> GetDataList()
        {
            var data = this.userRepository.GetAllItems().Select(u => new UserListModel
            {
                Id = u.Id,                
                FullName = u.People == null ? "":u.People.FirstName+" "+u.People.LastName,                
                UserName = u.UserName ?? string.Empty,
                Roles= String.Join(", ", u.Roles.Select(r => r.Name).ToArray()),
                IsActive = u.IsActive
            }).ToList();

            return data;
        }

        private static void LoadPermissions()
        {
            // Search among the assemblies which defined the SecurityRegulatorAttribute.
            IEnumerable<Assembly> assemblies =
            //AppDomain.CurrentDomain.GetAssemblies().Where(a => a.IsDefined(typeof(SecurityRegulatorAttribute), false));
            AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.Contains("BSG.ADInventory.Common"));

            // Create new permission list
            permissions = new List<PermissionItem>();

            // Find PermissionContainerType in the found assemblies.
            foreach (Assembly assembly in assemblies)
            {
                //FieldInfo[] fields1 = assembly.GetType().GetFields(BindingFlags.Public | BindingFlags.Static
                //                              BindingFlags.NonPublic |
                //                              BindingFlags.Instance);
                //PropertyInfo[] propertyInfos;
                //propertyInfos = typeof(Common.Security.Permissions).GetProperties(BindingFlags.Public | BindingFlags.Static);

                //var property = assembly.GetType("Permissions")
                //                       .GetFields(BindingFlags.Public | BindingFlags.Static);

                var attribute = typeof(BSG.ADInventory.Common.Security.Permissions)
                                       .GetFields(BindingFlags.Public | BindingFlags.Static);

                //var attribute =
                //    (SecurityRegulatorAttribute)assembly.GetCustomAttributes(typeof(SecurityRegulatorAttribute), false).FirstOrDefault();

                if (attribute != null)
                {
                    //FieldInfo[] fields = attribute.PermissionContainerType.GetFields(BindingFlags.Public | BindingFlags.Static);

                    List<PermissionItem> permissionItems =
                        attribute.Where(p => p.FieldType == typeof(PermissionItem)).Select(p => (PermissionItem)p.GetValue(null)).ToList();

                    // Add all found permissions to permission list
                    permissions.AddRange(permissionItems);
                }
            }

            Debug.WriteIf(permissions.Count == 0, "PermissionContainer.LoadPermissions called but no permission found.");
        }
        public List<string> GetUserPermissions()
        {
            List<string> data = new List<string>();
            var user = this.userRepository.GetCurrentUser();
            var permissions = Permissions;//Zcf.Security.PermissionContainer.Permissions;
            foreach (var item in permissions)
            {
                if (UserExtensions.IsInPermission(user, new string[] { item.PermissionId.ToString() }) == true)
                {
                    data.Add(item.PermissionId.ToString());
                }
            }

            return data;
        }
        private static readonly object SyncObject = new object();

        private static List<PermissionItem> permissions;
        public static List<PermissionItem> Permissions
        {
            get
            {
                // Checking out of lock block to increase performance
                if (permissions != null)
                {
                    return permissions;
                }

                lock (SyncObject)
                {
                    if (permissions == null)
                    {
                        LoadPermissions();
                    }

                    return permissions;
                }
            }
        }
    }
}


