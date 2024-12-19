using Core.Application.Dtos.CommonDtos;

namespace Core.Application.Dtos;

public class RelationUserToWorkspaceDto : BaseDto
{
    public bool IsOwner { get; set; }
    public byte UserType { get; set; }
    public Guid UserRelationId { get; set; }
    public Guid WorkspaceId { get; set; }
    public WorkspaceDto Workspace { get; set; }
}