using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.TimeTrackingQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.TimeTrackingQueries.Handlers;

public class
    GetAllTimeTrackingHandler : IRequestHandler<GetAllTimeTrackingQuery, IResultDataDto<List<TimeTrackingDto>>>
{
    private readonly IMapper _mapper;
    private readonly ITimeTrackingRepository _timeTrackingRepository;

    public GetAllTimeTrackingHandler(IMapper mapper, ITimeTrackingRepository timeTrackingRepository)
    {
        _mapper = mapper;
        _timeTrackingRepository = timeTrackingRepository;
    }

    public async Task<IResultDataDto<List<TimeTrackingDto>>> Handle(GetAllTimeTrackingQuery request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<List<TimeTrackingDto>> result = new ResultDataDto<List<TimeTrackingDto>>();
        try
        {
            var repoResult = _timeTrackingRepository.GetAll(predicate: d => d.IsEnable == true);
            if (repoResult.Count() <= 0)
                result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("The list is empty!");
            var map = _mapper.Map<List<TimeTrackingDto>>(repoResult);
            result.SetData(map);
            return result;
        }
        catch (Exception exception)
        {
            return result.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}