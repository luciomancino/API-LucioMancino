using AutoMapper;
using MadereraMancino.Application.Dtos.Identity.Roles;
using MadereraMancino.Entities.MicrosoftIdentity;

namespace MadereraMancino.WebAPI.Mapping
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<Role, RoleResponseDto>();
            CreateMap<RoleRequestDto, Role>();
        }
    }
}
