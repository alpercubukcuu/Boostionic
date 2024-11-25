using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;


namespace Core.Application.Features.Commands.UserCommands.Commands
{
    public class UpdateUserResetPasswordCommand : UserResetPasswordDto, IRequest<IResultDataDto<UserResetPasswordDto>>
    {
    }
}
