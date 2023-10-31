using EventsModule.Data.Context;
using EventsModule.Data.Database.Interface;
using EventsModule.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsModule.Data.Database.Service
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly EventsModuleMySQLContext _context;
        protected readonly IUnitOfWork _uow;

        public Repository(EventsModuleMySQLContext context, IUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        // Add Entity
        public Task<T> Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return Task.FromResult(entity);
        }

        // Delete Entity
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        //Gets Entity by ID
        public IQueryable<T> GetByID(int ID)
        {
            return _context.Set<T>().AsNoTracking().Where(x => x.ID == ID);
        }

        public IQueryable<T> GetSet()
        {
            return _context.Set<T>().AsNoTracking();
        }

        // Update Entity
        public Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return Task.CompletedTask;
        }
    }
}
