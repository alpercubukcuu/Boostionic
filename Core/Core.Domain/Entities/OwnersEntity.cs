using Core.Domain.Common;

namespace Core.Domain.Entities;

public class OwnersEntity : BaseEntity
{
    public Guid CompanyOwnerId { get; set; }
    public string CompanyOwnerTitle { get; set; }
}