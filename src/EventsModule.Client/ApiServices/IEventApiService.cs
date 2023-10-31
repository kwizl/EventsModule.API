using EventsModule.Data.Models;

namespace EventsModule.Client.ApiServices
{
    public interface IEventApiService
    {
        Task<IEnumerable<Event>> GetEvents();

        Task<Event> GetEvent(int? ID);
        
        Task<Event> GetEvent(int ID);
        
        Task<Event> FindEvent(int? ID);
        
        Task<Event> FindEvent(int ID);
        
        Task<Event> CreateEvent(Event request);
        
        Task<Event> UpdateEvent(Event request);
        
        Task<Event> DeleteEvent(int? ID);
        
        Task<bool> ExistEvents(int? ID);
    }
}
