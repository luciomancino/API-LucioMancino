using AutoMapper;
using MadereraMancino.Application.Dtos.Proveedor;
using MadereraMancino.Entities;

namespace MadereraMancino.WebAPI.Mapping
{
    public class ProveedorMappingProfile : Profile
    {
        public ProveedorMappingProfile()
        {
            CreateMap<Proveedor, ProveedorResponseDto>();
            CreateMap<ProveedorRequestDto, Proveedor>();
        }
    }
}
