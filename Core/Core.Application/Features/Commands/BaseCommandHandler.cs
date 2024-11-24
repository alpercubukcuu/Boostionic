using Core.Application.Enums;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;


namespace Core.Application.Features.Commands
{
    public class BaseCommandHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogRepository _logRepository;

        public BaseCommandHandler(IUserRepository userRepository, ILogRepository logRepository)
        {
            _userRepository = userRepository;
            _logRepository = logRepository;
        }

        public async Task<bool> AddUserLog(string title, string entity, Guid dataId, TransectionEnum transectionType, Guid adminId)
        {
            try
            {
                var user = await _userRepository.GetSingleAsync(predicate: d => d.Id == adminId);
                if (user == null) return false;

                var Log = new Log
                {
                    Title = title,
                    DataId = dataId,
                    Entity = entity,
                    Description = $"{user.Name} {user.SurName} named user has done this operation.",
                    TransectionType = (byte)transectionType,
                    UserId = user.Id,
                    Id = Guid.NewGuid(),
                    IsEnable = true,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };
                await _logRepository.AddAsync(Log);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
