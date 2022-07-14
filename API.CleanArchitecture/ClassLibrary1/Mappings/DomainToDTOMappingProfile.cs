using AutoMapper;
using Clean.Architecture.Application.DTOs;
using Clean.Architecture.Domain.Entities;

namespace Clean.Architecture.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
