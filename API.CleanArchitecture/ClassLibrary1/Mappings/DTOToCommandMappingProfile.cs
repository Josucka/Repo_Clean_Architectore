using AutoMapper;
using Clean.Architecture.Application.DTOs;
using Clean.Architecture.Application.Products.Commands;

namespace Clean.Architecture.Application.Mappings
{
    public class DTOToCommandMappingProfile : Profile
    {
        public DTOToCommandMappingProfile()
        {
            CreateMap<ProductDTO, ProductCreateCommand>();
            CreateMap<ProductDTO, ProductUpdateCommand>();
        }
    }
}
