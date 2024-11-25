using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;


namespace Core.Application.Features.Commands.BusinessPlaceCommands.Commands
{
    public class DeleteBusinessPlaceCommand : BusinessPlaceDto, IRequest<IResultDataDto<bool>>
    {
        public Guid Id { get; set; }
    }
}