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

public class UpdateProjectStageStageHandler : BaseCommandHandler,
    IRequestHandler<UpdateProjectStageCommand, IResultDataDto<ProjectStageDto>>
{
    private readonly IMapper _mapper;
    private readonly IProjectStageRepository _projectStageRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogRepository _logRepository;

    public UpdateProjectStageStageHandler(IMapper mapper, IProjectStageRepository projectStageRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _projectStageRepository = projectStageRepository;
        _userRepository = userRepository;
        _logRepository = logRepository;
    }

    public async Task<IResultDataDto<ProjectStageDto>> Handle(UpdateProjectStageCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<ProjectStageDto> resultDataDto = new ResultDataDto<ProjectStageDto>();

        try
        {
            var getData = _projectStageRepository.GetSingle(predicate: x => x.Id == request.Id);

            if (getData == null)
                resultDataDto.SetStatus(false).SetErrorMessage("ProjectStage not found")
                    .SetMessage("No ProjectStage found for the ID value");

            var mappedEntity = _mapper.Map<ProjectStage>(getData);
            mappedEntity.CreatedDate = getData.CreatedDate;
            mappedEntity.Id = getData.Id;

            var addResult = await _projectStageRepository.UpdateAsync(mappedEntity);
            var resultMapper = _mapper.Map<ProjectStageDto>(addResult);

            resultDataDto.SetStatus().SetMessage("The update process was successful").SetData(resultMapper);
            await AddUserLog("ProjectStage Update Handler", "ProjectStage", mappedEntity.Id,
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