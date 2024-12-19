using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Commands.RolePermissionCommands.Commands;

public class UpdateRolePermissionCommand : RolePermissionDto, IRequest<IResultDataDto<RolePermissionDto>>
{
}