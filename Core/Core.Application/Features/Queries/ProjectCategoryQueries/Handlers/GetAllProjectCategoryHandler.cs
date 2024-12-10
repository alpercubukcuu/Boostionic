using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.ProjectCategoryQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.ProjectCategoryQueries.Handlers;

public class
    GetAllProjectCategoryHandler : IRequestHandler<GetAllProjectCategoryQuery, IResultDataDto<List<ProjectCategoryDto>>>
{
    private readonly IMapper _mapper;
    private readonly IProjectCategoryRepository _projectCategoryRepository;

    public GetAllProjectCategoryHandler(IMapper mapper, IProjectCategoryRepository projectCategoryRepository)
    {
        _mapper = mapper;
        _projectCategoryRepository = projectCategoryRepository;
    }

    public async Task<IResultDataDto<List<ProjectCategoryDto>>> Handle(GetAllProjectCategoryQuery request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<List<ProjectCategoryDto>> result = new ResultDataDto<List<ProjectCategoryDto>>();
        try
        {
            var repoResult = _projectCategoryRepository.GetAll(predicate: d => d.IsEnable == true);
            if (repoResult.Count() <= 0)
                result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("The list is empty!");
            var map = _mapper.Map<List<ProjectCategoryDto>>(repoResult);
            result.SetData(map);
            return result;
        }
        catch (Exception exception)
        {
            return result.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}