using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;


namespace Core.Application.Features.Commands.BusinessPlaceCommands.Commands
{
    public class AddBusinessPlaceCommand : BusinessPlaceDto,  IRequest<IResultDataDto<BusinessPlaceDto>>
    {       
    }
}
