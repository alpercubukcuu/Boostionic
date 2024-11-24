using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.BusinessPlaceCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;


namespace Core.Application.Features.Commands.BusinessPlaceCommands.Handlers
{   
    public class AddBusinessPlaceHandler : BaseCommandHandler, IRequestHandler<AddBusinessPlaceCommand, IResultDataDto<BusinessPlaceDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBusinessPlaceRepository _businessPlaceRepository;
        private readonly ILogRepository _logRepository;
        private readonly IUserRepository _userRepository;

        public AddBusinessPlaceHandler(IMapper mapper, IBusinessPlaceRepository businessPlaceRepository, ILogRepository logRepository, IUserRepository userRepository) : base(userRepository, logRepository)
        {
            _mapper = mapper;
            _businessPlaceRepository = businessPlaceRepository;
            _logRepository = logRepository;
            _userRepository = userRepository;
        }
        public async Task<IResultDataDto<BusinessPlaceDto>> Handle(AddBusinessPlaceCommand request, CancellationToken cancellationToken)
        {
            IResultDataDto<BusinessPlaceDto> result = new ResultDataDto<BusinessPlaceDto>();
            try
            {
                var map = _mapper.Map<BusinessPlace>(request);
                var addResult = await _businessPlaceRepository.AddAsync(map);
                var resultMap = _mapper.Map<BusinessPlaceDto>(addResult);
                result.SetStatus().SetMessage("The create was successful").SetData(resultMap);

                await AddUserLog("BusinessPlace Create Handler", "BusinessPlace", map.Id, TransectionEnum.Create, addResult.UserId);
            }
            catch (Exception exception) { result.SetStatus(false).SetErrorMessage(exception.Message); }
            return result;
        }

    }
}
