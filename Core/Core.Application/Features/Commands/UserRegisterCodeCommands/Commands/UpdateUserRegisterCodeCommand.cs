using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;


namespace Core.Application.Features.Commands.UserRegisterCodeCommands.Commands
{
    public class UpdateUserRegisterCodeCommand : UserRegisterCodeDto, IRequest<IResultDataDto<UserRegisterCodeDto>>
    {
    }
}
