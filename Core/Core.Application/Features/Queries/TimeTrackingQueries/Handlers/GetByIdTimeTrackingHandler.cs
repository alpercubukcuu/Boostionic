using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.TimeTrackingQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.TimeTrackingQueries.Handlers;

public class
    GetByIdTimeTrackingHandler : IRequestHandler<GetByIdTimeTrackingQuery, IResultDataDto<TimeTrackingDto>>
{
    private readonly IMapper _mapper;
    private readonly ITimeTrackingRepository _timeTrackingRepository;

    public GetByIdTimeTrackingHandler(IMapper mapper, ITimeTrackingRepository timeTrackingRepository)
    {
        _mapper = mapper;
        _timeTrackingRepository = timeTrackingRepository;
    }

    public async Task<IResultDataDto<TimeTrackingDto>> Handle(GetByIdTimeTrackingQuery request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<TimeTrackingDto> result = new ResultDataDto<TimeTrackingDto>();
        try
        {
            var repositoryResult =
                _timeTrackingRepository.GetSingle(predicate: d => d.IsEnable == true && d.Id == request.Id);
            if (repositoryResult == null)
                result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("No data found for the ID value");
            var map = _mapper.Map<TimeTrackingDto>(repositoryResult);
            return result;
        }
        catch (Exception exception)
        {
            return result.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}