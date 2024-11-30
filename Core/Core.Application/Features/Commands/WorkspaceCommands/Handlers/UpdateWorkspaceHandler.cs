using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.WorkspaceCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Commands.WorkspaceCommands.Handlers
{
    public class UpdateWorkspaceHandler : BaseCommandHandler, IRequestHandler<UpdateWorkspaceCommand, IResultDataDto<WorkspaceDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogRepository _logRepository;
        private readonly IUserRepository _userRepository;
        private readonly IWorkspaceRepository _workspaceRepository;

        public UpdateWorkspaceHandler(IMapper mapper, ILogRepository logRepository, IUserRepository userRepository, IWorkspaceRepository workspaceRepository) : base(
            userRepository, logRepository)
        {
            _mapper = mapper;
            _logRepository = logRepository;
            _userRepository = userRepository;
            _workspaceRepository = workspaceRepository;
        }

        public async Task<IResultDataDto<WorkspaceDto>> Handle(UpdateWorkspaceCommand request,
            CancellationToken cancellationToken)
        {
            IResultDataDto<WorkspaceDto> result = new ResultDataDto<WorkspaceDto>();
            try
            {
                var getData = _workspaceRepository.GetSingle(predicate: p => p.Id == request.Id);
                if (getData == null)
                    return result.SetStatus(false).SetErrorMessage("Not Found Data")
                        .SetMessage("No content found for the ID value");

                var map = _mapper.Map<Workspace>(request);
                map.CreatedDate = getData.CreatedDate;
                map.Id = getData.Id;

                var addResult = await _workspaceRepository.UpdateAsync(map);
                var resultMap = _mapper.Map<WorkspaceDto>(addResult);

                result.SetStatus().SetMessage("The update process was successful").SetData(resultMap);
                await AddUserLog("Workspace Update Handler", "Workspace", map.Id, TransactionEnum.Update, getData.Id);
            }
            catch (Exception exception)
            {
                result.SetStatus(false).SetErrorMessage(exception.Message);
            }

            return result;
        }
    }
}