

namespace Core.Domain.Interface.Entity
{
    public interface IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsEnable { get; set; }
    }
}
