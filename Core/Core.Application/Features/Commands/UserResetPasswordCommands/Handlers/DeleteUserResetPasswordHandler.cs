using AutoMapper;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.UserCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;


namespace Core.Application.Features.Commands.UserCommands.Handlers
{
    public class DeleteUserResetPasswordHandler : BaseCommandHandler, IRequestHandler<DeleteUserResetPasswordCommand, IResultDataDto<bool>>
    {
        private readonly IMapper _mapper;
        private readonly ILogRepository _logRepository;
        private readonly IUserRepository _userRepository;
        public readonly IUserResetPasswordRepository _userResetPasswordRepository;
        public DeleteUserResetPasswordHandler(IMapper mapper, ILogRepository logRepository, IUserRepository userRepository, IUserResetPasswordRepository userResetPasswordRepository) : base(userRepository, logRepository)
        {
            _mapper = mapper;
            _logRepository = logRepository;
            _userRepository = userRepository;
            _userResetPasswordRepository = userResetPasswordRepository;
        }

        public async Task<IResultDataDto<bool>> Handle(DeleteUserResetPasswordCommand request, CancellationToken cancellationToken)
        {
            IResultDataDto<bool> result = new ResultDataDto<bool>();
            try
            {
                var getData = _userResetPasswordRepository.GetSingle(predicate: d => d.Id == request.Id);
                if (getData == null) return result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("No content found for the ID value");
                getData.IsEnable = false;
                await _userResetPasswordRepository.UpdateAsync(getData);
                await AddUserLog("UserResetPassword Delete Handler", "UserResetPassword", getData.Id, TransactionEnum.Delete, getData.Id);
            }
            catch (Exception exception) { result.SetStatus(false).SetErrorMessage(exception.Message); }
            return result;
        }
    }

}
