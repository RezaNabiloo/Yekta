using AutoMapper;
using BSG.ADInventory.Common.Enum;
using BSG.ADInventory.Entities;
using BSG.ADInventory.Models.Brand;
using BSG.ADInventory.Models.CarType;
using BSG.ADInventory.Models.Car;
using BSG.ADInventory.Models.Country;
using BSG.ADInventory.Models.Mat;
using BSG.ADInventory.Models.MatGroup;
using BSG.ADInventory.Models.MatUnit;
using BSG.ADInventory.Models.People;
using BSG.ADInventory.Models.ProductGroup;
using BSG.ADInventory.Models.Project;
using BSG.ADInventory.Models.ProjectDetail;
using BSG.ADInventory.Models.Province;
using BSG.ADInventory.Models.Township;
using BSG.ADInventory.Resources;
using BSG.ADInventory.Models.MatOrder;
using System.Security;
using BSG.ADInventory.Models.MatOrderDetail;

namespace BSG.ADInventory.Services.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Country
            CreateMap<Country, CountryListDTO>();
            CreateMap<Country, CountryCEDTO>().ReverseMap();

            
            #endregion Country

            #region Province
            CreateMap<Province, ProvinceListDTO>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country.Title))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive ? Resource.Yes : Resource.No));
            CreateMap<Province, ProvinceCEDTO>().ReverseMap();
            //CreateMap<Province, ProvinceUpdateDTO>().ReverseMap();
            #endregion Province

            #region Township
            CreateMap<Township, TownshipListDTO>()
                .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Province.Title))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive ? Resource.Yes : Resource.No))
                .ForMember(dest => dest.IsCapital, opt => opt.MapFrom(src => src.IsCapital ? Resource.Yes : Resource.No));
            CreateMap<Township, TownshipCEDTO>().ReverseMap();
            //CreateMap<Township, TownshipCreateDTO>().ReverseMap();
            //CreateMap<Township, TownshipUpdateDTO>().ReverseMap();
            #endregion Township

           

            #region Brand
            CreateMap<Brand, BrandListDTO>();
            CreateMap<Brand, BrandCEDTO>().ReverseMap();
            #endregion Brand


            #region MatGroup
            CreateMap<MatGroup, MatGroupCEDTO>().ReverseMap();
            CreateMap<MatGroup, MatGroupListDTO>();            
            CreateMap<MatGroup, MatGroupListVM>();
            #endregion MatGroup

            #region MatUnit
            CreateMap<MatUnit, MatUnitCEDTO>().ReverseMap();
            CreateMap<MatUnit, MatUnitListDTO>();
            #endregion MatUnit

            #region MatUnit
            CreateMap<Mat, MatCEDTO>().ReverseMap();
            CreateMap<Mat, MatListDTO>()
                .ForMember(dest => dest.MatGroupTitle, opt => opt.MapFrom(src => src.MatGroup.Title))
                .ForMember(dest => dest.BrandTitle, opt => opt.MapFrom(src => src.BrandId==null?string.Empty:src.Brand.FaTitle))
                .ForMember(dest => dest.MatUnitTitle, opt => opt.MapFrom(src => src.MatUnit.Title))
                .ForMember(dest => dest.MatAllocationType, opt => opt.MapFrom(src => Common.EnumHelper<MatAllocationType>.GetDisplayName(src.MatAllocationType)))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive?Resource.Active:Resource.InActive));
            #endregion MatUnit


            #region Project
            CreateMap<Project, ProjectCEDTO>().ReverseMap();
                
            CreateMap<Project, ProjectListDTO>()
                .ForMember(dest => dest.TownshipTitle, opt => opt.MapFrom(src => src.Township.Title))
                .ForMember(dest => dest.ProvinceTitle, opt => opt.MapFrom(src => src.Township.Province.Title))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive ? Resource.Active : Resource.InActive));


            CreateMap<ProjectDetail, ProjectDetailCEDTO>().ReverseMap();
            CreateMap<ProjectDetail, ProjectDetailListDTO>();
            #endregion Project


            #region People
            CreateMap<People, PeopleCEDTO>().ReverseMap();
            CreateMap<People, PeopleListDTO>()
                .ForMember(dest => dest.PeopleTypeTitle, opt => opt.MapFrom(src => Common.EnumHelper<PeopleType>.GetDisplayName(src.PeopleType)))
                .ForMember(dest => dest.GenderTitle, opt => opt.MapFrom(src => Common.EnumHelper<Gender>.GetDisplayName(src.Gender)))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive ? Resource.Active : Resource.InActive));
            #endregion People


            #region CarType
            CreateMap<CarType, CarTypeCEDTO>().ReverseMap();
            CreateMap<CarType, CarTypeListDTO>();
            #endregion CarType

            #region Car
            CreateMap<Car, CarCEDTO>().ReverseMap();
            CreateMap<Car, CarListDTO>()
                .ForMember(dest => dest.DriverName, opt => opt.MapFrom(src => src.OwnerDriver.FullName))
                .ForMember(dest => dest.PlateNumber, opt => opt.MapFrom(src => Resource.Iran + src.PlateSeries+"-"+src.PlateNumberPart1+src.PlateCharacter.Title+src.PlateNumberPart2))
                .ForMember(dest => dest.CarTypeTitle, opt => opt.MapFrom(src => src.CarType.Title))
                .ForMember(dest => dest.CarNatureTitle, opt => opt.MapFrom(src => Common.EnumHelper<CarNature>.GetDisplayName(src.CarNature)));
            #endregion Car



            #region MatOrder
            CreateMap<MatOrder, MatOrderCEDTO>()
                .ForMember(dest => dest.MatOrderDetails, opt => opt.MapFrom(src => src.MatOrderDetails))
                .ReverseMap();

            CreateMap<MatOrder, MatOrderListDTO>()
                .ForMember(dest => dest.ProjectTitle, opt => opt.MapFrom(src => src.Project.Title))
                .ForMember(dest => dest.PriorityTitle, opt => opt.MapFrom(src => Common.EnumHelper<Priority>.GetDisplayName(src.Priority)))
                .ForMember(dest => dest.ConfirmUserInfo, opt => opt.MapFrom(src => src.ConfirmUserId == null ? string.Empty : src.ConfirmUser.People.FullName))
                .ForMember(dest => dest.CreateUserInfo, opt => opt.MapFrom(src => src.CreateUserId == null ? string.Empty : src.CreateUser.People.FullName))
                .ForMember(dest => dest.MatItemCount, opt => opt.MapFrom(src => src.MatOrderDetails.Count));


            CreateMap<MatOrderDetail, MatOrderDetailCEDTO>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Mat.Title))
                .ReverseMap();
            #endregion MatOrder

        }


    }
}
