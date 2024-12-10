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

public class UpdateIndustryHandler : BaseCommandHandler,
    IRequestHandler<UpdateIndustryCommand, IResultDataDto<IndustryDto>>
{
    private readonly IMapper _mapper;
    private readonly IIndustryRepository _industryRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogRepository _logRepository;

    public UpdateIndustryHandler(IMapper mapper, IIndustryRepository industryRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _industryRepository = industryRepository;
        _userRepository = userRepository;
        _logRepository = logRepository;
    }

    public async Task<IResultDataDto<IndustryDto>> Handle(UpdateIndustryCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<IndustryDto> resultDataDto = new ResultDataDto<IndustryDto>();

        try
        {
            var getData = _industryRepository.GetSingle(predicate: x => x.Id == request.Id);

            if (getData == null)
                resultDataDto.SetStatus(false).SetErrorMessage("Industry not found")
                    .SetMessage("No Industry found for the ID value");

            var mappedEntity = _mapper.Map<Industry>(getData);
            mappedEntity.CreatedDate = getData.CreatedDate;
            mappedEntity.Id = getData.Id;

            var addResult = await _industryRepository.UpdateAsync(mappedEntity);
            var resultMapper = _mapper.Map<IndustryDto>(addResult);

            resultDataDto.SetStatus().SetMessage("The update process was successful").SetData(resultMapper);
            await AddUserLog("Industry Update Handler", "Industry", mappedEntity.Id, TransactionEnum.Update,
                getData.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}