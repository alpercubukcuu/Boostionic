using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Commands.UserCommands.Commands
{
    public class UserLoginCommand : UserDto, IRequest<IResultDataDto<UserDto>>
    {
    }
}
