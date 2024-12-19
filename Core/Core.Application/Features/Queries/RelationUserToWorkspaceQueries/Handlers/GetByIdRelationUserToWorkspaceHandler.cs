using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.RelationUserToWorkspaceQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.RelationUserToWorkspaceQueries.Handlers;

public class
    GetByIdRelationUserToWorkspaceHandler : IRequestHandler<GetByIdRelationUserToWorkspaceQuery, IResultDataDto<RelationUserToWorkspaceDto>>
{
    private readonly IMapper _mapper;
    private readonly IRelationUserToWorkspaceRepository _relationUserToWorkspaceRepository;

    public GetByIdRelationUserToWorkspaceHandler(IMapper mapper, IRelationUserToWorkspaceRepository relationUserToWorkspaceRepository)
    {
        _mapper = mapper;
        _relationUserToWorkspaceRepository = relationUserToWorkspaceRepository;
    }

    public async Task<IResultDataDto<RelationUserToWorkspaceDto>> Handle(GetByIdRelationUserToWorkspaceQuery request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<RelationUserToWorkspaceDto> result = new ResultDataDto<RelationUserToWorkspaceDto>();
        try
        {
            var repositoryResult =
                _relationUserToWorkspaceRepository.GetSingle(predicate: d => d.IsEnable == true && d.Id == request.Id);
            if (repositoryResult == null)
                result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("No data found for the ID value");
            var map = _mapper.Map<RelationUserToWorkspaceDto>(repositoryResult);
            return result;
        }
        catch (Exception exception)
        {
            return result.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}