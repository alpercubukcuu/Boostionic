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
        public readonly IUserRoleRepository _userRoleRepository;

        public AddUserHandler(IMapper mapper, ILogRepository logRepository, IUserRepository userRepository, IUserRoleRepository userRoleRepository) : base(
            userRepository, logRepository)
        {
            _mapper = mapper;
            _logRepository = logRepository;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<IResultDataDto<UserDto>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            IResultDataDto<UserDto> result = new ResultDataDto<UserDto>();
            try
            {
                if (!request.IsInvated)
                {
                    var map = _mapper.Map<User>(request);
                    map.UserRoleId = _userRoleRepository.GetSingle(predicate: u => u.IsEnable == true && u.RoleName == "Owner").Id;
                    map.IsOwner = true;
                    map.UserType = Convert.ToByte(UserTypeEnum.Owner);
                    map.CreatedDate = DateTime.Now;

                    var addResult = await _userRepository.AddAsync(map);
                    var resultMap = _mapper.Map<UserDto>(addResult);
                    result.SetStatus().SetMessage("The create was successful").SetData(resultMap);
                    await AddUserLog("User Create Handler", "User", map.Id, TransactionEnum.Create, addResult.Id);
                }
                else
                {
                    // We have to write code for invated people
                }

               
            }
            catch (Exception exception)
            {
                result.SetStatus(false).SetErrorMessage(exception.Message);
            }

            return result;
        }
    }
}