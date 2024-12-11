using AutoMapper;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.ProjectCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Commands.ProjectCommands.Handlers;

public class DeleteProjectHandler : BaseCommandHandler,
    IRequestHandler<DeleteProjectCommand, IResultDataDto<bool>>
{
    private readonly IMapper _mapper;
    private readonly IProjectRepository _projectRepository;
    private readonly ILogRepository _logRepository;
    private readonly IUserRepository _userRepository;

    public DeleteProjectHandler(IMapper mapper, IProjectRepository projectRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _projectRepository = projectRepository;
        _userRepository = userRepository;
        _logRepository = logRepository;
    }

    public async Task<IResultDataDto<bool>> Handle(DeleteProjectCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<bool> resultDataDto = new ResultDataDto<bool>();

        try
        {
            var getData = _projectRepository.GetSingle(predicate: x => x.Id == request.Id);
            if (getData == null)
                resultDataDto.SetStatus(false).SetErrorMessage("Project not found")
                    .SetMessage("The Project related to the ID value could not be found.");
            getData.IsEnable = false;
            await _projectRepository.UpdateAsync(getData);
            await AddUserLog("Project Delete Handler", "Project", getData.Id, TransactionEnum.Delete,
                getData.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}