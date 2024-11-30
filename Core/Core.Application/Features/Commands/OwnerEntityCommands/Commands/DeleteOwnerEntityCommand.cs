using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;


namespace Core.Application.Features.Commands.OwnerEntityCommands.Commands
{
    public class DeleteOwnerEntityCommand : OwnerEntityDto, IRequest<IResultDataDto<bool>>
    {
        public Guid Id { get; set; }
    }
}