using BSG.ADInventory.Data;
using BSG.ADInventory.Entities;
using BSG.ADInventory.Models.Report;
using BSG.ADInventory.Resources;
using BSG.ADInventory.Services;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
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
    //[Permission(PermissionIds = "SystemManagement,ReportManagement", LoginUrl = "~/Error/Index/403")]
    public class ReportController : Controller
    {
        #region ctor
        private IUnitOfWork unitOfWork;
        private readonly IUserService userService;
        private readonly IInvDocService invDocService;
        private readonly IProjectService projectService;
        private readonly IMatService matService;
        private readonly IMatGroupService matGroeupService;
        private readonly IPurchaseDocService purchaseDocService;



        public ReportController(IUnitOfWork unitOfWork, IUserService userService, IInvDocService invDocService,
            ProjectService projectService, IMatService matService,
            IMatGroupService matGroeupService, IPurchaseDocService purchaseDocService)
        {
            this.unitOfWork = unitOfWork;
            this.userService = userService;
            this.invDocService = invDocService;
            this.projectService = projectService;
            this.matService = matService;
            this.matGroeupService = matGroeupService;
            this.purchaseDocService = purchaseDocService;
        }

        #endregion

        #region InvDoc

        public ActionResult PrintInvDoc()
        {
            return PartialView("_PrintInvDoc");
        }

        public ActionResult GetPrintInvDoc(long id)
        {

            StiReport report = new StiReport();

            Stimulsoft.Base.StiLicense.LoadFromFile(Server.MapPath("~/Reports/license.key"));
            Stimulsoft.Base.StiFontCollection.AddFontFile(System.Web.HttpContext.Current.Server.MapPath("/fonts/B Nazanin.TTF"));
            Stimulsoft.Base.StiFontCollection.AddFontFile(System.Web.HttpContext.Current.Server.MapPath("/fonts/B Nazanin Bold.TTF"));
            Stimulsoft.Base.StiFontCollection.AddFontFile(System.Web.HttpContext.Current.Server.MapPath("/fonts/B Nazanin Outline.TTF"));

            var invDoc = this.invDocService.GetItemByKey(id);

            switch (invDoc.InvDocTypeId)
            {
                case 1: report.Load(Server.MapPath("~/Reports/BSG_RepPrintEntranceDoc.mrt"));  break;
                case 2: report.Load(Server.MapPath("~/Reports/BSG_RepPrintInternalEntranceDoc.mrt")); break;
                case 4: report.Load(Server.MapPath("~/Reports/BSG_RepPrintUseDoc.mrt")); break;
                case 5: report.Load(Server.MapPath("~/Reports/BSG_RepPrintReturnDoc.mrt")); break;
                case 6: report.Load(Server.MapPath("~/Reports/BSG_RepPrintInternalBOLDoc.mrt")); break;
                case 7: report.Load(Server.MapPath("~/Reports/BSG_RepPrintExternalBOLDoc.mrt")); break;
                default:
                    break;
            }

            

            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["ADInventoryContext"].ConnectionString;
            report.Dictionary.Databases.Clear();
            report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("MS SQL", cs));
            StiImage stiImage = report.GetComponents()["ImageLogo"] as StiImage;
            Image logoImage = Image.FromFile(Server.MapPath("~/Reports/logo.jpg"));
            stiImage.Image = logoImage;


            report.Dictionary.Variables["VarId"].ValueObject = id;


            report.Compile();
            report.Render();
            //https://stackoverflow.com/questions/51212151/how-to-print-page-without-print-preview-from-stimulsoft-in-web-api
            // report.Print(showPrintDialog: true);
            return StiMvcViewer.GetReportResult(report);

        }

        #endregion


        #region Mat Stock
        public ActionResult MatStock()
        {
            ProjectIds();
            MatIds();
            MatGroupIds();
            return View();
        }

        [HttpGet]
        public ActionResult ShowMatStock(MatStockRepParam paramModel)
        {
            var data = this.matService.RepMatStock(paramModel);
            return PartialView("_ShowMatStock", data);
        }

        public ActionResult PrintMatStock()
        {
            return View("_PrintMatStock");
        }

        public ActionResult GetPrintMatStock(MatStockRepParam paramModel)
        {

            StiReport report = new StiReport();

            Stimulsoft.Base.StiLicense.LoadFromFile(Server.MapPath("~/Reports/license.key"));
            Stimulsoft.Base.StiFontCollection.AddFontFile(System.Web.HttpContext.Current.Server.MapPath("/fonts/B Nazanin.TTF"));
            Stimulsoft.Base.StiFontCollection.AddFontFile(System.Web.HttpContext.Current.Server.MapPath("/fonts/B Nazanin Bold.TTF"));
            Stimulsoft.Base.StiFontCollection.AddFontFile(System.Web.HttpContext.Current.Server.MapPath("/fonts/B Nazanin Outline.TTF"));

            report.Load(Server.MapPath("~/Reports/BSG_RepMatStock.mrt"));


            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["ADInventoryContext"].ConnectionString;
            report.Dictionary.Databases.Clear();
            report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("MS SQL", cs));
            StiImage stiImage = report.GetComponents()["ImageLogo"] as StiImage;
            Image logoImage = Image.FromFile(Server.MapPath("~/Reports/logo.jpg"));
            stiImage.Image = logoImage;

            var mats = this.matService.RepMatStock(paramModel);

            report.RegBusinessObject("Mat", mats);

            report.Compile();
            report.Render();

            return StiMvcViewer.GetReportResult(report);
        }

        #endregion


        #region Mat Use
        public ActionResult MatUse()
        {
            ProjectIds();
            MatIds();
            MatGroupIds();
            return View();
        }

        [HttpGet]
        public ActionResult ShowMatUse(MatStockRepParam paramModel)
        {
            var data = this.matService.RepMatUse(paramModel);
            return PartialView("_ShowMatUse", data);
        }

        public ActionResult PrintMatUse()
        {
            return View("_PrintMatuse");
        }

        public ActionResult GetPrintMatUse(MatStockRepParam paramModel)
        {

            StiReport report = new StiReport();

            Stimulsoft.Base.StiLicense.LoadFromFile(Server.MapPath("~/Reports/license.key"));
            Stimulsoft.Base.StiFontCollection.AddFontFile(System.Web.HttpContext.Current.Server.MapPath("/fonts/B Nazanin.TTF"));
            Stimulsoft.Base.StiFontCollection.AddFontFile(System.Web.HttpContext.Current.Server.MapPath("/fonts/B Nazanin Bold.TTF"));
            Stimulsoft.Base.StiFontCollection.AddFontFile(System.Web.HttpContext.Current.Server.MapPath("/fonts/B Nazanin Outline.TTF"));

            report.Load(Server.MapPath("~/Reports/BSG_RepMatuse.mrt"));


            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["ADInventoryContext"].ConnectionString;
            report.Dictionary.Databases.Clear();
            report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("MS SQL", cs));
            StiImage stiImage = report.GetComponents()["ImageLogo"] as StiImage;
            Image logoImage = Image.FromFile(Server.MapPath("~/Reports/logo.jpg"));
            stiImage.Image = logoImage;

            var mats = this.matService.RepMatUse(paramModel);

            report.RegBusinessObject("Mat", mats);

            report.Compile();
            report.Render();

            return StiMvcViewer.GetReportResult(report);
        }

        #endregion

        #region FollowPurchaseDoc
        public ActionResult FollowPurchaseDoc() 
        {
            FollowPurchaseDocRepParam data = new FollowPurchaseDocRepParam();
            return View(data);
        }


        [HttpGet]
        public ActionResult ShowFollowPurchaseDoc(FollowPurchaseDocRepParam paramModel)
        {
            
            switch (paramModel.ReportType)
            {
                case 1:
                    var data1 = this.purchaseDocService.RepPurchaseDocFollow(paramModel);
                    return PartialView("_ShowFollowPurchaseDoc1", data1);
                    
                case 2:
                    var data2 = this.purchaseDocService.RepPurchaseDocDetail(paramModel);
                    return PartialView("_ShowFollowPurchaseDoc2", data2);

                case 3:
                    var data3 = this.purchaseDocService.RepPurchaseDocInvTransaction(paramModel);
                    return PartialView("_ShowFollowPurchaseDoc3", data3);

                case 4:
                    var data4 = this.purchaseDocService.RepPurchaseDocInvTransaction(paramModel);
                    return PartialView("_ShowFollowPurchaseDoc4", data4);

                case 5:
                    var data5 = this.purchaseDocService.RepPurchaseDocInvTransaction(paramModel);
                    return PartialView("_ShowFollowPurchaseDoc5", data5);

                case 6:
                    var data6 = this.purchaseDocService.RepPurchaseDocInvTransaction(paramModel);
                    return PartialView("_ShowFollowPurchaseDoc6", data6);

                default:                    
                    break;
            }

            return null;

        }

        public ActionResult PrintFollowPurchaseDoc()
        {
            return View("_PrintMatStock");
        }

        public ActionResult GetPrintFollowPurchaseDoc(MatStockRepParam paramModel)
        {

            StiReport report = new StiReport();

            Stimulsoft.Base.StiLicense.LoadFromFile(Server.MapPath("~/Reports/license.key"));
            Stimulsoft.Base.StiFontCollection.AddFontFile(System.Web.HttpContext.Current.Server.MapPath("/fonts/B Nazanin.TTF"));
            Stimulsoft.Base.StiFontCollection.AddFontFile(System.Web.HttpContext.Current.Server.MapPath("/fonts/B Nazanin Bold.TTF"));
            Stimulsoft.Base.StiFontCollection.AddFontFile(System.Web.HttpContext.Current.Server.MapPath("/fonts/B Nazanin Outline.TTF"));

            report.Load(Server.MapPath("~/Reports/BSG_RepMatStock.mrt"));


            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["ADInventoryContext"].ConnectionString;
            report.Dictionary.Databases.Clear();
            report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("MS SQL", cs));
            StiImage stiImage = report.GetComponents()["ImageLogo"] as StiImage;
            Image logoImage = Image.FromFile(Server.MapPath("~/Reports/logo.jpg"));
            stiImage.Image = logoImage;

            var mats = this.matService.RepMatStock(paramModel);

            report.RegBusinessObject("Mat", mats);

            report.Compile();
            report.Render();

            return StiMvcViewer.GetReportResult(report);
        }

        #endregion FollowPurchaseDoc

        #region PurchaseDoc
        
        public ActionResult PrintPurchaseDoc()
        {
            return View("_PrintPurchaseDoc");
        }

        public ActionResult GetPrintPurchaseDoc(long id)
        {

            StiReport report = new StiReport();

            Stimulsoft.Base.StiLicense.LoadFromFile(Server.MapPath("~/Reports/license.key"));
            Stimulsoft.Base.StiFontCollection.AddFontFile(System.Web.HttpContext.Current.Server.MapPath("/fonts/B Nazanin.TTF"));
            Stimulsoft.Base.StiFontCollection.AddFontFile(System.Web.HttpContext.Current.Server.MapPath("/fonts/B Nazanin Bold.TTF"));
            Stimulsoft.Base.StiFontCollection.AddFontFile(System.Web.HttpContext.Current.Server.MapPath("/fonts/B Nazanin Outline.TTF"));

            report.Load(Server.MapPath("~/Reports/BSG_RepPrintPurchaseDoc.mrt"));


            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["ADInventoryContext"].ConnectionString;
            report.Dictionary.Databases.Clear();
            report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("MS SQL", cs));
            StiImage stiImage = report.GetComponents()["ImageLogo"] as StiImage;
            Image logoImage = Image.FromFile(Server.MapPath("~/Reports/logo.jpg"));
            stiImage.Image = logoImage;

            report.Dictionary.Variables["VarId"].ValueObject = id;           

            report.Compile();
            report.Render();

            return StiMvcViewer.GetReportResult(report);
        }

        #endregion PurchaseDoc

        public ActionResult ViewerEvent()
        {
            return StiMvcViewer.ViewerEventResult();

        }


        private void MatIds()
        {
            this.ViewBag.MatIds = new SelectList(this.matService.GetAllItems().Select(s => new { Id = s.Id, Title = s.Title.ToString()}), "Id", "Title");
        }

        private void ProjectIds()
        {
            this.ViewBag.ProjectIds = new SelectList(this.projectService.GetAllItems().Select(s => new { s.Id, s.Title }), "Id", "Title");
        }

        private void MatGroupIds()
        {
            this.ViewBag.MatGroupIds = new SelectList(this.matGroeupService.GetAllItems().Select(s => new { s.Id, s.Title }), "Id", "Title");
        }

    }
}
