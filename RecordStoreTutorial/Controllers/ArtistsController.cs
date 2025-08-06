using Microsoft.AspNetCore.Mvc;
using RecordStore.Services.DTOs;
using RecordStore.Services.Interfaces;

namespace RecordStore.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        /// <summary>
        /// Get all artists
        /// </summary>
        /// <returns>List of artists</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ArtistDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ArtistDto>>> GetArtists()
        {
            var artists = await _artistService.GetAllArtistsAsync();
            return Ok(artists);
        }

        /// <summary>
        /// Get artist by ID
        /// </summary>
        /// <param name="id">Artist ID</param>
        /// <returns>Artist details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ArtistDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ArtistDto>> GetArtist(int id)
        {
            var artist = await _artistService.GetArtistByIdAsync(id);
            if (artist == null)
                return NotFound($"Artist with ID {id} not found");

            return Ok(artist);
        }

        /// <summary>
        /// Get active artists only
        /// </summary>
        /// <returns>List of active artists</returns>
        [HttpGet("active")]
        [ProducesResponseType(typeof(IEnumerable<ArtistDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ArtistDto>>> GetActiveArtists()
        {
            var artists = await _artistService.GetActiveArtistsAsync();
            return Ok(artists);
        }

        /// <summary>
        /// Create a new artist
        /// </summary>
        /// <param name="createArtistDto">Artist creation data</param>
        /// <returns>Created artist</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ArtistDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ArtistDto>> CreateArtist(CreateArtistDto createArtistDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var artist = await _artistService.CreateArtistAsync(createArtistDto);
            return CreatedAtAction(nameof(GetArtist), new { id = artist.Id }, artist);
        }
    }
}
