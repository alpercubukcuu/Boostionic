using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.UserCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;


namespace Core.Application.Features.Commands.UserCommands.Handlers
{
    public class AddUserHandler : BaseCommandHandler, IRequestHandler<AddUserCommand, IResultDataDto<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogRepository _logRepository;
        private readonly IUserRepository _userRepository;

        public AddUserHandler(IMapper mapper, ILogRepository logRepository, IUserRepository userRepository) : base(
            userRepository, logRepository)
        {
            _mapper = mapper;
            _logRepository = logRepository;
            _userRepository = userRepository;
        }

        public async Task<IResultDataDto<UserDto>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            IResultDataDto<UserDto> result = new ResultDataDto<UserDto>();
            try
            {
                var map = _mapper.Map<User>(request);
                var addResult = await _userRepository.AddAsync(map);
                var resultMap = _mapper.Map<UserDto>(addResult);
                result.SetStatus().SetMessage("The create was successful").SetData(resultMap);

                await AddUserLog("User Create Handler", "User", map.Id, TransactionEnum.Create, addResult.Id);
            }
            catch (Exception exception)
            {
                result.SetStatus(false).SetErrorMessage(exception.Message);
            }

            return result;
        }
    }
}