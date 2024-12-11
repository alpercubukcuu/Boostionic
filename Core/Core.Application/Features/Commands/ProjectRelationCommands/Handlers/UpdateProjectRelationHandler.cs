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

public class UpdateProjectRelationHandler : BaseCommandHandler,
    IRequestHandler<UpdateProjectRelationCommand, IResultDataDto<ProjectRelationDto>>
{
    private readonly IMapper _mapper;
    private readonly IProjectRelationRepository _projectRelationRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogRepository _logRepository;

    public UpdateProjectRelationHandler(IMapper mapper, IProjectRelationRepository projectRelationRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _projectRelationRepository = projectRelationRepository;
        _userRepository = userRepository;
        _logRepository = logRepository;
    }

    public async Task<IResultDataDto<ProjectRelationDto>> Handle(UpdateProjectRelationCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<ProjectRelationDto> resultDataDto = new ResultDataDto<ProjectRelationDto>();

        try
        {
            var getData = _projectRelationRepository.GetSingle(predicate: x => x.Id == request.Id);

            if (getData == null)
                resultDataDto.SetStatus(false).SetErrorMessage("Project Relation not found")
                    .SetMessage("No Project Relation found for the ID value");

            var mappedEntity = _mapper.Map<ProjectRelation>(getData);
            mappedEntity.CreatedDate = getData.CreatedDate;
            mappedEntity.Id = getData.Id;

            var addResult = await _projectRelationRepository.UpdateAsync(mappedEntity);
            var resultMapper = _mapper.Map<ProjectRelationDto>(addResult);

            resultDataDto.SetStatus().SetMessage("The update process was successful").SetData(resultMapper);
            await AddUserLog("ProjectRelation Update Handler", "ProjectRelation", mappedEntity.Id,
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