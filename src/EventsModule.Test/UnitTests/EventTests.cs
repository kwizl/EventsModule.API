using AutoMapper;
using EventsModule.Data.Models;
using EventsModule.Logic.AutoMapperProfiles;
using EventsModule.Logic.Request;
using EventsModule.Logic.Response;
using FluentAssertions;

namespace EventsModule.Test.UnitTests
{
    public class EventTests
    {
        private readonly IMapper _mapper;

        public EventTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<EventMappingProfile>());
            config.AssertConfigurationIsValid();
            _mapper = config.CreateMapper();
        }

        [Theory]
        [InlineData(1, 1)]
        public void Mapping_To_Response_Is_Valid(int id, int createdbyid)
        {
            var user = new User()
            {
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                Description = "Test",
                Name = "Test",
            };

            var evt = new Event()
            {
                Title = "Hell Week",
                //CreatedBy = user,
                //CreatedByID = createdbyid,
                Description = "Sample Text",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(1),
                ID = id
            };
            var dto = _mapper.Map<EventResponse>(evt);
            dto.Should().BeEquivalentTo(evt);
        }

        [Theory]
        [InlineData("Hell Week", "Sample Text")]
        public void Mapping_From_CreateRequest_To_Model_Is_Valid(string title, string description)
        {
            var evt = new EventCreateRequest()
            {
                Title = title,
                Description = description,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(1)
            };

            var dto = _mapper.Map<Event>(evt);
            dto.Should().BeEquivalentTo(evt);
        }
    }
}
