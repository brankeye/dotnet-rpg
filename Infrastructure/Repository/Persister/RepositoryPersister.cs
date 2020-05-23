using System;
using System.Threading.Tasks;
using dotnet_rpg.Data;

namespace dotnet_rpg.Infrastructure.Repository.Persister
{
    public class RepositoryPersister : IRepositoryPersister
    {
        private readonly DataContext _dataContext;
        
        public RepositoryPersister(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public void Commit()
        {
            _dataContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _dataContext.SaveChangesAsync();
        }
        
        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dataContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}