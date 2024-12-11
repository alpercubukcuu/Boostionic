using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.ProjectQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.ProjectQueries.Handlers;

public class
    GetByIdProjectHandler : IRequestHandler<GetByIdProjectQuery, IResultDataDto<ProjectDto>>
{
    private readonly IMapper _mapper;
    private readonly IProjectRepository _projectRepository;

    public GetByIdProjectHandler(IMapper mapper, IProjectRepository projectRepository)
    {
        _mapper = mapper;
        _projectRepository = projectRepository;
    }

    public async Task<IResultDataDto<ProjectDto>> Handle(GetByIdProjectQuery request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<ProjectDto> result = new ResultDataDto<ProjectDto>();
        try
        {
            var repositoryResult =
                _projectRepository.GetSingle(predicate: d => d.IsEnable == true && d.Id == request.Id);
            if (repositoryResult == null)
                result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("No data found for the ID value");
            var map = _mapper.Map<ProjectDto>(repositoryResult);
            return result;
        }
        catch (Exception exception)
        {
            return result.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}