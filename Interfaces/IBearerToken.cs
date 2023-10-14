using EventsModule.Data.Models;

namespace EventsModule.API.Interfaces
{
    public interface IBearerToken
    {
        string CreateToken(User user);
    }
}
