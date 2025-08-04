using RecordStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordStore.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAlbumRepository Albums { get; }
        IArtistRepository Artists { get; }
        IRepository<Genre> Genres { get; }
        Task<int> SaveChangesAsync();
    }
}
