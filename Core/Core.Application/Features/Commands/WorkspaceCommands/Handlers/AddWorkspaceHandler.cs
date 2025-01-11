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
    public class AddWorkspaceHandler : BaseCommandHandler, IRequestHandler<AddWorkspaceCommand, IResultDataDto<WorkspaceDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogRepository _logRepository;
        private readonly IUserRepository _userRepository;
        private readonly IWorkspaceRepository _workspaceRepository;

        public AddWorkspaceHandler(IMapper mapper, ILogRepository logRepository, IUserRepository userRepository, IWorkspaceRepository workspaceRepository) : base(
            userRepository, logRepository)
        {
            _mapper = mapper;
            _logRepository = logRepository;
            _userRepository = userRepository;
            _workspaceRepository = workspaceRepository;
        }

        public async Task<IResultDataDto<WorkspaceDto>> Handle(AddWorkspaceCommand request, CancellationToken cancellationToken)
        {
            IResultDataDto<WorkspaceDto> result = new ResultDataDto<WorkspaceDto>();
            try
            {
                var map = _mapper.Map<Workspace>(request);
                map.CreatedDate = DateTime.Now;
                var addResult = await _workspaceRepository.AddAsync(map);
                var resultMap = _mapper.Map<WorkspaceDto>(addResult);
                result.SetStatus(true).SetMessage("The create was successful").SetData(resultMap);

                await AddUserLog("Workspace Create Handler", "Workspace", map.Id, TransactionEnum.Create, addResult.Id);
            }
            catch (Exception exception)
            {
                result.SetStatus(false).SetErrorMessage(exception.Message);
            }

            return result;
        }
    }
}