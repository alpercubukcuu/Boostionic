using Core.Domain.Common;


namespace Core.Domain.Entities
{
    public class Workspace : BaseEntity
    {
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
        public OwnerEntity Owner { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
