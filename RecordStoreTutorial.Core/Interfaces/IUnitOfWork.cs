using RecordStore.Core.Models;

namespace RecordStore.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IAlbumRepository Albums { get; }
        IArtistRepository Artists { get; }
        IRepository<Genre> Genres { get; }
        Task<int> SaveChangesAsync();
    }
}
