using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Queries.RolePermissionQueries.Queries;

public class GetAllRolePermissionQuery : IRequest<IResultDataDto<List<RolePermissionDto>>>
{
}