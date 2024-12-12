using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.TaskRelationQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.TaskRelationQueries.Handlers;

public class
    GetAllTaskRelationHandler : IRequestHandler<GetAllTaskRelationQuery, IResultDataDto<List<TaskRelationDto>>>
{
    private readonly IMapper _mapper;
    private readonly ITaskRelationRepository _taskRelationRepository;

    public GetAllTaskRelationHandler(IMapper mapper, ITaskRelationRepository taskRelationRepository)
    {
        _mapper = mapper;
        _taskRelationRepository = taskRelationRepository;
    }

    public async Task<IResultDataDto<List<TaskRelationDto>>> Handle(GetAllTaskRelationQuery request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<List<TaskRelationDto>> result = new ResultDataDto<List<TaskRelationDto>>();
        try
        {
            var repoResult = _taskRelationRepository.GetAll(predicate: d => d.IsEnable == true);
            if (repoResult.Count() <= 0)
                result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("The list is empty!");
            var map = _mapper.Map<List<TaskRelationDto>>(repoResult);
            result.SetData(map);
            return result;
        }
        catch (Exception exception)
        {
            return result.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}