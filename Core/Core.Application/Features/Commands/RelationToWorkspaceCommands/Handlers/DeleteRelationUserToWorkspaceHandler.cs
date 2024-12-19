using AutoMapper;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.RelationToWorkspaceCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Commands.RelationToWorkspaceCommands.Handlers;

public class DeleteRelationUserToWorkspaceHandler : BaseCommandHandler,
    IRequestHandler<DeleteRelationUserToWorkspaceCommand, IResultDataDto<bool>>
{
    private readonly IMapper _mapper;
    private readonly IRelationUserToWorkspaceRepository _relationUserToWorkspaceRepository;
    private readonly ILogRepository _logRepository;
    private readonly IUserRepository _userRepository;

    public DeleteRelationUserToWorkspaceHandler(IMapper mapper, IRelationUserToWorkspaceRepository relationUserToWorkspaceRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _relationUserToWorkspaceRepository = relationUserToWorkspaceRepository;
        _userRepository = userRepository;
        _logRepository = logRepository;
    }

    public async Task<IResultDataDto<bool>> Handle(DeleteRelationUserToWorkspaceCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<bool> resultDataDto = new ResultDataDto<bool>();

        try
        {
            var getData = _relationUserToWorkspaceRepository.GetSingle(predicate: x => x.Id == request.Id);
            if (getData == null)
                resultDataDto.SetStatus(false).SetErrorMessage("Relation User to Workspace not found")
                    .SetMessage("The Relation User to Workspace related to the ID value could not be found.");
            getData.IsEnable = false;
            await _relationUserToWorkspaceRepository.UpdateAsync(getData);
            await AddUserLog("RelationUserToWorkspace Delete Handler", "RelationUserToWorkspace", getData.Id, TransactionEnum.Delete,
                getData.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}