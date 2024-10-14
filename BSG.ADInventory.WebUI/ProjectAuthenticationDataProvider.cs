namespace BSG.ADInventory.WebUI
{
    using Autofac;
    using Data;
    using Microsoft.Practices.Unity;
    using Services;
    using Zcf.Data;
    using Zcf.Security;
    using BSG.ADInventory.Data;
    using BSG.ADInventory.Services;
    

    public class ProjectAuthenticationDataProvider : Zcf.Security.IAuthenticationDataProvider
    {
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IUser GetUser(string name, string password)
        {
            var builder = new ContainerBuilder();
            builder.Register<IDbFactory>(c => new DbFactory(new ADInventoryContext())).InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<AuthenticationDataProvider>().As<IAuthenticationDataProvider>();
            builder.RegisterType<UserRepository>().As<Data.IUserRepository>();
            builder.RegisterType<RoleRepository>().As<Data.IRoleRepository>();

            var container = builder.Build();
            return container.Resolve<Zcf.Security.IAuthenticationDataProvider>().GetUser(name, password);

            //using (var container = DIContainer.Default.CreateChildContainer())
            //{
            //    return container.Resolve<Zcf.Security.IAuthenticationDataProvider>().GetUser(name, password);
            //}
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Zcf.Security.IUser GetUser(string name)
        {
            var builder = new ContainerBuilder();
            builder.Register<IDbFactory>(c => new DbFactory(new ADInventoryContext())).InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<AuthenticationDataProvider>().As<IAuthenticationDataProvider>();
            builder.RegisterType<UserRepository>().As<BSG.ADInventory.Data.IUserRepository>();
            builder.RegisterType<RoleRepository>().As<BSG.ADInventory.Data.IRoleRepository>();

            var container = builder.Build();
            return container.Resolve<IAuthenticationDataProvider>().GetUser(name);
            //using (var container = DIContainer.Default.CreateChildContainer())
            //{
            //    return container.Resolve<Zcf.Security.IAuthenticationDataProvider>().GetUser(name);
            //}
        }

        /// <summary>
        /// Gets the user by name or email.
        /// </summary>
        /// <param name="nameOrEmail">The name or email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Zcf.Security.IUser GetUserByNameOrEmail(string nameOrEmail, string password)
        {
            using (var container = DIContainer.Default.CreateChildContainer())
            {
                return container.Resolve<Zcf.Security.IAuthenticationDataProvider>().GetUser(nameOrEmail, password);
            }
        }

        public IUser GetUserByPhone(string phone, string verifyCode)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateUserLastActivityTime(string name)
        {
            string fName = name;
        }

        public void UpdateUserLastLoginTime(string name)
        {
            string fName = name;
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        //public void UpdateUser(Zcf.Security.IUser user)
        //{
        //    using (var container = DIContainer.Default.CreateChildContainer())
        //    {
        //        container.Resolve<Zcf.Security.IAuthenticationDataProvider>().UpdateUser(user);
        //    }
        //}
    }
}
