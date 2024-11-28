using Core.Domain.Common;

namespace Core.Domain.Entities
{
    public class RolePermission : OwnersEntity
    {
        public bool CanCreate { get; set; }
        public bool CanRead { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
    }
}
