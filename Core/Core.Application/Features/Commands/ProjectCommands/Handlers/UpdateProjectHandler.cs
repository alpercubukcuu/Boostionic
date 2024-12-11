using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.ProjectCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Commands.ProjectCommands.Handlers;

public class UpdateProjectHandler : BaseCommandHandler,
    IRequestHandler<UpdateProjectCommand, IResultDataDto<ProjectDto>>
{
    private readonly IMapper _mapper;
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogRepository _logRepository;

    public UpdateProjectHandler(IMapper mapper, IProjectRepository projectRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _projectRepository = projectRepository;
        _userRepository = userRepository;
        _logRepository = logRepository;
    }

    public async Task<IResultDataDto<ProjectDto>> Handle(UpdateProjectCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<ProjectDto> resultDataDto = new ResultDataDto<ProjectDto>();

        try
        {
            var getData = _projectRepository.GetSingle(predicate: x => x.Id == request.Id);

            if (getData == null)
                resultDataDto.SetStatus(false).SetErrorMessage("Project not found")
                    .SetMessage("No Project found for the ID value");

            var mappedEntity = _mapper.Map<Project>(getData);
            mappedEntity.CreatedDate = getData.CreatedDate;
            mappedEntity.Id = getData.Id;

            var addResult = await _projectRepository.UpdateAsync(mappedEntity);
            var resultMapper = _mapper.Map<ProjectDto>(addResult);

            resultDataDto.SetStatus().SetMessage("The update process was successful").SetData(resultMapper);
            await AddUserLog("Project Update Handler", "Project", mappedEntity.Id,
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