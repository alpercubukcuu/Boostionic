using AutoMapper;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.WorkspaceCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;


namespace Core.Application.Features.Commands.WorkspaceCommands.Handlers
{
    public class DeleteWorkspaceHandler : BaseCommandHandler, IRequestHandler<DeleteWorkspaceCommand, IResultDataDto<bool>>
    {
        private readonly IMapper _mapper;
        private readonly ILogRepository _logRepository;
        private readonly IUserRepository _userRepository;
        private readonly IWorkspaceRepository _workspaceRepository;

        public DeleteWorkspaceHandler(IMapper mapper, ILogRepository logRepository, IUserRepository userRepository, IWorkspaceRepository workspaceRepository) : base(
            userRepository, logRepository)
        {
            _mapper = mapper;
            _logRepository = logRepository;
            _userRepository = userRepository;
            _workspaceRepository = workspaceRepository;
        }

        public async Task<IResultDataDto<bool>> Handle(DeleteWorkspaceCommand request, CancellationToken cancellationToken)
        {
            IResultDataDto<bool> result = new ResultDataDto<bool>();
            try
            {
                var getData = _workspaceRepository.GetSingle(predicate: d => d.Id == request.Id);
                if (getData == null) return result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("No content found for the ID value");
                getData.IsEnable = false;
                await _workspaceRepository.UpdateAsync(getData);
                await AddUserLog("Workspace Delete Handler", "Workspace", getData.Id, TransactionEnum.Delete,
                    getData.Id);
            }
            catch (Exception exception)
            {
                result.SetStatus(false).SetErrorMessage(exception.Message);
            }

            return result;
        }
    }
}