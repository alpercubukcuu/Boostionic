using AutoMapper;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.BusinessPlaceCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;


namespace Core.Application.Features.Commands.BusinessPlaceCommands.Handlers
{
    public class DeleteBusinessPlaceHandler : BaseCommandHandler, IRequestHandler<DeleteBusinessPlaceCommand, IResultDataDto<bool>>
    {
        private readonly IMapper _mapper;
        private readonly IBusinessPlaceRepository _businessPlaceRepository;
        private readonly ILogRepository _logRepository;
        private readonly IUserRepository _userRepository;
        public DeleteBusinessPlaceHandler(IMapper mapper, IBusinessPlaceRepository businessPlaceRepository, ILogRepository logRepository, IUserRepository userRepository) : base(userRepository, logRepository)
        {
            _mapper = mapper;
            _businessPlaceRepository = businessPlaceRepository;
            _logRepository = logRepository;
            _userRepository = userRepository;
        }

        public async Task<IResultDataDto<bool>> Handle(DeleteBusinessPlaceCommand request, CancellationToken cancellationToken)
        {
            IResultDataDto<bool> result = new ResultDataDto<bool>();
            try
            {
                var getData = _businessPlaceRepository.GetSingle(predicate: d => d.Id == request.Id);
                if (getData == null) return result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("ID değerine ait içerik bulunamadı");
                getData.IsEnable = false;
                await _businessPlaceRepository.UpdateAsync(getData);
                await AddUserLog("BusinessPlace Delete Handler", "BusinessPlace", getData.Id, TransectionEnum.Delete, getData.UserId);
            }
            catch (Exception exception) { result.SetStatus(false).SetErrorMessage(exception.Message); }
            return result;
        }
    }

}
