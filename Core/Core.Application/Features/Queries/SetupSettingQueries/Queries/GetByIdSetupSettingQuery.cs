using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;


namespace Core.Application.Features.Queries.SetupSettingQueries.Queries
{
    public class GetByIdSetupSettingQuery : IRequest<IResultDataDto<SetupSettingDto>>
    {
        public Guid Id { get; set; }
    }
}
