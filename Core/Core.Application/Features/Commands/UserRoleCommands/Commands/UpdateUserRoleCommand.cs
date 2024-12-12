using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Commands.UserRoleCommands.Commands;

public class UpdateUserRoleCommand : UserRoleDto, IRequest<IResultDataDto<UserRoleDto>>
{
}