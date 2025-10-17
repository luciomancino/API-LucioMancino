using AutoMapper;
using MadereraMancino.Application.Dtos.Categoria;
using MadereraMancino.Entities;

namespace MadereraMancino.WebAPI.Mapping
{
    public class CategoriaMappingProfile: Profile
    {
        public CategoriaMappingProfile()
        {
            CreateMap<Categoria, CategoriaResponseDto>();
            CreateMap<CategoriaRequestDto, Categoria>();
        }
    }
}
