using BSG.ADInventory.Data;
using BSG.ADInventory.Entities;
using BSG.ADInventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMarkupMin.AspNet4.Mvc;
using Zcf.Data;
using Zcf.Paging;
using Zcf.Web.Mvc.Controllers;
using Zcf.Web.Mvc.Security;

namespace BSG.ADInventory.WebUI.Controllers
{
    [MinifyHtml]
    [Authorize]
    [Permission(PermissionIds = "SystemManagement,BaseDataManagement,MatGroupManagement", LoginUrl = "~/Error/Index/403")]
    public class MatGroupTechSpecController : CrudController<MatGroupTechSpec>
    {
        #region ctor
        private IUnitOfWork unitOfWork;        
        private readonly IMatGroupTechSpecService matGroupTechSpecService;        
        

        public MatGroupTechSpecController(IUnitOfWork unitOfWork, IMatGroupTechSpecService matGroupTechSpecService)
            : base(matGroupTechSpecService)
        {
            this.unitOfWork = unitOfWork;
            this.matGroupTechSpecService = matGroupTechSpecService;
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
        public ActionResult Index()
        {
            var data = this.matGroupTechSpecService.GetAllItems();
            return this.View(data);
        }

        [HttpPost]
        public override ActionResult Create(MatGroupTechSpec matGroupTechSpec)
        {
            if (!ModelState.IsValid)
            {
                return View(matGroupTechSpec);
            }
            return base.Create();
        }

        [HttpPost]
        public override ActionResult Edit(long id, MatGroupTechSpec matGroupTechSpec)
        {
            if (!ModelState.IsValid)
            {
                return View(matGroupTechSpec);
            }
            return base.Edit(id, matGroupTechSpec);
        }

        //[HttpGet]
        //public override ActionResult Create()
        //{

        //    return base.Create();
        //}


        //[HttpGet]
        //public override ActionResult Edit(long id)
        //{            
        //    return base.Edit(id);
        //}        
    }
}