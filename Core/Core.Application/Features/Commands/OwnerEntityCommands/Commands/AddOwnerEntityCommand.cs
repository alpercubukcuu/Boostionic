using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;


namespace Core.Application.Features.Commands.OwnerEntityCommands.Commands
{
    public class AddOwnerEntityCommand : OwnerEntityDto, IRequest<IResultDataDto<OwnerEntityDto>>
    {
        public string OwnerTitle { get; set; }
    }
}