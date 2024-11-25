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
    public class UpdateBusinessPlaceHandler : BaseCommandHandler,
        IRequestHandler<UpdateBusinessPlaceCommand, IResultDataDto<BusinessPlaceDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBusinessPlaceRepository _businessPlaceRepository;
        private readonly ILogRepository _logRepository;
        private readonly IUserRepository _userRepository;

        public UpdateBusinessPlaceHandler(IMapper mapper, IBusinessPlaceRepository businessPlaceRepository,
            ILogRepository logRepository, IUserRepository userRepository) : base(userRepository, logRepository)
        {
            _mapper = mapper;
            _businessPlaceRepository = businessPlaceRepository;
            _logRepository = logRepository;
            _userRepository = userRepository;
        }

        public async Task<IResultDataDto<BusinessPlaceDto>> Handle(UpdateBusinessPlaceCommand request,
            CancellationToken cancellationToken)
        {
            IResultDataDto<BusinessPlaceDto> result = new ResultDataDto<BusinessPlaceDto>();
            try
            {
                var getData = _businessPlaceRepository.GetSingle(predicate: p => p.Id == request.Id);
                if (getData == null)
                    return result.SetStatus(false).SetErrorMessage("Not Found Data")
                        .SetMessage("No content found for the ID value");

                var map = _mapper.Map<BusinessPlace>(request);
                map.CreatedDate = getData.CreatedDate;
                map.Id = getData.Id;

                var addResult = await _businessPlaceRepository.UpdateAsync(map);
                var resultMap = _mapper.Map<BusinessPlaceDto>(addResult);

                result.SetStatus().SetMessage("The update process was successful").SetData(resultMap);
                await AddUserLog("BusinessPlace Update Handler", "BusinessPlace", map.Id, TransactionEnum.Update,
                    getData.UserId);
            }
            catch (Exception exception)
            {
                result.SetStatus(false).SetErrorMessage(exception.Message);
            }

            return result;
        }
    }
}