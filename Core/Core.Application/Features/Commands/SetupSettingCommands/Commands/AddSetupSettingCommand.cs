using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;


namespace Core.Application.Features.Commands.SetupSettingCommands.Commands
{
    public class AddSetupSettingCommand : SetupSettingDto, IRequest<IResultDataDto<SetupSettingDto>>
    {
        
    }
}