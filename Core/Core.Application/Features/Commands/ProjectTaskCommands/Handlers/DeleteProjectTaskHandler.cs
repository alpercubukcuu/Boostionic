using AutoMapper;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.ProjectTaskCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Commands.ProjectTaskCommands.Handlers;

public class DeleteProjectTaskHandler : BaseCommandHandler,
    IRequestHandler<DeleteProjectTaskCommand, IResultDataDto<bool>>
{
    private readonly IMapper _mapper;
    private readonly IProjectTaskRepository _projectTaskRepository;
    private readonly ILogRepository _logRepository;
    private readonly IUserRepository _userRepository;

    public DeleteProjectTaskHandler(IMapper mapper, IProjectTaskRepository projectTaskRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _projectTaskRepository = projectTaskRepository;
        _userRepository = userRepository;
        _logRepository = logRepository;
    }

    public async Task<IResultDataDto<bool>> Handle(DeleteProjectTaskCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<bool> resultDataDto = new ResultDataDto<bool>();

        try
        {
            var getData = _projectTaskRepository.GetSingle(predicate: x => x.Id == request.Id);
            if (getData == null)
                resultDataDto.SetStatus(false).SetErrorMessage("Project Task not found")
                    .SetMessage("The Project Task related to the ID value could not be found.");
            getData.IsEnable = false;
            await _projectTaskRepository.UpdateAsync(getData);
            await AddUserLog("ProjectTask Delete Handler", "ProjectTask", getData.Id, TransactionEnum.Delete,
                getData.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}