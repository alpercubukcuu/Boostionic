using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.RolePermissionQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.RolePermissionQueries.Handlers;

public class
    GetByIdRolePermissionHandler : IRequestHandler<GetByIdRolePermissionQuery, IResultDataDto<RolePermissionDto>>
{
    private readonly IMapper _mapper;
    private readonly IRolePermissionRepository _rolePermissionRepository;

    public GetByIdRolePermissionHandler(IMapper mapper, IRolePermissionRepository rolePermissionRepository)
    {
        _mapper = mapper;
        _rolePermissionRepository = rolePermissionRepository;
    }

    public async Task<IResultDataDto<RolePermissionDto>> Handle(GetByIdRolePermissionQuery request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<RolePermissionDto> result = new ResultDataDto<RolePermissionDto>();
        try
        {
            var repositoryResult =
                _rolePermissionRepository.GetSingle(predicate: d => d.IsEnable == true && d.Id == request.Id);
            if (repositoryResult == null)
                result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("No data found for the ID value");
            var map = _mapper.Map<RolePermissionDto>(repositoryResult);
            return result;
        }
        catch (Exception exception)
        {
            return result.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}