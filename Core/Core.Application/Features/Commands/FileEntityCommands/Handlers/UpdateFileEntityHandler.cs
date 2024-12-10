using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.ClientCommands.Handlers;
using Core.Application.Features.Commands.FileEntityCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Commands.FileEntityCommands.Handlers;

public class UpdateFileEntityHandler : BaseCommandHandler,
    IRequestHandler<UpdateFileEntityCommand, IResultDataDto<FileEntityDto>>
{
    private readonly IMapper _mapper;
    private readonly IFileEntityRepository _fileEntityRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogRepository _logRepository;

    public UpdateFileEntityHandler(IMapper mapper, IFileEntityRepository fileEntityRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _fileEntityRepository = fileEntityRepository;
        _userRepository = userRepository;
        _logRepository = logRepository;
    }

    public async Task<IResultDataDto<FileEntityDto>> Handle(UpdateFileEntityCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<FileEntityDto> resultDataDto = new ResultDataDto<FileEntityDto>();

        try
        {
            var getData = _fileEntityRepository.GetSingle(predicate: x => x.Id == request.Id);

            if (getData == null)
                resultDataDto.SetStatus(false).SetErrorMessage("File not found")
                    .SetMessage("No file found for the ID value");

            var mappedEntity = _mapper.Map<FileEntity>(getData);
            mappedEntity.CreatedDate = getData.CreatedDate;
            mappedEntity.Id = getData.Id;

            var addResult = await _fileEntityRepository.UpdateAsync(mappedEntity);
            var resultMapper = _mapper.Map<FileEntityDto>(addResult);

            resultDataDto.SetStatus().SetMessage("The update process was successful").SetData(resultMapper);
            await AddUserLog("FileEntity Update Handler", "FileEntity", mappedEntity.Id, TransactionEnum.Update,
                getData.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}