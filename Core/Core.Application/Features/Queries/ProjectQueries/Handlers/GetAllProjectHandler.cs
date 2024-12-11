using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.ProjectQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.ProjectQueries.Handlers;

public class
    GetAllProjectHandler : IRequestHandler<GetAllProjectQuery, IResultDataDto<List<ProjectDto>>>
{
    private readonly IMapper _mapper;
    private readonly IProjectRepository _projectRepository;

    public GetAllProjectHandler(IMapper mapper, IProjectRepository projectRepository)
    {
        _mapper = mapper;
        _projectRepository = projectRepository;
    }

    public async Task<IResultDataDto<List<ProjectDto>>> Handle(GetAllProjectQuery request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<List<ProjectDto>> result = new ResultDataDto<List<ProjectDto>>();
        try
        {
            var repoResult = _projectRepository.GetAll(predicate: d => d.IsEnable == true);
            if (repoResult.Count() <= 0)
                result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("The list is empty!");
            var map = _mapper.Map<List<ProjectDto>>(repoResult);
            result.SetData(map);
            return result;
        }
        catch (Exception exception)
        {
            return result.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}