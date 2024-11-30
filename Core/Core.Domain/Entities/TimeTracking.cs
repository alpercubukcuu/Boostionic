using Core.Domain.Common;


namespace Core.Domain.Entities
{
    public class TimeTracking : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ProjectTaskId { get; set; }
        public ProjectTask ProjectTask { get; set; }
        public int Duration { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
