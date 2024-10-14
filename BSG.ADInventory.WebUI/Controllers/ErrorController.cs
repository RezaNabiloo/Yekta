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

namespace BSG.ADInventory.WebUI.Controllers
{
    [MinifyHtml]
    [Authorize]
    public class ErrorController : Controller
    {
   
        [HttpGet]
        public ActionResult Index(int? id, string ReturnUrl)
        {
            ViewBag.StatusCode = id;
            return View();
        }

   
    }
}