using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.TaskRelationCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Commands.TaskRelationCommands.Handlers;

public class UpdateTaskRelationHandler : BaseCommandHandler,
    IRequestHandler<UpdateTaskRelationCommand, IResultDataDto<TaskRelationDto>>
{
    private readonly IMapper _mapper;
    private readonly ITaskRelationRepository _taskRelationRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogRepository _logRepository;

    public UpdateTaskRelationHandler(IMapper mapper, ITaskRelationRepository taskRelationRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _taskRelationRepository = taskRelationRepository;
        _userRepository = userRepository;
        _logRepository = logRepository;
    }

    public async Task<IResultDataDto<TaskRelationDto>> Handle(UpdateTaskRelationCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<TaskRelationDto> resultDataDto = new ResultDataDto<TaskRelationDto>();

        try
        {
            var getData = _taskRelationRepository.GetSingle(predicate: x => x.Id == request.Id);

            if (getData == null)
                resultDataDto.SetStatus(false).SetErrorMessage("Task Relation not found")
                    .SetMessage("No Task Relation found for the ID value");

            var mappedEntity = _mapper.Map<TaskRelation>(getData);
            mappedEntity.CreatedDate = getData.CreatedDate;
            mappedEntity.Id = getData.Id;

            var addResult = await _taskRelationRepository.UpdateAsync(mappedEntity);
            var resultMapper = _mapper.Map<TaskRelationDto>(addResult);

            resultDataDto.SetStatus().SetMessage("The update process was successful").SetData(resultMapper);
            await AddUserLog("TaskRelation Update Handler", "TaskRelation", mappedEntity.Id,
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