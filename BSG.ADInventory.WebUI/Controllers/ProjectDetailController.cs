using AutoMapper;
using BSG.ADInventory.Common.Classes;
using BSG.ADInventory.Data;
using BSG.ADInventory.Entities;
using BSG.ADInventory.Models.DataTable;
using BSG.ADInventory.Models.Project;
using BSG.ADInventory.Models.ProjectDetail;
using BSG.ADInventory.Resources;
using BSG.ADInventory.Services;
using BSG.ADInventory.WebUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    [Permission(PermissionIds = "SystemManagement,BaseDataManagement,GeoLocations", LoginUrl = "~/Error/Index/403")]
    public class ProjectDetailController : CrudController<ProjectDetail>
    {
        #region ctor
        private IUnitOfWork _unitOfWork;        
        private readonly IProjectDetailService _projectDetailService;
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectDetailController(IUnitOfWork unitOfWork, IProjectDetailService projectDetailService,
            IProjectService projectService, IMapper mapper)
            : base(projectDetailService)
        {
            _unitOfWork = unitOfWork;
            _projectDetailService = projectDetailService;
            _projectService = projectService;
            _mapper = mapper;
        }

        [NonAction]
        public override ActionResult Index(PagedQueryable criteria)
        {
            return base.Index(criteria);
        }
        #endregion

        public ActionResult Index(long id)
        {
            ViewBag.ProjectId = id;
            return this.View();
        }

        public ActionResult GetDataTableFilteredList(JqDataTable filter, long id)
        {
            var query = this._projectDetailService.GetProjectDetails(id);

            var mappedQuery = _mapper.Map<List<ProjectDetailListDTO>>(query);
            var filteredData = DataTableHelper.FilterAndSort(mappedQuery, filter);
            var data = filteredData.Skip(filter.iDisplayStart).Take(filter.iDisplayLength).ToList();

            var result = new
            {
                iTotalRecords = filteredData.Count(),//records per page 
                iTotalDisplayRecords = filteredData.Count(), //total table count
                aaData = data //data list
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        public ActionResult CrudAction(long id, long projectId)
        {
         
            ViewBag.Id = id;
            ViewBag.ProjectId = projectId;
            ProjectDetail projectDetail= new ProjectDetail { ProjectId = projectId};
            if (id != 0)
            {
                projectDetail = this._projectDetailService.GetItemByKey(id);
            }
            var data = _mapper.Map<ProjectDetailCEDTO>(projectDetail);
            return PartialView("CrudAction", data);
        }

        [HttpPost]
        public ActionResult SetCrudAction(ProjectDetailCEDTO dataModel)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (dataModel.Id > 0)
                    {
                        var projectDetail = this._projectDetailService.GetItemByKey(dataModel.Id);                        
                        _mapper.Map(dataModel, projectDetail);                        
                        this._projectDetailService.Update(projectDetail);                        
                    }
                    else
                    {
                        var projectDetail = _mapper.Map<ProjectDetail>(dataModel);                        
                        this._projectDetailService.Add(projectDetail);
                    }

                    ModelState.Clear();
                    ViewBag.Result = true;
                    ViewBag.ErrorMessage = Resource.DataAddSuccessfully;
                    Response.StatusDescription = "Ok";
                }
                catch (Exception ex)
                {
                    Response.StatusDescription = "NotOk";
                    ViewBag.Result = false;
                    ViewBag.ErrorMessage = ex.Message;
                }
                return PartialView("CreateOrEdit", dataModel);
            }
            else
            {
                Response.StatusDescription = "NotOk";
                ViewBag.Result = false;
                return PartialView("CreateOrEdit", dataModel);
            }

        }

       


    }
}