using Microsoft.AspNetCore.Mvc;
using REST_music_api.Repository;
using REST_music_api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace REST_music_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    
    public class MusicRecordsController : ControllerBase
    {
        private readonly MusicRecordRepository _repo;

        public MusicRecordsController(MusicRecordRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<MusicRecord> Get(
            [FromQuery] string? title = null,
            [FromQuery] string? artist = null,
            [FromQuery] int? id = null,
            [FromQuery] int? duration = null,
            [FromQuery] int? publicationYear = null,
            [FromQuery] string? sortBy = null)
        {
            return _repo.GetAll(
                title: title,
                sortBy: sortBy,
                artist: artist,
                id: id,
                duration: duration,
                publicationYear: publicationYear);
        }
    }
    
}
