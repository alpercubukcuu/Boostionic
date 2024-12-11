using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.ProjectTaskCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Commands.ProjectTaskCommands.Handlers;

public class UpdateProjectTaskStageHandler : BaseCommandHandler,
    IRequestHandler<UpdateProjectTaskCommand, IResultDataDto<ProjectTaskDto>>
{
    private readonly IMapper _mapper;
    private readonly IProjectTaskRepository _projectTaskRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogRepository _logRepository;

    public UpdateProjectTaskStageHandler(IMapper mapper, IProjectTaskRepository projectTaskRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _projectTaskRepository = projectTaskRepository;
        _userRepository = userRepository;
        _logRepository = logRepository;
    }

    public async Task<IResultDataDto<ProjectTaskDto>> Handle(UpdateProjectTaskCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<ProjectTaskDto> resultDataDto = new ResultDataDto<ProjectTaskDto>();

        try
        {
            var getData = _projectTaskRepository.GetSingle(predicate: x => x.Id == request.Id);

            if (getData == null)
                resultDataDto.SetStatus(false).SetErrorMessage("Project Task not found")
                    .SetMessage("No Project Task found for the ID value");

            var mappedEntity = _mapper.Map<ProjectTask>(getData);
            mappedEntity.CreatedDate = getData.CreatedDate;
            mappedEntity.Id = getData.Id;

            var addResult = await _projectTaskRepository.UpdateAsync(mappedEntity);
            var resultMapper = _mapper.Map<ProjectTaskDto>(addResult);

            resultDataDto.SetStatus().SetMessage("The update process was successful").SetData(resultMapper);
            await AddUserLog("ProjectTask Update Handler", "ProjectTask", mappedEntity.Id,
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