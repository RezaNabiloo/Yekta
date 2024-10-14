namespace BSG.ADInventory.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;
    using Zcf.Common.MVC.Helpers.DynamicGrid.CommandGeneration;
    using Zcf.Common.MVC.Helpers.DynamicGrid;

    public class DynamicGridSecurityBaseCommandGenrationHandler : ICommandGenerationController
    {
        private readonly HttpContextBase httpContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicGridSecurityBaseCommandGenrationHandler" /> class.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>        
        public DynamicGridSecurityBaseCommandGenrationHandler(HttpContextBase httpContext)
        {
            this.httpContext = httpContext;
        }

        /// <summary>
        /// Builds the model.
        /// </summary>
        /// <param name="gridFieldModel">The grid field model.</param>
        /// <param name="field">The field.</param>
        /// <param name="command">The command.</param>
        /// <param name="title">The title.</param>
        /// <param name="template">The template.</param>
        /// <param name="width">The width.</param>
        /// <param name="fieldAttributes">The field attributes.</param>
        /// <param name="filterUiName">Name of the filter UI.</param>
        /// <param name="isFilterable">if set to <c>true</c> [is filterable].</param>
        /// <param name="isHidden"></param>
        public void BuildModel(ref List<GridFieldModel> gridFieldModel, string field, GridCommandColumn command = null, 
            string title = null, string template = null, 
            int? width = null, FieldAttributesModel fieldAttributes = null, 
            string filterUiName = null, bool? isFilterable = null, bool? isHidden = null)
        {
            gridFieldModel.Add(new GridFieldModel()
            {
                field = field,
                width = width,
                hidden = isHidden.HasValue ? isHidden.Value : false,
                title = title,
                template = template,
                IsFilterAble = isFilterable.HasValue ? isFilterable.Value : true,
                FieldAttributeModel = fieldAttributes,
                filterable = new GridFieldFilterableModel()
                {
                    UI = filterUiName
                },
                Command = command
            });
        }

        /// <summary>
        /// Checks the command generation.
        /// </summary>
        /// <param name="commandType">Type of the command.</param>
        /// <returns></returns>
        public bool CheckCommandGeneration(GridCommandType commandType)
        {
            return CheckGenerationState(commandType.ToString());
        }
        
        /// <summary>
        /// Checks the command generation.
        /// </summary>
        /// <param name="commandTypeName">Name of the command type.</param>
        /// <returns></returns>
        public bool CheckCommandGeneration(string commandTypeName)
        {
            return CheckGenerationState(commandTypeName);
        }

        /// <summary>
        /// Checks the state of the generation.
        /// </summary>
        /// <param name="commandName">Name of the command.</param>
        /// <returns></returns>
        private bool CheckGenerationState(string commandName) 
        {
            //Func<Element, bool> permissionChecker = element => 
            //{
            //    if (element.Operations.Any(w => w.IsValid &&
            //        w.Name.ToLower() == commandName.ToLower()))
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //};

            //return accountSecurity.HasPermission(httpContext.Request.RequestContext.RouteData, permissionChecker);
            return true;

        }

    }
}
