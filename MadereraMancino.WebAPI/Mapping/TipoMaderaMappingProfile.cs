using AutoMapper;
using MadereraMancino.Application.Dtos.TipoMadera;
using MadereraMancino.Entities;

namespace MadereraMancino.WebAPI.Mapping
{
    public class TipoMaderaMappingProfile : Profile
    {
        public TipoMaderaMappingProfile()
        {
            CreateMap<TipoMadera, TipoMaderaResponseDto>();
            CreateMap<TipoMaderaRequestDto, TipoMadera>();
        }
    }
}
