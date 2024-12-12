using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Commands.UserRoleCommands.Commands;

public class DeleteUserRoleCommand : UserRoleDto, IRequest<IResultDataDto<bool>>
{
    public Guid Id { get; set; }
}