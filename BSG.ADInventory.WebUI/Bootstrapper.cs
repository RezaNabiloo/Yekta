namespace BSG.ADInventory.WebUI
{
    using System;
    using System.Web.Mvc;
    using Autofac;
    using Autofac.Integration.Mvc;
    using System.Reflection;
    using BSG.ADInventory.Services;
    using BSG.ADInventory.Web.UI.Mappers;
    using Zcf.Data;
    using Zcf.Services;
    using Zcf.Web.Mvc.Controllers;
    using AutoMapper;
    using Microsoft.Practices.Unity;
    using Zcf.Security;
    using BSG.ADInventory.Data;
    using Zcf.ExportImport;
    using System.Data.Entity;
    using log4net;
    using BSG.ADInventory.Services.Profiles;

    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
            AutoMapperConfiguration.Configure();
           // Mapper.AssertConfigurationIsValid();
            Authentication.Default.DataProvider = DIContainer.Default.Resolve<ProjectAuthenticationDataProvider>();
        }

        /// <summary>
        /// Initializes the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        public static void Initialize(IUnityContainer container)
        {

            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<BSG.ADInventory.ShaparakData.ShaparakContext, BSG.ADInventory.ShaparakData.Migrations.ShaparakConfiguration>());

            //container
            //    .RegisterType(typeof(Zcf.ExportImport.IExporter<>), typeof(Zcf.ExportImport.ExcelExporter<>), new HierarchicalLifetimeManager());

            //var executingAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            //container.ConfigureAutoRegistration()
            //    .ExcludeSystemAssemblies()
            //    .ExcludeAssemblies(a => a != executingAssembly)
            //    .Include(If.Any, Then.Register().UsingLifetime<HierarchicalLifetimeManager>())
            //    .ApplyAutoRegistration();
            //container.RegisterType<IRepository<Entities.Pos>, PosRepository>()
            //.RegisterType<IPosRepository, PosRepository>();


            //// Register service types
            //container
            //    .RegisterType<IUserService, UserService>(new HierarchicalLifetimeManager())
            //    .RegisterType<IRoleService, RoleService>(new HierarchicalLifetimeManager())
            //    .RegisterType<IDbFactory, DbFactory>(new HierarchicalLifetimeManager())
            //    .RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager())
            //    .RegisterType<BSG.ADInventory.Data.IUserRepository, UserRepository>(new HierarchicalLifetimeManager())
            //    .RegisterType<BSG.ADInventory.Data.IRoleRepository, RoleRepository>(new HierarchicalLifetimeManager())

            //    .RegisterType<Zcf.Security.IAuthenticationDataProvider, AuthenticationDataProvider>(new HierarchicalLifetimeManager());


            //var executingAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            //container.ConfigureAutoRegistration()
            //    .ExcludeSystemAssemblies()
            //    .ExcludeAssemblies(a => a != executingAssembly)
            //    .Include(If.Any, Then.Register().UsingLifetime<HierarchicalLifetimeManager>())
            //    .ApplyAutoRegistration();

        }

        /// <summary>
        /// Sets the autofac container.
        /// </summary>
        private static void SetAutofacContainer()
        {
            #region TODO commnet
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<HamiContext, BSG.ADInventory.Data.Migrations.Configuration>());                      
            //Database.SetInitializer(new Zcf.Data.DatabaseConfiguration());

            //IUnityContainer container = new UnityContainer();
            //container.ConfigureAutoRegistration()
            //    .ExcludeSystemAssemblies()
            //.Include(type => type.ImplementsOpenGeneric(typeof(ICommandHandler<>)),
            //    Then.Register().AsFirstInterfaceOfType().WithTypeName())
            //.ApplyAutoRegistration();

            //var container = new UnityContainer();

            //container
            //    .ConfigureAutoRegistration()
            //    .ExcludeAssemblies(a => a.GetName().FullName.Contains("Test"))
            //    .Include(If.Implements<ILogger>, Then.Register().UsingPerCallMode())
            //    .Include(If.ImplementsITypeName, Then.Register().WithTypeName())
            //    .Include(If.Implements<ICustomerRepository>, Then.Register().WithName("Sample"))
            //    .Include(If.Implements<IOrderRepository>,
            //             Then.Register().AsSingleInterfaceOfType().UsingPerCallMode())
            //    .Include(If.DecoratedWith<LoggerAttribute>,
            //             Then.Register()
            //                    .As<IDisposable>()
            //                    .WithTypeName()
            //                    .UsingLifetime<MyLifetimeManager>())
            //    .Exclude(t => t.Name.Contains("Trace"))
            //    .ApplyAutoRegistration();

            #endregion

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ADInventoryContext, BSG.ADInventory.Data.Migrations.Configuration>());
            var builder = new ContainerBuilder();

            #region  Register to Automapper that used in service layer

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            })).AsSelf().SingleInstance();

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().InstancePerLifetimeScope();
            #endregion  Register to Automapper that used in service layer



            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.Register<IDbFactory>(c => new DbFactory(new ADInventoryContext())).InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<AuthenticationDataProvider>().As<IAuthenticationDataProvider>().InstancePerHttpRequest();
            //builder.RegisterType<AccountSecurity>().As<IAccountSecurity>().InstancePerHttpRequest();            
            builder.RegisterType<Microsoft.Practices.Unity.UnityContainer>().As<Microsoft.Practices.Unity.IUnityContainer>().InstancePerHttpRequest();
            builder.RegisterGeneric(typeof(ExcelExporter<>)).As(typeof(IExporter<>));
            //builder.RegisterType<StimulReportHelper>().As<IStimulReportHelper>().InstancePerLifetimeScope();

            builder.Register(c => LogManager.GetLogger(typeof(Object))).As<ILog>();

            var data = Assembly.Load("BSG.ADInventory.Data");
            var services = Assembly.Load("BSG.ADInventory.Services");

            builder.RegisterAssemblyTypes(data)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(services)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(services)
            .AsClosedTypesOf(typeof(IRepository<>)).InstancePerHttpRequest();

            builder.RegisterAssemblyTypes(services)
            .AsClosedTypesOf(typeof(IExporter<>)).InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(services)
            .AsClosedTypesOf(typeof(IBaseService<>)).InstancePerHttpRequest();

            builder.RegisterAssemblyTypes(services)
            .AsClosedTypesOf(typeof(ICrudService<>)).InstancePerHttpRequest();

            builder.RegisterAssemblyTypes(services)
            .AsClosedTypesOf(typeof(ICrudController<>)).InstancePerHttpRequest();

            builder.RegisterAssemblyTypes(services)
            .AsClosedTypesOf(typeof(IBaseController<>)).InstancePerHttpRequest();

            builder.RegisterAssemblyTypes(services)
            .AsClosedTypesOf(typeof(ICudService<>)).InstancePerHttpRequest();

            builder.RegisterAssemblyTypes(services)
            .AsClosedTypesOf(typeof(ICrudController<>)).InstancePerHttpRequest();

            builder.RegisterAssemblyTypes(services)
            .AsClosedTypesOf(typeof(IBaseController<>)).InstancePerHttpRequest();

            builder.RegisterAssemblyTypes(services)
            .AsClosedTypesOf(typeof(IQueryableRepository<>)).InstancePerHttpRequest();

            builder.RegisterAssemblyTypes(services)
            .AsClosedTypesOf(typeof(IExportableService<>)).InstancePerHttpRequest();

            //builder.Register(c => new MerchantRequestReportController(c.Resolve<IRepository<Repository>>()));
            //builder.Register<IRepository<>>(x => new Repository<Entity>(new DbContext())).As<IRepository<IEntity>>();

            builder.RegisterFilterProvider();
            IContainer container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}
