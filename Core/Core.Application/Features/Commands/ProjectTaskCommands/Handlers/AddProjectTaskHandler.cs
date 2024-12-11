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

public class AddProjectTaskHandler : BaseCommandHandler,
    IRequestHandler<AddProjectTaskCommand, IResultDataDto<ProjectTaskDto>>
{
    private readonly IMapper _mapper;
    private readonly IProjectTaskRepository _projectTaskRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogRepository _logRepository;

    public AddProjectTaskHandler(IMapper mapper, IProjectTaskRepository projectTaskRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _projectTaskRepository = projectTaskRepository;
        _logRepository = logRepository;
        _userRepository = userRepository;
    }

    public async Task<IResultDataDto<ProjectTaskDto>> Handle(AddProjectTaskCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<ProjectTaskDto> resultDataDto = new ResultDataDto<ProjectTaskDto>();

        try
        {
            var mapperEntity = _mapper.Map<ProjectTask>(request);
            var addResult = await _projectTaskRepository.AddAsync(mapperEntity);
            var resultMapper = _mapper.Map<ProjectTaskDto>(addResult);
            resultDataDto.SetStatus().SetMessage("The create was successful").SetData(resultMapper);

            await AddUserLog("ProjectTask Create Handler", "ProjectTask", mapperEntity.Id, TransactionEnum.Create,
                addResult.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}