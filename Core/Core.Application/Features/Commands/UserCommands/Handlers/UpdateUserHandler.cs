using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.BusinessPlaceCommands.Commands;
using Core.Application.Features.Commands.UserCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Commands.UserCommands.Handlers
{
    public class UpdateUserHandler : BaseCommandHandler, IRequestHandler<UpdateUserCommand, IResultDataDto<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogRepository _logRepository;
        private readonly IUserRepository _userRepository;

        public UpdateUserHandler(IMapper mapper, ILogRepository logRepository, IUserRepository userRepository) : base(
            userRepository, logRepository)
        {
            _mapper = mapper;
            _logRepository = logRepository;
            _userRepository = userRepository;
        }

        public async Task<IResultDataDto<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            IResultDataDto<UserDto> result = new ResultDataDto<UserDto>();
            try
            {
                var map = _mapper.Map<User>(request);
                map.UpdatedDate = DateTime.Now;

                var addResult = await _userRepository.UpdateAsync(map);
                var resultMap = _mapper.Map<UserDto>(addResult);

                result.SetStatus(true).SetMessage("The update process was successful").SetData(resultMap);
                await AddUserLog("User Update Handler", "User", map.Id, TransactionEnum.Update, request.Id);
            }
            catch (Exception exception)
            {
                result.SetStatus(false).SetErrorMessage(exception.Message);
            }

            return result;
        }
    }
}