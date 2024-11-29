using Core.Domain.Common;


namespace Core.Domain.Entities
{
    public class FileEntity : BaseEntity
    {
        public string FileName { get; set; } 
        public string FilePath { get; set; } 
        public string ContentType { get; set; } 
        public long Size { get; set; }

        public Guid ObjectId { get; set; }
        public string ObjectType { get; set; }

        public Guid OwnerId { get; set; }
        public OwnersEntity Owner { get; set; }

    }
}
