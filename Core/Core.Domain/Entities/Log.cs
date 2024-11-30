using Core.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;


namespace Core.Domain.Entities
{
    public partial class Log : BaseEntity
    {
        public string Title { get; set; }
        public byte TransectionType { get; set; }
        public string Description { get; set; }
        public Guid DataId { get; set; }
        public string Entity { get; set; }

    }
    public partial class Log
    {
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }



    }
}
