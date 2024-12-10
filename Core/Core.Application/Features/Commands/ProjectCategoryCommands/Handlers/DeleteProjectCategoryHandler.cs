using AutoMapper;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.ProjectCategoryCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Commands.ProjectCategoryCommands.Handlers;

public class DeleteProjectCategoryHandler : BaseCommandHandler,
    IRequestHandler<DeleteProjectCategoryCommand, IResultDataDto<bool>>
{
    private readonly IMapper _mapper;
    private readonly IProjectCategoryRepository _projectCategoryRepository;
    private readonly ILogRepository _logRepository;
    private readonly IUserRepository _userRepository;

    public DeleteProjectCategoryHandler(IMapper mapper, IProjectCategoryRepository projectCategoryRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _projectCategoryRepository = projectCategoryRepository;
        _userRepository = userRepository;
        _logRepository = logRepository;
    }

    public async Task<IResultDataDto<bool>> Handle(DeleteProjectCategoryCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<bool> resultDataDto = new ResultDataDto<bool>();

        try
        {
            var getData = _projectCategoryRepository.GetSingle(predicate: x => x.Id == request.Id);
            if (getData == null)
                resultDataDto.SetStatus(false).SetErrorMessage("ProjectCategory not found")
                    .SetMessage("The ProjectCategory related to the ID value could not be found.");
            getData.IsEnable = false;
            await _projectCategoryRepository.UpdateAsync(getData);
            await AddUserLog("Project Category Delete Handler", "ProjectCategory", getData.Id, TransactionEnum.Delete,
                getData.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}