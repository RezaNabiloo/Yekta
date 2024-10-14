using AutoMapper;
using BSG.ADInventory.Entities;
using BSG.ADInventory.Models.DataTable;
using BSG.ADInventory.Models.Project;
using BSG.ADInventory.Models.Project;
using BSG.ADInventory.Resources;
using BSG.ADInventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
    [Permission(PermissionIds = "SystemManagement,BaseDataManagement,ProjectManagement", LoginUrl = "~/Error/Index/403")]
    public class ProjectController : CrudController<Project>
    {
        #region ctor
        private IUnitOfWork unitOfWork;        
        private readonly IProjectService _projectService;
        private readonly IProvinceService _provinceService;
        private readonly ITownshipService _townshipService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ProjectController(IUnitOfWork unitOfWork, IProjectService projectService, IProvinceService provinceService, ITownshipService townshipService,
            IUserService userService, IMapper mapper)
            : base(projectService)
        {
            this.unitOfWork = unitOfWork;
            _projectService = projectService;
            _provinceService = provinceService;
            _townshipService = townshipService;
            _userService = userService;
            _mapper = mapper;
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
            return this.View();
        }


        public ActionResult GetDataTableFilteredList(JqDataTable filter)
        {
            var query = this._projectService.GetAllItems().OrderByDescending(t => t.Id);
            var mappedQuery = _mapper.Map<List<ProjectListDTO>>(query);
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

        public ActionResult CrudAction(long id)
        {

            ViewBag.Id = id;
            Project project = new Project();
            if (id != 0)
            {
                project = this._projectService.GetItemByKey(id);
                this.ProvinceIds(project.Township.ProvinceId);
                this.TownshipIds(project.Township.ProvinceId, project.TownshipId);
            }
            else {
                this.ProvinceIds(null);
                this.TownshipIds(null, null);
            }
            
            var data = _mapper.Map<ProjectCEDTO>(project);
            
            return PartialView("CrudAction", data);

        }

        [HttpPost]
        public ActionResult SetCrudAction(ProjectCEDTO dataModel)
        {
            var township = _townshipService.GetItemByKey(dataModel.TownshipId);
            this.ProvinceIds(township.ProvinceId);
            this.TownshipIds(township.ProvinceId, township.Id);
        


            if (ModelState.IsValid)
            {
                try
                {
                    if (dataModel.Id > 0)
                    {
                        var project = _projectService.GetItemByKey(dataModel.Id);
                        _mapper.Map(dataModel, project);
                        this._projectService.Update(project);
                    }
                    else
                    {
                        var project = _mapper.Map<Project>(dataModel);
                        this._projectService.Add(project);
                    }
                    ModelState.Clear();
                    ViewBag.Result = true;
                    ViewBag.ErrorMessage = Resource.DataAddSuccessfully;
                }
                catch (Exception ex)
                {
                    ViewBag.Result = false;
                    ViewBag.ErrorMessage = ex.Message;

                }

                return PartialView("CreateOrEdit");
            }
            else
            {
                
                return PartialView("CreateOrEdit", dataModel);
            }

        }

        private void ProvinceIds(long? id)
        {

            this.ViewBag.ProvinceIds = new SelectList(this._provinceService.GetAllItems().Select(s => new { s.Id, s.Title }), "Id", "Title", id);
        }
        private void TownshipIds(long? provinceId, long? projectId)
        {
            this.ViewBag.TownshipIds = new SelectList(this._townshipService.GetAllItems().Where(t => t.ProvinceId == (provinceId ?? 0)).Select(s => new { s.Id, s.Title }), "Id", "Title", projectId);
        }




    }
}