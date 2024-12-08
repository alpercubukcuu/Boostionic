using Core.Application.Dtos.CommonDtos;

namespace Core.Application.Dtos;

public class TicketDto : BaseDto
{
    public string  Title { get; set; }
    public string  Subject { get; set; }
    public byte  Priority  { get; set; }
    public string Message { get; set; }
        
    public Guid UserId { get; set; }
    public UserDto User { get; set; }

}