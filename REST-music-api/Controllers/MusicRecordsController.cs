using Microsoft.AspNetCore.Mvc;
using REST_music_api.Repository;
using REST_music_api.Model;
using Microsoft.AspNetCore.Http;

namespace REST_music_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class MusicRecordsController : ControllerBase
    {
        private readonly MusicRecordRepository _repo;

        public MusicRecordsController(MusicRecordRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
