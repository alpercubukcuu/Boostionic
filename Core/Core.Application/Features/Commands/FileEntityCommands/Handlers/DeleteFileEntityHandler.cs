using AutoMapper;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.FileEntityCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Commands.FileEntityCommands.Handlers;

public class DeleteFileEntityHandler : BaseCommandHandler,
    IRequestHandler<DeleteFileEntityCommand, IResultDataDto<bool>>
{
    private readonly IMapper _mapper;
    private readonly IFileEntityRepository _fileEntityRepository;
    private readonly ILogRepository _logRepository;
    private readonly IUserRepository _userRepository;

    public DeleteFileEntityHandler(IMapper mapper, IFileEntityRepository fileEntityRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _fileEntityRepository = fileEntityRepository;
        _userRepository = userRepository;
        _logRepository = logRepository;
    }

    public async Task<IResultDataDto<bool>> Handle(DeleteFileEntityCommand request, CancellationToken cancellationToken)
    {
        IResultDataDto<bool> resultDataDto = new ResultDataDto<bool>();

        try
        {
            var getData = _fileEntityRepository.GetSingle(predicate: x => x.Id == request.Id);
            if (getData == null)
                resultDataDto.SetStatus(false).SetErrorMessage("File not found")
                    .SetMessage("The file related to the ID value could not be found.");
            getData.IsEnable = false;
            await _fileEntityRepository.UpdateAsync(getData);
            await AddUserLog("File Delete Handler", "FileEntity", getData.Id, TransactionEnum.Delete,
                getData.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}