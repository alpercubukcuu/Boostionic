using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Commands.ClientCommands.Commands;

public class UpdateClientCommand : ClientDto , IRequest<IResultDataDto<ClientDto>>
{
    
}