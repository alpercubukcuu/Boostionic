using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;


namespace Core.Application.Features.Commands.UserCommands.Commands
{
    public class UpdateUserPasswordCommand : UserDto, IRequest<IResultDataDto<UserDto>>
    {
        public Guid UserId { get; set; }
        public string ConfirmedPassword { get; set; }
    }
}
