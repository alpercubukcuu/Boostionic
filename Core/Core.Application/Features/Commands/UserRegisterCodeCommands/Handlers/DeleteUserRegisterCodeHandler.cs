using AutoMapper;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.UserCommands.Commands;
using Core.Application.Features.Commands.UserRegisterCodeCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;


namespace Core.Application.Features.Commands.UserRegisterCodeCommands.Handlers
{
    public class DeleteUserRegisterCodeHandler : BaseCommandHandler, IRequestHandler<DeleteUserRegisterCodeCommand, IResultDataDto<bool>>
    {
        private readonly IMapper _mapper;
        private readonly ILogRepository _logRepository;
        private readonly IUserRepository _userRepository;
        public readonly IUserRegisterCodeRepository _userRegisterCodeRepository;
        public DeleteUserRegisterCodeHandler(IMapper mapper, ILogRepository logRepository, IUserRepository userRepository, IUserRegisterCodeRepository userRegisterCodeRepository) : base(userRepository, logRepository)
        {
            _mapper = mapper;
            _logRepository = logRepository;
            _userRepository = userRepository;
            _userRegisterCodeRepository = userRegisterCodeRepository;
        }

        public async Task<IResultDataDto<bool>> Handle(DeleteUserRegisterCodeCommand request, CancellationToken cancellationToken)
        {
            IResultDataDto<bool> result = new ResultDataDto<bool>();
            try
            {
                var getData = _userRegisterCodeRepository.GetSingle(predicate: d => d.Id == request.Id);
                if (getData == null) return result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("No content found for the ID value");
                getData.IsEnable = false;
                await _userRegisterCodeRepository.UpdateAsync(getData);
                await AddUserLog("UserRegisterCode Delete Handler", "UserRegisterCode", getData.Id, TransactionEnum.Delete, getData.Id);
            }
            catch (Exception exception) { result.SetStatus(false).SetErrorMessage(exception.Message); }
            return result;
        }
    }

}
