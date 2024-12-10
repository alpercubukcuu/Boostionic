using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.FileEntityCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Commands.FileEntityCommands.Handlers;

public class AddFileEntityHandler : BaseCommandHandler,
    IRequestHandler<AddFileEntityCommand, IResultDataDto<FileEntityDto>>
{
    private readonly IMapper _mapper;
    private readonly IFileEntityRepository _fileEntityRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogRepository _logRepository;

    public AddFileEntityHandler(IMapper mapper, IFileEntityRepository fileEntityRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _fileEntityRepository = fileEntityRepository;
        _logRepository = logRepository;
        _userRepository = userRepository;
    }

    public async Task<IResultDataDto<FileEntityDto>> Handle(AddFileEntityCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<FileEntityDto> resultDataDto = new ResultDataDto<FileEntityDto>();

        try
        {
            var mapperEntity = _mapper.Map<FileEntity>(request);
            var addResult = await _fileEntityRepository.AddAsync(mapperEntity);
            var resultMapper = _mapper.Map<FileEntityDto>(addResult);
            resultDataDto.SetStatus().SetMessage("The create was successful").SetData(resultMapper);

            await AddUserLog("FileEntity Create Handler", "FileEntity", mapperEntity.Id, TransactionEnum.Create,
                addResult.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}