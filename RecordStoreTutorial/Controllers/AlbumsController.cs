using Microsoft.AspNetCore.Mvc;
using RecordStore.Services.DTOs;
using RecordStore.Services.Interfaces;

namespace RecordStore.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumsController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> GetAlbums()
        {
            var albums = await _albumService.GetAllAlbumsAsync();
            return Ok(albums);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlbumDto>> GetAlbum(Guid id)
        {
            var album = await _albumService.GetAlbumByIdAsync(id);
            if (album == null)
                return NotFound();

            return Ok(album);
        }

        [HttpGet("artist/{artistId}")]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> GetAlbumsByArtist(int artistId)
        {
            var albums = await _albumService.GetAlbumsByArtistAsync(artistId);
            return Ok(albums);
        }

        [HttpGet("genre/{genreId}")]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> GetAlbumsByGenre(int genreId)
        {
            var albums = await _albumService.GetAlbumsByGenreAsync(genreId);
            return Ok(albums);
        }

        [HttpGet("in-stock")]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> GetAlbumsInStock()
        {
            var albums = await _albumService.GetAlbumsInStockAsync();
            return Ok(albums);
        }

        [HttpGet("price-range")]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> GetAlbumsByPriceRange(
            [FromQuery] decimal minPrice,
            [FromQuery] decimal maxPrice)
        {
            var albums = await _albumService.GetAlbumsByPriceRangeAsync(minPrice, maxPrice);
            return Ok(albums);
        }

        [HttpPost]
        public async Task<ActionResult<AlbumDto>> CreateAlbum(CreateAlbumDto createAlbumDto)
        {
            var album = await _albumService.CreateAlbumAsync(createAlbumDto);
            return CreatedAtAction(nameof(GetAlbum), new { id = album.AlbumId }, album);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAlbum(Guid id, CreateAlbumDto updateAlbumDto)
        {
            var updated = await _albumService.UpdateAlbumAsync(id, updateAlbumDto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(Guid id)
        {
            var deleted = await _albumService.DeleteAlbumAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
