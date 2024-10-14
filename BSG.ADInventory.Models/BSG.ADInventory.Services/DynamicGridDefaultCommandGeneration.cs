namespace BSG.ADInventory.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zcf.Common.MVC.Helpers.DynamicGrid;
    using Zcf.Common.MVC.Helpers.DynamicGrid.CommandGeneration;

    public class DynamicGridDefaultCommandGeneration : ICommandGenerationController
    {
        public void BuildModel(ref List<GridFieldModel> gridFieldModel, 
            string field, 
            GridCommandColumn command = null, 
            string title = null, 
            string template = null, 
            int? width = null, 
            FieldAttributesModel fieldAttributes = null, 
            string filterUiName = null, 
            bool? isFilterable = null, 
            bool? isHidden = null)
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

        public bool CheckCommandGeneration(GridCommandType commandType)
        {
            return true;
        }


        public bool CheckCommandGeneration(string commandTypeName)
        {
            return true;
        }
    }
}
