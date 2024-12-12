using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Queries.RolePermissionQueries.Queries;

public class GetByIdRolePermissionQuery : IRequest<IResultDataDto<RolePermissionDto>>
{
    public Guid Id { get; set; }
}