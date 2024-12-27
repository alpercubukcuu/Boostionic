using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.UserCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Core.Application.Features.Commands.UserCommands.Handlers
{
    public class RegisterUserHandler : BaseCommandHandler, IRequestHandler<RegisterUserCommand, IResultDataDto<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogRepository _logRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOwnerEntityRepository _ownerEntityRepository;
        private readonly IWorkspaceRepository _workspaceRepository;
        public readonly IUserRoleRepository _userRoleRepository;
        public RegisterUserHandler(IMapper mapper, ILogRepository logRepository, IUserRepository userRepository, IOwnerEntityRepository ownerEntityRepository, IWorkspaceRepository workspaceRepository, IUserRoleRepository userRoleRepository) : base(
            userRepository, logRepository)
        {
            _mapper = mapper;
            _logRepository = logRepository;
            _userRepository = userRepository;
            _ownerEntityRepository = ownerEntityRepository;
            _workspaceRepository = workspaceRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<IResultDataDto<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            IResultDataDto<UserDto> result = new ResultDataDto<UserDto>();
            OwnerEntity ownerEntity = new();
            try
            {

                var userResult = _userRepository.GetSingle(predicate: d => d.Email == request.Email);
                if (userResult != null) return result.SetStatus(false).SetErrorMessage("You have account please try to login.").SetMessage("You have account please try to login.");
    
                var map = _mapper.Map<User>(request);
                string ownerTitle = map.Name + map.SurName + "Owner";

                ownerEntity.CreatedDate = request.CreatedDate;
                ownerEntity.UpdatedDate = request.UpdatedDate;
                ownerEntity.CompanyOwnerTitle = ownerTitle;
                ownerEntity.Id = Guid.NewGuid();
                ownerEntity.IsEnable = true;

                var ownerResult = await _ownerEntityRepository.AddAsync(ownerEntity);

                map.OwnerId = ownerResult.Id;
                map.IsEnable = true;
                map.UserRoleId = _userRoleRepository.GetSingle(predicate: u => u.IsEnable == true && u.RoleName == "Owner").Id;
                map.CreatedDate = DateTime.Now;

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