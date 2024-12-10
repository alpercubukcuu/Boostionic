using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.ProjectCategoryQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.ProjectCategoryQueries.Handlers;

public class
    GetByIdProjectCategoryHandler : IRequestHandler<GetByIdProjectCategoryQuery, IResultDataDto<ProjectCategoryDto>>
{
    private readonly IMapper _mapper;
    private readonly IProjectCategoryRepository _projectCategoryRepository;

    public GetByIdProjectCategoryHandler(IMapper mapper, IProjectCategoryRepository projectCategoryRepository)
    {
        _mapper = mapper;
        _projectCategoryRepository = projectCategoryRepository;
    }

    public async Task<IResultDataDto<ProjectCategoryDto>> Handle(GetByIdProjectCategoryQuery request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<ProjectCategoryDto> result = new ResultDataDto<ProjectCategoryDto>();
        try
        {
            var repositoryResult =
                _projectCategoryRepository.GetSingle(predicate: d => d.IsEnable == true && d.Id == request.Id);
            if (repositoryResult == null)
                result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("No data found for the ID value");
            var map = _mapper.Map<ProjectCategoryDto>(repositoryResult);
            return result;
        }
        catch (Exception exception)
        {
            return result.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}