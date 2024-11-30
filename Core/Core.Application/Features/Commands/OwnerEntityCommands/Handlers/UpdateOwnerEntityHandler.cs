using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.OwnerEntityCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Commands.OwnerEntityCommands.Handlers
{
    public class UpdateOwnerEntityHandler : BaseCommandHandler, IRequestHandler<UpdateOwnerEntityCommand, IResultDataDto<OwnerEntityDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogRepository _logRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOwnerEntityRepository _ownerEntityRepository;

        public UpdateOwnerEntityHandler(IMapper mapper, ILogRepository logRepository, IUserRepository userRepository, IOwnerEntityRepository ownerEntityRepository) : base(
            userRepository, logRepository)
        {
            _mapper = mapper;
            _logRepository = logRepository;
            _userRepository = userRepository;
            _ownerEntityRepository = ownerEntityRepository;
        }

        public async Task<IResultDataDto<OwnerEntityDto>> Handle(UpdateOwnerEntityCommand request,
            CancellationToken cancellationToken)
        {
            IResultDataDto<OwnerEntityDto> result = new ResultDataDto<OwnerEntityDto>();
            try
            {
                var getData = _ownerEntityRepository.GetSingle(predicate: p => p.Id == request.Id);
                if (getData == null)
                    return result.SetStatus(false).SetErrorMessage("Not Found Data")
                        .SetMessage("No content found for the ID value");

                var map = _mapper.Map<OwnersEntity>(request);
                map.CreatedDate = getData.CreatedDate;
                map.Id = getData.Id;

                var addResult = await _ownerEntityRepository.UpdateAsync(map);
                var resultMap = _mapper.Map<OwnerEntityDto>(addResult);

                result.SetStatus().SetMessage("The update process was successful").SetData(resultMap);
                await AddUserLog("Owner Update Handler", "OwnerEntity", map.Id, TransactionEnum.Update, getData.Id);
            }
            catch (Exception exception)
            {
                result.SetStatus(false).SetErrorMessage(exception.Message);
            }

            return result;
        }
    }
}