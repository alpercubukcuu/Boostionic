using Core.Domain.Common;

namespace Core.Domain.Entities
{
    public class UserRole : BaseEntity
    {
        public string RoleName { get; set; }
        public RolePermission RolePermission { get; set; }

    }
}
