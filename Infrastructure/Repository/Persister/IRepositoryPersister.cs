using System;
using System.Threading.Tasks;

namespace dotnet_rpg.Infrastructure.Repository.Persister
{
    public interface IRepositoryPersister : IDisposable
    {
        void Commit();

        Task CommitAsync();
    }
}