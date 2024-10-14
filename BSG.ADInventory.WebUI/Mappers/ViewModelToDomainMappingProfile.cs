using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace BSG.ADInventory.Web.UI.Mappers
{
    class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        //protected override void Configure()
        //{
        //    //Mapper.CreateMap<CustomerelFormModel, CreateOrUpdateCustomerelCommand>();
        //    //Mapper.CreateMap<ExpenseFormModel, CreateOrUpdateExpenseCommand>();
        //    //Mapper.CreateMap<UserFormModel, UserRegisterCommand>(); 
        //}
    }
}