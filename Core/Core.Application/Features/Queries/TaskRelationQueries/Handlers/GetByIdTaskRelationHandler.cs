using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.TaskRelationQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.TaskRelationQueries.Handlers;

public class
    GetByIdTaskRelationHandler : IRequestHandler<GetByIdTaskRelationQuery, IResultDataDto<TaskRelationDto>>
{
    private readonly IMapper _mapper;
    private readonly ITaskRelationRepository _taskRelationRepository;

    public GetByIdTaskRelationHandler(IMapper mapper, ITaskRelationRepository taskRelationRepository)
    {
        _mapper = mapper;
        _taskRelationRepository = taskRelationRepository;
    }

    public async Task<IResultDataDto<TaskRelationDto>> Handle(GetByIdTaskRelationQuery request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<TaskRelationDto> result = new ResultDataDto<TaskRelationDto>();
        try
        {
            var repositoryResult =
                _taskRelationRepository.GetSingle(predicate: d => d.IsEnable == true && d.Id == request.Id);
            if (repositoryResult == null)
                result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("No data found for the ID value");
            var map = _mapper.Map<TaskRelationDto>(repositoryResult);
            return result;
        }
        catch (Exception exception)
        {
            return result.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}