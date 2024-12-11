using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.ProjectRelationCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Commands.ProjectRelationCommands.Handlers;

public class AddProjectRelationHandler : BaseCommandHandler,
    IRequestHandler<AddProjectRelationCommand, IResultDataDto<ProjectRelationDto>>
{
    private readonly IMapper _mapper;
    private readonly IProjectRelationRepository _projectRelationRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogRepository _logRepository;

    public AddProjectRelationHandler(IMapper mapper, IProjectRelationRepository projectRelationRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _projectRelationRepository = projectRelationRepository;
        _logRepository = logRepository;
        _userRepository = userRepository;
    }

    public async Task<IResultDataDto<ProjectRelationDto>> Handle(AddProjectRelationCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<ProjectRelationDto> resultDataDto = new ResultDataDto<ProjectRelationDto>();

        try
        {
            var mapperEntity = _mapper.Map<ProjectRelation>(request);
            var addResult = await _projectRelationRepository.AddAsync(mapperEntity);
            var resultMapper = _mapper.Map<ProjectRelationDto>(addResult);
            resultDataDto.SetStatus().SetMessage("The create was successful").SetData(resultMapper);

            await AddUserLog("Project Relation Create Handler", "ProjectRelation", mapperEntity.Id, TransactionEnum.Create,
                addResult.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}