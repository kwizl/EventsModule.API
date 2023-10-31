using EventsModule.Data.Models;

namespace EventsModule.Data.Database.Interface
{
    public interface IRepository<T>
    {
        Task<T> Add(T entity);

        void Delete(T entity);

        IQueryable<T> GetByID(int id);

        IQueryable<T> GetSet();

        Task Update(T entity);
    }
}
