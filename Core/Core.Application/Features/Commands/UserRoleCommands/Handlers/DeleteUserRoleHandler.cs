using AutoMapper;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.UserRoleCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Commands.UserRoleCommands.Handlers;

public class DeleteUserRoleHandler : BaseCommandHandler,
    IRequestHandler<DeleteUserRoleCommand, IResultDataDto<bool>>
{
    private readonly IMapper _mapper;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly ILogRepository _logRepository;
    private readonly IUserRepository _userRepository;

    public DeleteUserRoleHandler(IMapper mapper, IUserRoleRepository userRoleRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _userRoleRepository = userRoleRepository;
        _userRepository = userRepository;
        _logRepository = logRepository;
    }

    public async Task<IResultDataDto<bool>> Handle(DeleteUserRoleCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<bool> resultDataDto = new ResultDataDto<bool>();

        try
        {
            var getData = _userRoleRepository.GetSingle(predicate: x => x.Id == request.Id);
            if (getData == null)
                resultDataDto.SetStatus(false).SetErrorMessage("User Role not found")
                    .SetMessage("The User Role related to the ID value could not be found.");
            getData.IsEnable = false;
            await _userRoleRepository.UpdateAsync(getData);
            await AddUserLog("UserRole Delete Handler", "UserRole", getData.Id, TransactionEnum.Delete,
                getData.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}