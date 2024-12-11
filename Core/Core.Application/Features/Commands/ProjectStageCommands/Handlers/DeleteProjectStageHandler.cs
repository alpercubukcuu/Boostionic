using AutoMapper;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.ProjectStageCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Commands.ProjectStageCommands.Handlers;

public class DeleteProjectStageHandler : BaseCommandHandler,
    IRequestHandler<DeleteProjectStageCommand, IResultDataDto<bool>>
{
    private readonly IMapper _mapper;
    private readonly IProjectStageRepository _projectStageRepository;
    private readonly ILogRepository _logRepository;
    private readonly IUserRepository _userRepository;

    public DeleteProjectStageHandler(IMapper mapper, IProjectStageRepository projectStageRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _projectStageRepository = projectStageRepository;
        _userRepository = userRepository;
        _logRepository = logRepository;
    }

    public async Task<IResultDataDto<bool>> Handle(DeleteProjectStageCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<bool> resultDataDto = new ResultDataDto<bool>();

        try
        {
            var getData = _projectStageRepository.GetSingle(predicate: x => x.Id == request.Id);
            if (getData == null)
                resultDataDto.SetStatus(false).SetErrorMessage("Project Stage not found")
                    .SetMessage("The Project Stage related to the ID value could not be found.");
            getData.IsEnable = false;
            await _projectStageRepository.UpdateAsync(getData);
            await AddUserLog("ProjectStage Delete Handler", "ProjectStage", getData.Id, TransactionEnum.Delete,
                getData.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}