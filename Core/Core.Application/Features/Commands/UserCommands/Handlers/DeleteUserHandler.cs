using AutoMapper;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.UserCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;


namespace Core.Application.Features.Commands.UserCommands.Handlers
{
    public class DeleteUserHandler : BaseCommandHandler, IRequestHandler<DeleteUserCommand, IResultDataDto<bool>>
    {
        private readonly IMapper _mapper;
        private readonly ILogRepository _logRepository;
        private readonly IUserRepository _userRepository;
        public DeleteUserHandler(IMapper mapper,  ILogRepository logRepository, IUserRepository userRepository) : base(userRepository, logRepository)
        {
            _mapper = mapper;
            _logRepository = logRepository;
            _userRepository = userRepository;
        }

        public async Task<IResultDataDto<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            IResultDataDto<bool> result = new ResultDataDto<bool>();
            try
            {
                var getData = _userRepository.GetSingle(predicate: d => d.Id == request.Id);
                if (getData == null) return result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("ID değerine ait içerik bulunamadı");
                getData.IsEnable = false;
                await _userRepository.UpdateAsync(getData);
                await AddUserLog("User Delete Handler", "BusinessPlace", getData.Id, TransectionEnum.Delete, getData.Id);
            }
            catch (Exception exception) { result.SetStatus(false).SetErrorMessage(exception.Message); }
            return result;
        }
    }

}
