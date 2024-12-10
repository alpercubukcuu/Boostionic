using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.ProjectCategoryCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Commands.ProjectCategoryCommands.Handlers;


public class UpdateProjectCategoryHandler : BaseCommandHandler,
    IRequestHandler<UpdateProjectCategoryCommand, IResultDataDto<ProjectCategoryDto>>
{
    private readonly IMapper _mapper;
    private readonly IProjectCategoryRepository _projectCategoryRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogRepository _logRepository;

    public UpdateProjectCategoryHandler(IMapper mapper, IProjectCategoryRepository projectCategoryRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _projectCategoryRepository = projectCategoryRepository;
        _userRepository = userRepository;
        _logRepository = logRepository;
    }

    public async Task<IResultDataDto<ProjectCategoryDto>> Handle(UpdateProjectCategoryCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<ProjectCategoryDto> resultDataDto = new ResultDataDto<ProjectCategoryDto>();

        try
        {
            var getData = _projectCategoryRepository.GetSingle(predicate: x => x.Id == request.Id);

            if (getData == null)
                resultDataDto.SetStatus(false).SetErrorMessage("ProjectCategory not found")
                    .SetMessage("No ProjectCategory found for the ID value");

            var mappedEntity = _mapper.Map<ProjectCategory>(getData);
            mappedEntity.CreatedDate = getData.CreatedDate;
            mappedEntity.Id = getData.Id;

            var addResult = await _projectCategoryRepository.UpdateAsync(mappedEntity);
            var resultMapper = _mapper.Map<ProjectCategoryDto>(addResult);

            resultDataDto.SetStatus().SetMessage("The update process was successful").SetData(resultMapper);
            await AddUserLog("ProjectCategory Update Handler", "ProjectCategory", mappedEntity.Id, TransactionEnum.Update,
                getData.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}