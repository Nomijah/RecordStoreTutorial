using Microsoft.EntityFrameworkCore;
using RecordStore.Core.Interfaces;
using RecordStore.Core.Models;
using RecordStore.Infrastructure.Data;

namespace RecordStore.Infrastructure.Repositories
{
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        public ArtistRepository(RecordStoreDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Artist>> GetArtistsWithAlbumsAsync()
        {
            return await _dbSet.Include(a => a.Albums).ToListAsync();
        }

        public async Task<Artist?> GetArtistWithAlbumsAsync(int id)
        {
            return await _dbSet.Include(a => a.Albums)
                              .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Artist>> GetActiveArtistsAsync()
        {
            return await _dbSet.Where(a => a.IsActive).ToListAsync();
        }

        public async Task<IEnumerable<Artist>> GetArtistsByCountryAsync(string country)
        {
            return await _dbSet.Where(a => a.Country == country).ToListAsync();
        }
    }
}
