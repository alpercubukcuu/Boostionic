using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;


namespace Core.Application.Features.Commands.UserRegisterCodeCommands.Commands
{
    public class AddUserRegisterCodeCommand : UserRegisterCodeDto,  IRequest<IResultDataDto<UserRegisterCodeDto>>
    {       
    }
}
