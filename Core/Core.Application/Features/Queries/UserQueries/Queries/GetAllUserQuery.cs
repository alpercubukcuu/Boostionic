﻿using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;


namespace Core.Application.Features.Queries.UserQueries.Queries
{
    public class GetAllUserQuery : IRequest<IResultDataDto<List<UserDto>>>
    {
    }
}
