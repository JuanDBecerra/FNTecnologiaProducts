using AutoMapper;
using Prueba.Domain.Entities.Dtos;
using Prueba.Domain.Entities.Model;
using Prueba.Domain.Entities.Request;

namespace Prueba.Infraestructure.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Products, ProductsDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Pro_Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Pro_Name))
                .ForMember(d => d.Price, opt => opt.MapFrom(src => src.Pro_Price))
                .ForMember(d => d.Description, opt => opt.MapFrom(src => src.Pro_Description))
                .ForMember(d => d.CategoryId, opt => opt.MapFrom(src => src.Pro_CategoryId))
                .ForMember(d => d.StatusId, opt => opt.MapFrom(src => src.Pro_StatusId))
                .ForMember(d => d.Category, opt => opt.MapFrom(src => src.Category.Cat_Description))
                .ForMember(d => d.Status, opt => opt.MapFrom(src => src.Status.Sta_Description))
                .ReverseMap();

            CreateMap<Products, ProductRequest>()
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Pro_Name))
                .ForMember(d => d.Price, opt => opt.MapFrom(src => src.Pro_Price))
                .ForMember(d => d.Description, opt => opt.MapFrom(src => src.Pro_Description))
                .ForMember(d => d.CategoryId, opt => opt.MapFrom(src => src.Pro_CategoryId))
                .ForMember(d => d.StatusId, opt => opt.MapFrom(src => src.Pro_StatusId))
                .ReverseMap();

            CreateMap<Status, CategoryDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Sta_Id))
                .ForMember(d => d.Description, opt => opt.MapFrom(src => src.Sta_Description))
                .ReverseMap();
            
            CreateMap<Category, CategoryDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Cat_Id))
                .ForMember(d => d.Description, opt => opt.MapFrom(src => src.Cat_Description))
                .ReverseMap();
        }
    }
}
