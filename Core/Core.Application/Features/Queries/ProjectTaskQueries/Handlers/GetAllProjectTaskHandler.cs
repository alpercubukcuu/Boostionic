using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.ProjectTaskQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.ProjectTaskQueries.Handlers;

public class
    GetAllProjectTaskHandler : IRequestHandler<GetAllProjectTaskQuery, IResultDataDto<List<ProjectTaskDto>>>
{
    private readonly IMapper _mapper;
    private readonly IProjectTaskRepository _projectTaskRepository;

    public GetAllProjectTaskHandler(IMapper mapper, IProjectTaskRepository projectTaskRepository)
    {
        _mapper = mapper;
        _projectTaskRepository = projectTaskRepository;
    }

    public async Task<IResultDataDto<List<ProjectTaskDto>>> Handle(GetAllProjectTaskQuery request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<List<ProjectTaskDto>> result = new ResultDataDto<List<ProjectTaskDto>>();
        try
        {
            var repoResult = _projectTaskRepository.GetAll(predicate: d => d.IsEnable == true);
            if (repoResult.Count() <= 0)
                result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("The list is empty!");
            var map = _mapper.Map<List<ProjectTaskDto>>(repoResult);
            result.SetData(map);
            return result;
        }
        catch (Exception exception)
        {
            return result.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}