using AutoMapper;
using Core.Application.Dtos;
using Core.Domain.Entities;


namespace Core.Application.Mappings
{
    public class RolePermissionProfile : Profile
    {
        public RolePermissionProfile()
        {
            CreateMap<RolePermission, RolePermissionDto>().ReverseMap();
            CreateMap<RolePermissionDto, RolePermission>().ReverseMap();
        }
        
    }
}
