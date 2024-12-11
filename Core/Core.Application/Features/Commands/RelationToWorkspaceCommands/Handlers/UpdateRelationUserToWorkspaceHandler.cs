using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.RelationToWorkspaceCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Commands.RelationToWorkspaceCommands.Handlers;

public class UpdateRelationUserToWorkspaceHandler : BaseCommandHandler,
    IRequestHandler<UpdateRelationUserToWorkspaceCommand, IResultDataDto<RelationUserToWorkspaceDto>>
{
    private readonly IMapper _mapper;
    private readonly IRelationUserToWorkspaceRepository _relationUserToWorkspaceRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogRepository _logRepository;

    public UpdateRelationUserToWorkspaceHandler(IMapper mapper, IRelationUserToWorkspaceRepository relationUserToWorkspaceRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _relationUserToWorkspaceRepository = relationUserToWorkspaceRepository;
        _userRepository = userRepository;
        _logRepository = logRepository;
    }

    public async Task<IResultDataDto<RelationUserToWorkspaceDto>> Handle(UpdateRelationUserToWorkspaceCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<RelationUserToWorkspaceDto> resultDataDto = new ResultDataDto<RelationUserToWorkspaceDto>();

        try
        {
            var getData = _relationUserToWorkspaceRepository.GetSingle(predicate: x => x.Id == request.Id);

            if (getData == null)
                resultDataDto.SetStatus(false).SetErrorMessage("Relation User to Workspace not found")
                    .SetMessage("No Relation User to Workspace found for the ID value");

            var mappedEntity = _mapper.Map<RelationUserToWorkspace>(getData);
            mappedEntity.CreatedDate = getData.CreatedDate;
            mappedEntity.Id = getData.Id;

            var addResult = await _relationUserToWorkspaceRepository.UpdateAsync(mappedEntity);
            var resultMapper = _mapper.Map<RelationUserToWorkspaceDto>(addResult);

            resultDataDto.SetStatus().SetMessage("The update process was successful").SetData(resultMapper);
            await AddUserLog("RelationUserToWorkspace Update Handler", "RelationUserToWorkspace", mappedEntity.Id,
                TransactionEnum.Update,
                getData.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}