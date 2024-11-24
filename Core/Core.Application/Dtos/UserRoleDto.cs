using Core.Application.Dtos.CommonDtos;



namespace Core.Application.Dtos
{
    public class UserRoleDto : BaseDto
    {
        public string RoleName { get; set; }
        public RolePermissionDto RolePermission { get; set; }
    }
}
