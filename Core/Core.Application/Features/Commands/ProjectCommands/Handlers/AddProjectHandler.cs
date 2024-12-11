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

public class AddProjectHandler : BaseCommandHandler,
    IRequestHandler<AddProjectCommand, IResultDataDto<ProjectDto>>
{
    private readonly IMapper _mapper;
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogRepository _logRepository;

    public AddProjectHandler(IMapper mapper, IProjectRepository projectRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _projectRepository = projectRepository;
        _logRepository = logRepository;
        _userRepository = userRepository;
    }

    public async Task<IResultDataDto<ProjectDto>> Handle(AddProjectCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<ProjectDto> resultDataDto = new ResultDataDto<ProjectDto>();

        try
        {
            var mapperEntity = _mapper.Map<Project>(request);
            var addResult = await _projectRepository.AddAsync(mapperEntity);
            var resultMapper = _mapper.Map<ProjectDto>(addResult);
            resultDataDto.SetStatus().SetMessage("The create was successful").SetData(resultMapper);

            await AddUserLog("Project Create Handler", "Project", mapperEntity.Id, TransactionEnum.Create,
                addResult.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}