using RecordStore.Core.Interfaces;
using RecordStore.Core.Models;
using RecordStore.Infrastructure.Data;

namespace RecordStore.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RecordStoreDbContext _context;
        private IAlbumRepository? _albums;
        private IArtistRepository? _artists;
        private IRepository<Genre>? _genres;

        public UnitOfWork(RecordStoreDbContext context)
        {
            _context = context;
        }

        public IAlbumRepository Albums =>
            _albums ??= new AlbumRepository(_context);

        public IArtistRepository Artists =>
            _artists ??= new ArtistRepository(_context);

        public IRepository<Genre> Genres =>
            _genres ??= new Repository<Genre>(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
