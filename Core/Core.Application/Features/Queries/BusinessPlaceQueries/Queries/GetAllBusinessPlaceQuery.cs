using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;


namespace Core.Application.Features.Queries.BusinessPlaceQueries.Queries
{
    public class GetAllBusinessPlaceQuery : IRequest<IResultDataDto<List<BusinessPlaceDto>>>
    {
    }
}
