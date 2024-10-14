using BSG.ADInventory.Data;
using BSG.ADInventory.Entities;
using BSG.ADInventory.Models.InvDocAttachment;
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

namespace BSG.ADInventory.WebUI.Controllers
{
    [MinifyHtml]
    [Authorize]
    [Permission(PermissionIds = "SystemManagement,BaseDataManagement,GeoLocations", LoginUrl = "~/Error/Index/403")]
    public class InvDocAttachmentController : CrudController<InvDocAttachment>
    {
        #region ctor
        private IUnitOfWork unitOfWork;        
        private readonly IInvDocAttachmentService invDocAttachmentService;        
        

        public InvDocAttachmentController(IUnitOfWork unitOfWork, IInvDocAttachmentService invDocAttachmentService)
            : base(invDocAttachmentService)
        {
            this.unitOfWork = unitOfWork;
            this.invDocAttachmentService = invDocAttachmentService;
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
            var data = this.invDocAttachmentService.GetInvDocAttachments(id);
            return PartialView("GetAttachments", data);
        }

        public ActionResult DownloadAttachment(string filePath)
        {                        
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(filePath));
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(filePath));
        }

    }
}