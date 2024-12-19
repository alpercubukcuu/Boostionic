using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.UserRoleQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.UserRoleQueries.Handlers;

public class
    GetByIdUserRoleHandler : IRequestHandler<GetByIdUserRoleQuery, IResultDataDto<UserRoleDto>>
{
    private readonly IMapper _mapper;
    private readonly IUserRoleRepository _userRoleRepository;

    public GetByIdUserRoleHandler(IMapper mapper, IUserRoleRepository userRoleRepository)
    {
        _mapper = mapper;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<IResultDataDto<UserRoleDto>> Handle(GetByIdUserRoleQuery request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<UserRoleDto> result = new ResultDataDto<UserRoleDto>();
        try
        {
            var repositoryResult =
                _userRoleRepository.GetSingle(predicate: d => d.IsEnable == true && d.Id == request.Id);
            if (repositoryResult == null)
                result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("No data found for the ID value");
            var map = _mapper.Map<UserRoleDto>(repositoryResult);
            return result;
        }
        catch (Exception exception)
        {
            return result.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}