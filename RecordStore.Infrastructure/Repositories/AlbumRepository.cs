using Microsoft.EntityFrameworkCore;
using RecordStore.Core.Interfaces;
using RecordStore.Core.Models;
using RecordStore.Infrastructure.Data;

namespace RecordStore.Infrastructure.Repositories
{
    public class AlbumRepository : Repository<Album>, IAlbumRepository
    {
        public AlbumRepository(RecordStoreDbContext context) : base(context)
        {
        }

        public async Task<Album?> GetByIdAsync(Guid id)
        {
            return await _dbSet.Include(a => a.Artist)
                              .Include(a => a.Genre)
                              .FirstOrDefaultAsync(a => a.AlbumId == id);
        }

        public async Task<IEnumerable<Album>> GetAlbumsByArtistAsync(int artistId)
        {
            return await _dbSet.Include(a => a.Artist)
                              .Include(a => a.Genre)
                              .Where(a => a.ArtistId == artistId)
                              .ToListAsync();
        }

        public async Task<IEnumerable<Album>> GetAlbumsByGenreAsync(int genreId)
        {
            return await _dbSet.Include(a => a.Artist)
                              .Include(a => a.Genre)
                              .Where(a => a.GenreId == genreId)
                              .ToListAsync();
        }

        public async Task<IEnumerable<Album>> GetAlbumsInStockAsync()
        {
            return await _dbSet.Include(a => a.Artist)
                              .Include(a => a.Genre)
                              .Where(a => a.Stock > 0)
                              .ToListAsync();
        }

        public async Task<IEnumerable<Album>> GetAlbumsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _dbSet.Include(a => a.Artist)
                              .Include(a => a.Genre)
                              .Where(a => a.Price >= minPrice && a.Price <= maxPrice)
                              .ToListAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbSet.AnyAsync(a => a.AlbumId == id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var album = await _dbSet.FirstOrDefaultAsync(a => a.AlbumId == id);
            if (album != null)
            {
                _dbSet.Remove(album);
            }
        }

        public override async Task<IEnumerable<Album>> GetAllAsync()
        {
            return await _dbSet.Include(a => a.Artist)
                              .Include(a => a.Genre)
                              .ToListAsync();
        }
    }
}
