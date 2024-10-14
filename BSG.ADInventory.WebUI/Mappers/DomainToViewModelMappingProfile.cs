using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace BSG.ADInventory.Web.UI.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        //protected override void Configure()
        //{
        //    //Mapper.CreateMap<Customerel, CustomerelFormModel>();
        //    //Mapper.CreateMap<Expense, ExpenseFormModel>().ForMember(dest => dest.Customerel, opt => opt.Ignore());

        //}
    }
}