using AutoMapper;
using MadereraMancino.Application.Dtos.Cliente;
using MadereraMancino.Entities;

namespace MadereraMancino.WebAPI.Mapping
{
    public class ClienteMappingProfile: Profile
    {
        public ClienteMappingProfile() 
        {
            CreateMap<Cliente, ClienteResponseDto>();
            CreateMap<ClienteRequestDto, Cliente>(); 
        }
            
    }
}
