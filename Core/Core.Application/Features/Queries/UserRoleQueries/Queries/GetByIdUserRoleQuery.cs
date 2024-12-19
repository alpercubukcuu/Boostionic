using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Queries.UserRoleQueries.Queries;

public class GetByIdUserRoleQuery : IRequest<IResultDataDto<UserRoleDto>>
{
    public Guid Id { get; set; }
}