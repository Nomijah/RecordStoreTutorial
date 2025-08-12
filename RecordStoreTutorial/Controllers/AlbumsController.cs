using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecordStore.Services.DTOs;
using RecordStore.Services.Interfaces;

namespace RecordStore.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumsController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        /// <summary>
        /// Get all albums
        /// </summary>
        /// <returns>List of albums</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AlbumDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> GetAlbums()
        {
            var albums = await _albumService.GetAllAlbumsAsync();
            return Ok(albums);
        }

        /// <summary>
        /// Get album by ID
        /// </summary>
        /// <param name="id">Album ID</param>
        /// <returns>Album details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AlbumDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AlbumDto>> GetAlbum(Guid id)
        {
            var album = await _albumService.GetAlbumByIdAsync(id);
            if (album == null)
                return NotFound($"Album with ID {id} not found");

            return Ok(album);
        }

        /// <summary>
        /// Get albums by artist
        /// </summary>
        /// <param name="artistId">Artist ID</param>
        /// <returns>List of albums by the artist</returns>
        [HttpGet("artist/{artistId}")]
        [ProducesResponseType(typeof(IEnumerable<AlbumDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> GetAlbumsByArtist(int artistId)
        {
            var albums = await _albumService.GetAlbumsByArtistAsync(artistId);
            return Ok(albums);
        }

        /// <summary>
        /// Get albums by genre
        /// </summary>
        /// <param name="genreId">Genre ID</param>
        /// <returns>List of albums in the genre</returns>
        [HttpGet("genre/{genreId}")]
        [ProducesResponseType(typeof(IEnumerable<AlbumDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> GetAlbumsByGenre(int genreId)
        {
            var albums = await _albumService.GetAlbumsByGenreAsync(genreId);
            return Ok(albums);
        }

        /// <summary>
        /// Get albums in stock
        /// </summary>
        /// <returns>List of albums with stock > 0</returns>
        [HttpGet("in-stock")]
        [ProducesResponseType(typeof(IEnumerable<AlbumDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> GetAlbumsInStock()
        {
            var albums = await _albumService.GetAlbumsInStockAsync();
            return Ok(albums);
        }

        /// <summary>
        /// Get albums by price range
        /// </summary>
        /// <param name="minPrice">Minimum price</param>
        /// <param name="maxPrice">Maximum price</param>
        /// <returns>List of albums within price range</returns>
        [HttpGet("price-range")]
        [ProducesResponseType(typeof(IEnumerable<AlbumDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> GetAlbumsByPriceRange(
            [FromQuery] decimal minPrice,
            [FromQuery] decimal maxPrice)
        {
            if (minPrice < 0 || maxPrice < 0 || minPrice > maxPrice)
            {
                return BadRequest("Invalid price range. Minimum price must be less than or equal to maximum price, and both must be positive.");
            }

            var albums = await _albumService.GetAlbumsByPriceRangeAsync(minPrice, maxPrice);
            return Ok(albums);
        }

        /// <summary>
        /// Create a new album
        /// </summary>
        /// <param name="createAlbumDto">Album creation data</param>
        /// <returns>Created album</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(typeof(AlbumDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AlbumDto>> CreateAlbum(CreateAlbumDto createAlbumDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var album = await _albumService.CreateAlbumAsync(createAlbumDto);
            return CreatedAtAction(nameof(GetAlbum), new { id = album.AlbumId }, album);
        }

        /// <summary>
        /// Update an album
        /// </summary>
        /// <param name="id">Album ID</param>
        /// <param name="updateAlbumDto">Album update data</param>
        /// <returns>No content if successful</returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAlbum(Guid id, UpdateAlbumDto updateAlbumDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _albumService.UpdateAlbumAsync(id, updateAlbumDto);
            if (!updated)
                return NotFound($"Album with ID {id} not found");

            return NoContent();
        }

        /// <summary>
        /// Delete an album
        /// </summary>
        /// <param name="id">Album ID</param>
        /// <returns>No content if successful</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAlbum(Guid id)
        {
            var deleted = await _albumService.DeleteAlbumAsync(id);
            if (!deleted)
                return NotFound($"Album with ID {id} not found");

            return NoContent();
        }
    }
}
