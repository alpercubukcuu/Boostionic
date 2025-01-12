using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.UserRoleQueries.Queries;
using Core.Application.Features.Queries.WorkspaceQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.Queries.WorkspaceQueries.Handlers;

public class GetAllByOwnerIdWorkspaceHandler : IRequestHandler<GetAllByOwnerIdWorkspaceQuery, IResultDataDto<List<WorkspaceDto>>>
{
    private readonly IMapper _mapper;
    private readonly IWorkspaceRepository _workspaceRepository;

    public GetAllByOwnerIdWorkspaceHandler(IMapper mapper, IWorkspaceRepository workspaceRepository)
    {
        _mapper = mapper;
        _workspaceRepository = workspaceRepository;
    }

    public async Task<IResultDataDto<List<WorkspaceDto>>> Handle(GetAllByOwnerIdWorkspaceQuery request, CancellationToken cancellationToken)
    {
        IResultDataDto<List<WorkspaceDto>> result = new ResultDataDto<List<WorkspaceDto>>();
        try
        {
            var repositoryResult = _workspaceRepository.GetAll(predicate: d => d.IsEnable == true && d.OwnerId == request.OwnerId, include: i => i.Include(i => i.Projects));
            if (!repositoryResult.Any()) result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("No data found for the ID value");

            var map = _mapper.Map<List<WorkspaceDto>>(repositoryResult);
            result.SetStatus().SetData(map).SetMessage("The workspaces have been found");

            return result;
        }
        catch (Exception exception)
        {
            return result.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}