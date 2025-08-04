using RecordStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordStore.Core.Interfaces
{
    public interface IAlbumRepository : IRepository<Album>
    {
        Task<Album?> GetByIdAsync(Guid id);
        Task<IEnumerable<Album>> GetAlbumsByArtistAsync(int artistId);
        Task<IEnumerable<Album>> GetAlbumsByGenreAsync(int genreId);
        Task<IEnumerable<Album>> GetAlbumsInStockAsync();
        Task<IEnumerable<Album>> GetAlbumsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<bool> ExistsAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}
