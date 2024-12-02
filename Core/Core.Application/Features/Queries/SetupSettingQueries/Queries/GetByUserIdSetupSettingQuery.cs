using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;


namespace Core.Application.Features.Queries.SetupSettingQueries.Queries
{
    public class GetByUserIdSetupSettingQuery : IRequest<IResultDataDto<SetupSettingDto>>
    {
        public Guid UserId { get; set; }
    }
}
