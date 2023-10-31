using AutoMapper;
using EventsModule.Data.Models;
using EventsModule.Logic.Request;
using EventsModule.Logic.Response;

namespace EventsModule.Logic.AutoMapperProfiles
{
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            CreateMap<Event, EventResponse>().ReverseMap();

            CreateMap<EventCreateRequest, Event>().ReverseMap();

            CreateMap<EventUpdateRequest, Event>().ReverseMap();
        }
    }
}
