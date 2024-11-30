using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;


namespace Core.Application.Features.Commands.OwnerEntityCommands.Commands
{
    public class UpdateOwnerEntityCommand : OwnerEntityDto, IRequest<IResultDataDto<OwnerEntityDto>>
    {
    }
}