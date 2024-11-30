using Core.Domain.Common;


namespace Core.Domain.Entities
{
    public class TaskRelation : BaseEntity
    {
        public byte TaskType { get; set; }
        public Guid AssignedUserId { get; set; }
        public User AssignedUser { get; set; }
        public Guid ProjectTaskId { get; set; }
        public ProjectTask ProjectTask { get; set; }
    }
}
