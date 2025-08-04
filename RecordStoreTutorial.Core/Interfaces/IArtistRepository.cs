using RecordStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordStore.Core.Interfaces
{
    public interface IArtistRepository : IRepository<Artist>
    {
        Task<IEnumerable<Artist>> GetArtistsWithAlbumsAsync();
        Task<Artist?> GetArtistWithAlbumsAsync(int id);
        Task<IEnumerable<Artist>> GetActiveArtistsAsync();
        Task<IEnumerable<Artist>> GetArtistsByCountryAsync(string country);
    }
}
