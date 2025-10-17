using AutoMapper;
using MadereraMancino.Application.Dtos.Empleado;
using MadereraMancino.Entities;

namespace MadereraMancino.WebAPI.Mapping
{
    public class EmpleadoMappingProfile: Profile
    {
        public EmpleadoMappingProfile() 
        {
            CreateMap<Empleado, EmpleadoResponseDto>();
            CreateMap<EmpleadoRequestDto, Empleado>(); 
        }
        
    }
}
