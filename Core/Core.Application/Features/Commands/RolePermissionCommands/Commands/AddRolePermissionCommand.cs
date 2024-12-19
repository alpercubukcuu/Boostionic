using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Commands.RolePermissionCommands.Commands;

public class AddRolePermissionCommand : RolePermissionDto, IRequest<IResultDataDto<RolePermissionDto>>
{
}