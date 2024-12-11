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

public class AddProjectCategoryHandler : BaseCommandHandler,
    IRequestHandler<AddProjectCategoryCommand, IResultDataDto<ProjectCategoryDto>>
{
    private readonly IMapper _mapper;
    private readonly IProjectCategoryRepository _projectCategoryRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogRepository _logRepository;

    public AddProjectCategoryHandler(IMapper mapper, IProjectCategoryRepository projectCategoryRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _projectCategoryRepository = projectCategoryRepository;
        _logRepository = logRepository;
        _userRepository = userRepository;
    }

    public async Task<IResultDataDto<ProjectCategoryDto>> Handle(AddProjectCategoryCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<ProjectCategoryDto> resultDataDto = new ResultDataDto<ProjectCategoryDto>();

        try
        {
            var mapperEntity = _mapper.Map<ProjectCategory>(request);
            var addResult = await _projectCategoryRepository.AddAsync(mapperEntity);
            var resultMapper = _mapper.Map<ProjectCategoryDto>(addResult);
            resultDataDto.SetStatus().SetMessage("The create was successful").SetData(resultMapper);

            await AddUserLog("ProjectCategory Create Handler", "ProjectCategory", mapperEntity.Id,
                TransactionEnum.Create,
                addResult.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}