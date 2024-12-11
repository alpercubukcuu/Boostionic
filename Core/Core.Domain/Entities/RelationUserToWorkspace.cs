using Core.Domain.Common;
namespace Core.Domain.Entities
{
    public class RelationUserToWorkspace : BaseEntity
    {
        public bool IsOwner { get; set; }
        public byte UserType { get; set; }
        public Guid UserRelationId { get; set; }
        public Guid WorkspaceId { get; set; }
        public Workspace Workspace { get; set; }
    }
}