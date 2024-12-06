using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.OwnerEntityCommands.Commands;
using Core.Application.Features.Commands.UserCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;


namespace Core.Application.Features.Commands.OwnerEntityCommands.Handlers
{
    public class AddOwnerEntityHandler : BaseCommandHandler, IRequestHandler<AddOwnerEntityCommand, IResultDataDto<OwnerEntityDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogRepository _logRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOwnerEntityRepository _ownerEntityRepository;

        public AddOwnerEntityHandler(IMapper mapper, ILogRepository logRepository, IUserRepository userRepository, IOwnerEntityRepository ownerEntityRepository) : base(
            userRepository, logRepository)
        {
            _mapper = mapper;
            _logRepository = logRepository;
            _userRepository = userRepository;
            _ownerEntityRepository = ownerEntityRepository;
        }

        public async Task<IResultDataDto<OwnerEntityDto>> Handle(AddOwnerEntityCommand request, CancellationToken cancellationToken)
        {
            IResultDataDto<OwnerEntityDto> result = new ResultDataDto<OwnerEntityDto>();
            try
            {
                var map = _mapper.Map<OwnerEntity>(request);
                map.CompanyOwnerTitle = request.OwnerTitle + " " +"Owner";                
                map.CreatedDate = DateTime.Now;

                var addResult = await _ownerEntityRepository.AddAsync(map);
                var resultMap = _mapper.Map<OwnerEntityDto>(addResult);
                result.SetStatus().SetMessage("The create was successful").SetData(resultMap);

                await AddUserLog("Owner Create Handler", "OwnerEntity", map.Id, TransactionEnum.Create, addResult.Id);
            }
            catch (Exception exception)
            {
                result.SetStatus(false).SetErrorMessage(exception.Message);
            }

            return result;
        }
    }
}