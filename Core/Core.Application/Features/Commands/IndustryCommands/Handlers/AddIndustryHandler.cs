using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.IndustryCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Commands.IndustryCommands.Handlers;

public class AddIndustryHandler : BaseCommandHandler,
    IRequestHandler<AddIndustryCommand, IResultDataDto<IndustryDto>>
{
    private readonly IMapper _mapper;
    private readonly IIndustryRepository _industryRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogRepository _logRepository;

    public AddIndustryHandler(IMapper mapper, IIndustryRepository industryRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _industryRepository = industryRepository;
        _logRepository = logRepository;
        _userRepository = userRepository;
    }

    public async Task<IResultDataDto<IndustryDto>> Handle(AddIndustryCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<IndustryDto> resultDataDto = new ResultDataDto<IndustryDto>();

        try
        {
            var mapperEntity = _mapper.Map<Industry>(request);
            var addResult = await _industryRepository.AddAsync(mapperEntity);
            var resultMapper = _mapper.Map<IndustryDto>(addResult);
            resultDataDto.SetStatus().SetMessage("The create was successful").SetData(resultMapper);

            await AddUserLog("Industry Create Handler", "Industry", mapperEntity.Id, TransactionEnum.Create,
                addResult.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}