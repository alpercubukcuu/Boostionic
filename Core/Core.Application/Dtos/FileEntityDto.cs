using Core.Application.Dtos.CommonDtos;

namespace Core.Application.Dtos;

public class FileEntityDto : BaseDto
{
    public string FileName { get; set; } 
    public string FilePath { get; set; } 
    public string ContentType { get; set; } 
    public long Size { get; set; }

    public Guid ObjectId { get; set; }  
    public string ObjectType { get; set; } 

    public Guid OwnerId { get; set; }
    public OwnerEntityDto OwnerEntity { get; set; }
}