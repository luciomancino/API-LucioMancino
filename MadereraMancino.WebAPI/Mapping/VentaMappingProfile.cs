using AutoMapper;
using MadereraMancino.Application.Dtos.Venta;
using MadereraMancino.Entities;

namespace MadereraMancino.WebAPI.Mapping
{
    public class VentaMappingProfile : Profile
    {
        public VentaMappingProfile()
        {
            CreateMap<Venta, VentaResponseDto>()
            .ForMember(dest => dest.Fecha, ori => ori.MapFrom(src => src.Fecha.ToShortDateString()));
            CreateMap<VentaRequestDto, Venta>();
        }
    }
}
