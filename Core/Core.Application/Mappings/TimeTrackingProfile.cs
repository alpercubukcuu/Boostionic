using AutoMapper;
using Core.Application.Dtos;
using Core.Domain.Entities;

namespace Core.Application.Mappings;

public class TimeTrackingProfile : Profile
{
    public TimeTrackingProfile()
    {
        CreateMap<TimeTrackingDto, TimeTracking>();
        CreateMap<TimeTracking, TimeTrackingDto>();
    }
}