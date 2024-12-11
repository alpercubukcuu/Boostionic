using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.ProjectTaskQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.ProjectTaskQueries.Handlers;

public class
    GetByIdProjectTaskHandler : IRequestHandler<GetByIdProjectTaskQuery, IResultDataDto<ProjectTaskDto>>
{
    private readonly IMapper _mapper;
    private readonly IProjectTaskRepository _projectTaskRepository;

    public GetByIdProjectTaskHandler(IMapper mapper, IProjectTaskRepository projectTaskRepository)
    {
        _mapper = mapper;
        _projectTaskRepository = projectTaskRepository;
    }

    public async Task<IResultDataDto<ProjectTaskDto>> Handle(GetByIdProjectTaskQuery request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<ProjectTaskDto> result = new ResultDataDto<ProjectTaskDto>();
        try
        {
            var repositoryResult =
                _projectTaskRepository.GetSingle(predicate: d => d.IsEnable == true && d.Id == request.Id);
            if (repositoryResult == null)
                result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("No data found for the ID value");
            var map = _mapper.Map<ProjectTaskDto>(repositoryResult);
            return result;
        }
        catch (Exception exception)
        {
            return result.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}