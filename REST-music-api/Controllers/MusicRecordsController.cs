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
        private readonly SqlMusicRecordRepository _repo;

        public MusicRecordsController(SqlMusicRecordRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<MusicRecord>>> Get(
            [FromQuery] string? title = null,
            [FromQuery] string? artist = null,
            [FromQuery] int? id = null,
            [FromQuery] int? duration = null,
            [FromQuery] int? publicationYear = null,
            [FromQuery] string? sortBy = null)
        {
            var records = await _repo.GetAllAsync(
                title: title,
                artist: artist,
                id: id,
                duration: duration,
                publicationYear: publicationYear,
                sortBy: sortBy);
            return Ok(records);
        }

        [HttpPost]
        [ProducesResponseType(typeof(MusicRecord), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MusicRecord>> Add([FromBody] MusicRecord record)
        {
            if (string.IsNullOrWhiteSpace(record.Title) ||
                string.IsNullOrWhiteSpace(record.Artist) ||
                record.Duration <= 0 ||
                record.PublicationYear <= 0)
            {
                return BadRequest();
            }

            var added = await _repo.AddAsync(record);
            return StatusCode(StatusCodes.Status201Created, added);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _repo.DeleteAsync(id) is null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(MusicRecord), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MusicRecord>> Update(int id, [FromBody] MusicRecord record)
        {
            if (string.IsNullOrWhiteSpace(record.Title) ||
                string.IsNullOrWhiteSpace(record.Artist) ||
                record.Duration <= 0 ||
                record.PublicationYear <= 0)
            {
                return BadRequest();
            }

            var updated = await _repo.UpdateAsync(id, record);
            if (updated is null)
            {
                return NotFound();
            }

            return Ok(updated);
        }
    }   
}
