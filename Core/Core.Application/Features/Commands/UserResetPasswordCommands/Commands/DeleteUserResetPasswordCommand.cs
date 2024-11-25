using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;


namespace Core.Application.Features.Commands.UserCommands.Commands
{
    public class DeleteUserResetPasswordCommand : UserResetPasswordDto, IRequest<IResultDataDto<bool>>
    {
        public Guid Id { get; set; }
    }
}
