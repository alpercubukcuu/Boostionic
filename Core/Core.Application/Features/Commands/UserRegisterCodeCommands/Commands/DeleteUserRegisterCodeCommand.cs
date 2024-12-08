using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;


namespace Core.Application.Features.Commands.UserRegisterCodeCommands.Commands
{
    public class DeleteUserRegisterCodeCommand : UserRegisterCodeDto, IRequest<IResultDataDto<bool>>
    {
        public Guid Id { get; set; }
    }
}
