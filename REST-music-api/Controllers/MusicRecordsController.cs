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
                artist: artist,
                id: id,
                duration: duration,
                publicationYear: publicationYear,
                sortBy: sortBy);
        }

        [HttpPost]
        [ProducesResponseType(typeof(MusicRecord), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<MusicRecord> Add([FromBody] MusicRecord record)
        {
            if (string.IsNullOrWhiteSpace(record.Title) ||
                string.IsNullOrWhiteSpace(record.Artist) ||
                record.Duration <= 0 ||
                record.PublicationYear <= 0)
            {
                return BadRequest();
            }

            var added = _repo.Add(record);
            return StatusCode(StatusCodes.Status201Created, added);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            if (_repo.Delete(id) is null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(MusicRecord), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MusicRecord> Update(int id, [FromBody] MusicRecord record)
        {
            if (string.IsNullOrWhiteSpace(record.Title) ||
                string.IsNullOrWhiteSpace(record.Artist) ||
                record.Duration <= 0 ||
                record.PublicationYear <= 0)
            {
                return BadRequest();
            }

            var updated = _repo.Update(id, record);
            if (updated is null)
            {
                return NotFound();
            }

            return Ok(updated);
        }
    }
    
}
