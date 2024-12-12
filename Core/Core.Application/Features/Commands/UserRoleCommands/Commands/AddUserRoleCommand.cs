using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Commands.UserRoleCommands.Commands;

public class AddUserRoleCommand : UserRoleDto, IRequest<IResultDataDto<UserRoleDto>>
{
}