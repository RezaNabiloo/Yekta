using BSG.ADInventory.Data;
using BSG.ADInventory.Entities;

using BSG.ADInventory.Models.DataTable;
using BSG.ADInventory.Models.User;
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
    public class HomeController : Controller
    {

        private IUnitOfWork unitOfWork;
        private readonly IUserService userService;
        private readonly IPeopleService peopleService;




        public HomeController(IUnitOfWork unitOfWork, IUserService userService, IPeopleService peopleService)
        {
            this.unitOfWork = unitOfWork;
            this.userService = userService;
            this.peopleService = peopleService;
            //this.attendanceRequestService = attendanceRequestService;


        }

        public ActionResult Index()
        {
            //https://stackoverflow.com/questions/1064271/asp-net-mvc-set-custom-iidentity-or-iprincipal
            //var reza = (User as CustomPrincipal).Id;
            //var ali = (User as CustomPrincipal).FirstName;
            //var mohammad = (User as CustomPrincipal).LastName;
            var user = this.userService.GetCurrentUser();
            ViewBag.CurrentUser = "";// user.People.FullName;

            return View();

            //return RedirectToAction("Invitation");
        }


        #region Invitation
        public ActionResult Invitation()
        {
            ViewBag.UserPermisions = this.userService.GetUserPermissions();
            return View();
        }

        public ActionResult ConfirmInvitation()
        {
            ViewBag.UserPermisions = this.userService.GetUserPermissions();
            return View();
        }


        #endregion

        #region Attendance Request
        public ActionResult AttendanceRequest()
        {
            ViewBag.UserPermisions = this.userService.GetUserPermissions();
            return View();
        }

        //public ActionResult GetUserAttendanceRequests()
        //{
        //    ViewBag.UserPermisions = this.userService.GetUserPermissions();
        //    var data = this.attendanceRequestService.GetMyDataList().OrderByDescending(r=>r.StartTime).Select(r => new AttendanceRequestListModel
        //    {
        //        Id = r.Id,
        //        StartTime = r.StartTime.ToString(),
        //        EndTime = r.EndTime.ToString(),
        //        AttendanceReason = r.AttendanceReason,
        //        Confirmed = r.Confirmed
        //    }).ToList();

        //    return PartialView("_GetUserAttendnanceRequests");

        //}

        public ActionResult ConfirmAttendanceRequest()
        {
            ViewBag.UserPermisions = this.userService.GetUserPermissions();
            return View();
        }

        #endregion

        #region Car Request
        public ActionResult CarRequest()
        {
            ViewBag.UserPermisions = this.userService.GetUserPermissions();
            return View();
        }

        public ActionResult ConfirmCarRequest() 
        {
            ViewBag.UserPermisions = this.userService.GetUserPermissions();
            return View();
        }
        #endregion



        //public ActionResult DefineUsers ()
        //{
        //    var emps = this.peopleService.GetAllItems().Where(p => p.PeopleType == Common.Enum.PeopleType.Employee
        //    && p.BranchDepartment.BranchId == 1 && p.NationalCode != null && p.NationalCode.Length == 10
        //    && (p.NationalCode != "4280584176"
        //    && p.NationalCode != "0062994670"
        //    && p.NationalCode != "1652883983"
        //    && p.NationalCode != "0381196471"
        //    && p.NationalCode != "0491982119"
        //    && p.NationalCode != "0036931373"
        //    && p.NationalCode != "2170109749"
        //    )).ToList();



        //    foreach (var item in emps)
        //    {
        //        try
        //        {
        //            UserRoleModel u = new UserRoleModel
        //            {
        //                PeopleId = item.Id,
        //                UserName = item.NationalCode,
        //                Password = item.NationalCode,
        //                IsActive = true
        //            };
        //            this.userService.AddUserRoleModel(u);
        //        }
        //        catch (Exception)
        //        {

        //            //throw;
        //        }

        //    }
        //    return RedirectToAction("Invitation", "Home");
        //}

        public ActionResult Test() 
        {
            return View();
        }
    }
}