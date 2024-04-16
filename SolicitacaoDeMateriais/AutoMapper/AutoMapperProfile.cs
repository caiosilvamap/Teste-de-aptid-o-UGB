using AutoMapper;
using SolicitacaoDeMateriais.Models;
using SolicitacaoDeMateriais.ViewModels;

namespace SolicitacaoDeMateriais.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDisplayViewModel>()
            .ForMember(
                dest => dest.DepartmentName,
                opt => opt.MapFrom(
                    src => src.DepartmentObj.Name));

            CreateMap<Product, ProductDisplayViewModel>()
            .ForMember(
            dest => dest.SupplierName,
            opt => opt.MapFrom(
                src => src.SupplierObj.Name));

            CreateMap<Service, ServiceDisplayViewModel>()
               .ForMember(
               dest => dest.SupplierName,
               opt => opt.MapFrom(
                   src => src.SupplierObj.Name));

            CreateMap<RequestProductService, RequestProductServiceDisplayViewModel>()
                .ForMember(
                    dest => dest.ProductName,
                    opt => opt.MapFrom(
                        src => src.ProductObj.Name))
                .ForMember(
                    dest => dest.ProductEANcode,
                    opt => opt.MapFrom(
                        src => src.ProductObj.EANcode))
                .ForMember(
                    dest => dest.ProductManufacturer,
                    opt => opt.MapFrom(
                        src => src.ProductObj.Manufacturer))
                .ForMember(
                    dest => dest.DepartmentName,
                    opt => opt.MapFrom(
                        src => src.DepartmentObj.Name))
                .ForMember(
                    dest => dest.ServiceName,
                    opt => opt.MapFrom(
                        src => src.ServiceObj.Name))
                .ForMember(
                    dest => dest.UserName,
                    opt => opt.MapFrom(
                        src => src.UserObj.Name))
                .ForMember(
                    dest => dest.ServiceSupplier,
                    opt => opt.MapFrom(
                        src => src.ServiceObj.SupplierObj.Name));

            CreateMap<StockEntry, StockEntryDisplayViewModel>()
                .ForMember(
                    dest => dest.ProductName,
                    opt => opt.MapFrom(
                        src => src.ProductObj.Name))
                .ForMember(
                    dest => dest.ProductSupplierName,
                    opt => opt.MapFrom(
                        src => src.ProductObj.SupplierObj.Name))
                .ForMember(
                    dest => dest.ProductEANcode,
                    opt => opt.MapFrom(
                        src => src.ProductObj.EANcode));

            CreateMap<StockExit, StockExitDisplayViewModel>()
                 .ForMember(
                    dest => dest.ProductName,
                    opt => opt.MapFrom(
                        src => src.ProductObj.Name))
                .ForMember(
                    dest => dest.ProductSupplierName,
                    opt => opt.MapFrom(
                        src => src.ProductObj.SupplierObj.Name))
                .ForMember(
                    dest => dest.ProductEANcode,
                    opt => opt.MapFrom(
                        src => src.ProductObj.EANcode))
                .ForMember(
                    dest => dest.UserName,
                    opt => opt.MapFrom(
                        src => src.UserObj.Name))
                .ForMember(
                    dest => dest.DepartmentName,
                    opt => opt.MapFrom(
                        src => src.DepartmentObj.Name));

            CreateMap<Stock, StockDisplayViewModel>()
                .ForMember(
                    dest => dest.ProductName,
                    opt => opt.MapFrom(
                        src => src.ProductObj.Name))
                .ForMember(
                    dest => dest.ProductEANcode,
                    opt => opt.MapFrom(
                        src => src.ProductObj.EANcode))
                 .ForMember(
                    dest => dest.ProductSupplierName,
                    opt => opt.MapFrom(
                        src => src.ProductObj.SupplierObj.Name))
                 .ForMember(
                    dest => dest.MinimumStock,
                    opt => opt.MapFrom(
                        src => src.ProductObj.MinimumStock));
          

            CreateMap<User, UserViewModel>().ReverseMap();

            CreateMap<Department, DepartmentViewModel>().ReverseMap();

            CreateMap<Product, ProductViewModel>().ReverseMap();

            CreateMap<Service, ServiceViewModel>().ReverseMap();

            CreateMap<RequestProductService, RequestProductServiceViewModel>().ReverseMap();

            CreateMap<Supplier, SupplierViewModel>().ReverseMap();

            CreateMap<StockEntry, StockEntryViewModel>().ReverseMap();

            CreateMap<StockExit, StockExitViewModel>().ReverseMap();

            CreateMap<Stock, StockViewModel>().ReverseMap();

        }
    }
}
