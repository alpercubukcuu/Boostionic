using AutoMapper;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.OwnerEntityCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;


namespace Core.Application.Features.Commands.OwnerEntityCommands.Handlers
{
    public class DeleteOwnerEntityHandler : BaseCommandHandler, IRequestHandler<DeleteOwnerEntityCommand, IResultDataDto<bool>>
    {
        private readonly IMapper _mapper;
        private readonly ILogRepository _logRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOwnerEntityRepository _ownerEntityRepository;

        public DeleteOwnerEntityHandler(IMapper mapper, ILogRepository logRepository, IUserRepository userRepository, IOwnerEntityRepository ownerEntityRepository) : base(
            userRepository, logRepository)
        {
            _mapper = mapper;
            _logRepository = logRepository;
            _userRepository = userRepository;
            _ownerEntityRepository = ownerEntityRepository;
        }

        public async Task<IResultDataDto<bool>> Handle(DeleteOwnerEntityCommand request, CancellationToken cancellationToken)
        {
            IResultDataDto<bool> result = new ResultDataDto<bool>();
            try
            {
                var getData = _ownerEntityRepository.GetSingle(predicate: d => d.Id == request.Id);
                if (getData == null) return result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("No content found for the ID value");
                getData.IsEnable = false;
                await _ownerEntityRepository.UpdateAsync(getData);
                await AddUserLog("Owner Delete Handler", "OwnerEntity", getData.Id, TransactionEnum.Delete,
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