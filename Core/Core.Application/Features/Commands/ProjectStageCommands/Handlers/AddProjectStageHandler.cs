using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.ProjectStageCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Commands.ProjectStageCommands.Handlers;

public class AddProjectStageHandler : BaseCommandHandler,
    IRequestHandler<AddProjectStageCommand, IResultDataDto<ProjectStageDto>>
{
    private readonly IMapper _mapper;
    private readonly IProjectStageRepository _projectStageRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogRepository _logRepository;

    public AddProjectStageHandler(IMapper mapper, IProjectStageRepository projectStageRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _projectStageRepository = projectStageRepository;
        _logRepository = logRepository;
        _userRepository = userRepository;
    }

    public async Task<IResultDataDto<ProjectStageDto>> Handle(AddProjectStageCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<ProjectStageDto> resultDataDto = new ResultDataDto<ProjectStageDto>();

        try
        {
            var mapperEntity = _mapper.Map<ProjectStage>(request);
            var addResult = await _projectStageRepository.AddAsync(mapperEntity);
            var resultMapper = _mapper.Map<ProjectStageDto>(addResult);
            resultDataDto.SetStatus().SetMessage("The create was successful").SetData(resultMapper);

            await AddUserLog("ProjectStage Create Handler", "ProjectStage", mapperEntity.Id, TransactionEnum.Create,
                addResult.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}