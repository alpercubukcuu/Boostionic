using AutoMapper;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.RolePermissionCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Commands.RolePermissionCommands.Handlers;

public class DeleteRolePermissionHandler : BaseCommandHandler,
    IRequestHandler<DeleteRolePermissionCommand, IResultDataDto<bool>>
{
    private readonly IMapper _mapper;
    private readonly IRolePermissionRepository _rolePermissionRepository;
    private readonly ILogRepository _logRepository;
    private readonly IUserRepository _userRepository;

    public DeleteRolePermissionHandler(IMapper mapper, IRolePermissionRepository rolePermissionRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _rolePermissionRepository = rolePermissionRepository;
        _userRepository = userRepository;
        _logRepository = logRepository;
    }

    public async Task<IResultDataDto<bool>> Handle(DeleteRolePermissionCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<bool> resultDataDto = new ResultDataDto<bool>();

        try
        {
            var getData = _rolePermissionRepository.GetSingle(predicate: x => x.Id == request.Id);
            if (getData == null)
                resultDataDto.SetStatus(false).SetErrorMessage("Role Permission not found")
                    .SetMessage("The Role Permission related to the ID value could not be found.");
            getData.IsEnable = false;
            await _rolePermissionRepository.UpdateAsync(getData);
            await AddUserLog("RolePermission Delete Handler", "RolePermission", getData.Id, TransactionEnum.Delete,
                getData.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}