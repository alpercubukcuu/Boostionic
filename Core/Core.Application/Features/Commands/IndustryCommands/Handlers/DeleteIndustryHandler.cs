using AutoMapper;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.IndustryCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Commands.IndustryCommands.Handlers;

public class DeleteIndustryHandler : BaseCommandHandler,
    IRequestHandler<DeleteIndustryCommand, IResultDataDto<bool>>
{
    private readonly IMapper _mapper;
    private readonly IIndustryRepository _industryRepository;
    private readonly ILogRepository _logRepository;
    private readonly IUserRepository _userRepository;

    public DeleteIndustryHandler(IMapper mapper, IIndustryRepository industryRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _industryRepository = industryRepository;
        _userRepository = userRepository;
        _logRepository = logRepository;
    }

    public async Task<IResultDataDto<bool>> Handle(DeleteIndustryCommand request, CancellationToken cancellationToken)
    {
        IResultDataDto<bool> resultDataDto = new ResultDataDto<bool>();

        try
        {
            var getData = _industryRepository.GetSingle(predicate: x => x.Id == request.Id);
            if (getData == null)
                resultDataDto.SetStatus(false).SetErrorMessage("Industry not found")
                    .SetMessage("The Industry related to the ID value could not be found.");
            getData.IsEnable = false;
            await _industryRepository.UpdateAsync(getData);
            await AddUserLog("Industry Delete Handler", "Industry", getData.Id, TransactionEnum.Delete,
                getData.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}