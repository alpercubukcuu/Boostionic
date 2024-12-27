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
    public class RegisterUserCommand : UserDto, IRequest<IResultDataDto<UserDto>>
    {        
        public bool IsInvated { get; set; } = false;
    }
}
