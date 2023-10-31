using EventsModule.Data.Database.Interface;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Transactions;

namespace EventsModule.Data.Context.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        public EventsModuleMySQLContext _context { get; }

        public UnitOfWork(EventsModuleMySQLContext context)
        {
            _context = context;
        }

        // Persist data using transactions
        public Task CommitAsync(CancellationToken token)
        {
            var transaction = _context.Database.CurrentTransaction;
            if (transaction is null)
                throw new InvalidOperationException("A transaction has not been started.");

            try
            {
                transaction.CommitAsync();
                transaction.Dispose();
                transaction = null!;
            }
            catch (Exception ex)
            {
                if (transaction is not null)
                    transaction.Rollback();

                throw new Exception(ex.Message);
            }

            _context.SaveChangesAsync(token);
            return Task.CompletedTask;
        }

        // Begins transaction when interacting with databbase
        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        // Begins transaction when interacting with databbase
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        // Releases db resources
        public void Dispose()
        {
            var transaction = _context.Database.CurrentTransaction;
            _context.Dispose();
            transaction?.Dispose();
        }

        // Save Changes
        public Task<int> SaveChangesAsync(CancellationToken token)
        {
            return _context.SaveChangesAsync(token);
        }
    }
}
