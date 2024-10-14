using BSG.ADInventory.Data;
using BSG.ADInventory.Entities;
using BSG.ADInventory.Models.PurchaseDocAttachment;
using BSG.ADInventory.Models.DataTable;
using BSG.ADInventory.Resources;
using BSG.ADInventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zcf.Data;
using Zcf.Models;
using Zcf.Paging;
using Zcf.Web.Mvc.Controllers;
using Zcf.Web.Mvc.Security;
using System.IO;
using WebMarkupMin.AspNet4.Mvc;
using BSG.ADInventory.Models.InvDocAttachment;

namespace BSG.ADInventory.WebUI.Controllers
{
    [MinifyHtml]
    [Authorize]
    [Permission(PermissionIds = "SystemManagement,BaseDataManagement,GeoLocations", LoginUrl = "~/Error/Index/403")]
    public class PurchaseDocAttachmentController : CrudController<PurchaseDocAttachment>
    {
        #region ctor
        private IUnitOfWork unitOfWork;        
        private readonly IPurchaseDocAttachmentService purchaseDocAttachmentService;        
        

        public PurchaseDocAttachmentController(IUnitOfWork unitOfWork, IPurchaseDocAttachmentService purchaseDocAttachmentService)
            : base(purchaseDocAttachmentService)
        {
            this.unitOfWork = unitOfWork;
            this.purchaseDocAttachmentService = purchaseDocAttachmentService;
        }

        [NonAction]
        public override ActionResult Index(PagedQueryable criteria)
        {
            return base.Index(criteria);
        }
        #endregion

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>

        public ActionResult GetAttachments(long id)
        {
            var data = this.purchaseDocAttachmentService.GetPurchaseDocAttachments(id);
            return PartialView("GetAttachments", data);
        }

        public ActionResult DownloadAttachment(string filePath)
        {                        
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(filePath));
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(filePath));
        }

        public ActionResult GetPurchaseDocAttachmentFile(long purchaseDocId)
        {
            PurchaseDocAttachmentDataModel data = new PurchaseDocAttachmentDataModel { PurchaseDocId = purchaseDocId };            
            return PartialView("UploadFile", data);
        }

        [HttpPost]
        public ActionResult SetPurchaseDocAttachmentFile(PurchaseDocAttachmentDataModel dataModel, HttpPostedFileBase filePath)
        {
            
            if (filePath == null)
            {
                ViewBag.Result = false;
                Response.StatusCode = 200;
                Response.StatusDescription = "NotOk";
                return PartialView("UploadFile", dataModel);
            }

            try
            {
                var uploadPath = Path.Combine(Server.MapPath("~/Uploads/PurchaseDoc/" + dataModel.PurchaseDocId + "/"));

                bool folderExits = DoesDirectoryExist(uploadPath);
                if (folderExits)
                {

                    string fileName = Guid.NewGuid().ToString().Replace("-", string.Empty);
                    string fileExtention = Path.GetExtension(filePath.FileName);

                    filePath.SaveAs(uploadPath + fileName + fileExtention);
                    PurchaseDocAttachment idc = new PurchaseDocAttachment { PurchaseDocId = dataModel.PurchaseDocId, FilePath = "/Uploads/PurchaseDoc/" + dataModel.PurchaseDocId + "/" + fileName + fileExtention, Title = dataModel.Title };
                    this.purchaseDocAttachmentService.Add(idc);
                }

                ViewBag.Result = true;
                ViewBag.ErrorMessage = Resource.DataSavedSuccessfully;
                Response.StatusCode = 200;
                Response.StatusDescription = "Ok";
                return PartialView("UploadFile");
            }
            catch (Exception ex)
            {
                //throw;
                Response.StatusCode = 503;
                Response.StatusDescription = ex.Message;
                return PartialView("UploadFile", dataModel);
            }



        }

        private static bool DoesDirectoryExist(string folderPath)
        {

            bool exists = System.IO.Directory.Exists(folderPath);

            if (!exists)
            {
                try
                {
                    System.IO.Directory.CreateDirectory(folderPath);
                    exists = true;
                }
                catch (Exception ex)
                {
                    //throw;
                    exists = false;
                    var reza = ex;
                }
            }
            return exists;
        }

    }
}