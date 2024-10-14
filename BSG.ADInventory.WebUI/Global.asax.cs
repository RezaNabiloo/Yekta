
using BSG.ADInventory.Models.User;
using BSG.ADInventory.WebUI.Classes;
using GSD.Globalization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using Zcf.Data.CustomFiltering;

namespace BSG.ADInventory.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //protected void Application_Start()
        //{
        //    AreaRegistration.RegisterAllAreas();
        //    RouteConfig.RegisterRoutes(RouteTable.Routes);
        //}
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
                );

            //routes.MapRoute(
            //    "BasicInformation", // Route name
            //    "BasicInformation/{type}/{action}/{id}", // URL with parameters
            //    new { controller = "BasicInformation", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
            //    new[] { "Vermica.Web.UI.Controllers" });

            //routes.MapRoute(
            //    "Default",
            //    "{controller}.mvc/{action}/{id}",
            //    new { action = "Index", id = "" }
            //  );

            //routes.MapRoute(
            //  "Root",
            //  "",
            //  new { controller = "Home", action = "Index", id = "" }
            //);

        }

        /// <summary>
        /// Handles the AcquireTerminalStatus event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Application_AcquireTerminalStatus(object sender, EventArgs e)
        {
            var culture = CultureInfo.GetCultureInfo("fa");
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
        }

        private static bool IsValidAuthCookie(HttpCookie authCookie)
        {
            return authCookie != null && !String.IsNullOrEmpty(authCookie.Value);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(System.Web.Mvc.GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //ModelBinders.Binders.Add(typeof(ExcelFilterModel), new KendoGridExcelModelBinder());

            log4net.Config.XmlConfigurator.Configure(new FileInfo(this.Server.MapPath("~/Web.config")));

            Bootstrapper.Run();
            Bootstrapper.Initialize(DIContainer.Default);

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("fa-IR");
            var persianCulture = new PersianCulture();
            persianCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            persianCulture.DateTimeFormat.LongDatePattern = "dddd d MMMM yyyy";
            persianCulture.DateTimeFormat.AMDesignator = "صبح";
            persianCulture.DateTimeFormat.PMDesignator = "عصر";
            persianCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = persianCulture;
            Thread.CurrentThread.CurrentUICulture = persianCulture;
        }

        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    RegisterGlobalFilters(System.Web.Mvc.GlobalFilters.Filters);
        //    BundleConfig.RegisterBundles(BundleTable.Bundles);


        //    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("fa-IR");

        //    var persianCulture = new PersianCulture();
        //    persianCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
        //    persianCulture.DateTimeFormat.LongDatePattern = "dddd d MMMM yyyy";
        //    persianCulture.DateTimeFormat.AMDesignator = "صبح";
        //    persianCulture.DateTimeFormat.PMDesignator = "عصر";
        //    Thread.CurrentThread.CurrentCulture = persianCulture;
        //    Thread.CurrentThread.CurrentUICulture = persianCulture;            
        //}


        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                CustomPrincipalSerializeModel serializeModel = serializer.Deserialize<CustomPrincipalSerializeModel>(authTicket.UserData);

                CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
                //newUser.Id = serializeModel.Id;
                //newUser.PeopleId = serializeModel.PeopleId;
                //newUser.FirstName = serializeModel.FirstName;
                //newUser.Email = serializeModel.Email;
                //newUser.LastName = serializeModel.LastName;
                //newUser.BranchId = serializeModel.BranchId;
                //newUser.BranchInfo = serializeModel.BranchInfo;
                //newUser.BranchDepartmentId = serializeModel.BranchDepartmentId;
                //newUser.BranchDepartmentTitle = serializeModel.BranchDepartmentTitle;
                //newUser.ProfileImage = serializeModel.ProfileImage;
                //newUser.TownshipTitle = serializeModel.TownshipTitle;
                //HttpContext.Current.User = newUser;

                //Session["UserId"] = serializeModel.Id;
                //Session["PeopleId"] = serializeModel.PeopleId;
                //Session["FirstName"] = serializeModel.FirstName;
                //Session["LastName"] = serializeModel.LastName;
                //Session["BranchId"] = serializeModel.BranchId;
                //Session["BranchDepartmentId"] = serializeModel.BranchDepartmentId;
                //Session["BranchInfo"] = serializeModel.BranchInfo;
                //Session["BranchDepartmentTitle"] = serializeModel.BranchDepartmentTitle;
                //Session["TownshipTitle"] = serializeModel.TownshipTitle;
                //Session["ProfileImage"] = serializeModel.ProfileImage;
                //Session["UserMenus"] = this.userService.UserAccessMenu(user.Id);


                GlobalVariables.CurrentUser = newUser;
            }
        }



        protected void Session_End()
        {

        }


    }
}
