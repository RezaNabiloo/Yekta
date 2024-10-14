namespace BSG.ADInventory.Services
{
    using System.Linq;
    using Zcf.Data;
    using BSG.ADInventory.Entities;
    using Zcf.Security;

    /// <summary>
    /// Create Authentication DataProvider 
    /// </summary>
    public class AuthenticationDataProvider : Zcf.Security.IAuthenticationDataProvider
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Data.IUserRepository userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationDataProvider" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="userRepository">The user repository.</param>
        public AuthenticationDataProvider(IUnitOfWork unitOfWork, Data.IUserRepository userRepository)
        {
            this.unitOfWork = unitOfWork;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Zcf.Security.IUser GetUser(string name, string password)
        {
            return this.userRepository.GetUser(name, password);
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Zcf.Security.IUser GetUser(string name)
        {
            return this.userRepository.GetUser(name);
        }

        /// <summary>
        /// Gets the user by name or email.
        /// </summary>
        /// <param name="nameOrEmail">The name or email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public Zcf.Security.IUser GetUserByNameOrEmail(string nameOrEmail, string password)
        {
            var usernameOrEmail = nameOrEmail.ToLower();
            return this.userRepository.Query
                .FirstOrDefault(u => (u.Email.ToLower() == usernameOrEmail || u.UserName.ToLower() == usernameOrEmail) && u.Password == password);
        }

        public IUser GetUserByPhone(string phone, string verifyCode)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void UpdateUser(Zcf.Security.IUser user)
        {
            this.userRepository.Update((Entities.User)user);
            this.unitOfWork.Commit();
        }

        /// <summary>
        /// Updates the user last activity time.
        /// </summary>
        /// <param name="name">The name.</param>
        public void UpdateUserLastActivityTime(string name)
        {
            string fName = name;
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="name">The name.</param>
        public void UpdateUserLastLoginTime(string name)
        {
            string fName = name;
        }
    }
}
