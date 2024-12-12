using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Queries.UserRoleQueries.Queries;

public class GetAllUserRoleQuery : IRequest<IResultDataDto<List<UserRoleDto>>>
{
}