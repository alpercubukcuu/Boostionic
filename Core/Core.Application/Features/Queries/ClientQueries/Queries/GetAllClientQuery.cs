using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Queries.ClientQueries.Queries;

public class GetAllClientQuery : IRequest<IResultDataDto<List<ClientDto>>>
{
}