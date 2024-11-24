using Core.Application.Dtos.CommonDtos;


namespace Core.Application.Dtos
{
    public class RolePermissionDto : BaseDto
    {
        public bool CanCreate { get; set; }
        public bool CanRead { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
    }
}
