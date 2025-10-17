using AutoMapper;
using MadereraMancino.Application.Dtos.Producto;
using MadereraMancino.Entities;

namespace MadereraMancino.WebAPI.Mapping
{
    public class ProductoMappingProfile: Profile
    {
        public ProductoMappingProfile()
        {
            CreateMap<Producto, ProductoResponseDto>();
            CreateMap<ProductoRequestDto, Producto>();
        }
    }
}
