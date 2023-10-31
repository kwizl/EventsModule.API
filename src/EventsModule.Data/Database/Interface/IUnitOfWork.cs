using EventsModule.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace EventsModule.Data.Database.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        EventsModuleMySQLContext _context { get; }
        
        Task CommitAsync(CancellationToken token);
        
        IDbContextTransaction BeginTransaction();
        
        Task<IDbContextTransaction> BeginTransactionAsync();

        Task<int> SaveChangesAsync(CancellationToken token);
    }
}
