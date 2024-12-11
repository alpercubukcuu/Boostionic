using AutoMapper;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.ProjectRelationCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Commands.ProjectRelationCommands.Handlers;

public class DeleteProjectRelationRelationHandler : BaseCommandHandler,
    IRequestHandler<DeleteProjectRelationCommand, IResultDataDto<bool>>
{
    private readonly IMapper _mapper;
    private readonly IProjectRelationRepository _ProjectRelationRepository;
    private readonly ILogRepository _logRepository;
    private readonly IUserRepository _userRepository;

    public DeleteProjectRelationRelationHandler(IMapper mapper, IProjectRelationRepository ProjectRelationRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _ProjectRelationRepository = ProjectRelationRepository;
        _userRepository = userRepository;
        _logRepository = logRepository;
    }

    public async Task<IResultDataDto<bool>> Handle(DeleteProjectRelationCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<bool> resultDataDto = new ResultDataDto<bool>();

        try
        {
            var getData = _ProjectRelationRepository.GetSingle(predicate: x => x.Id == request.Id);
            if (getData == null)
                resultDataDto.SetStatus(false).SetErrorMessage("ProjectRelation not found")
                    .SetMessage("The ProjectRelation related to the ID value could not be found.");
            getData.IsEnable = false;
            await _ProjectRelationRepository.UpdateAsync(getData);
            await AddUserLog("ProjectRelation Delete Handler", "ProjectRelation", getData.Id, TransactionEnum.Delete,
                getData.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}