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
        public IEnumerable<MusicRecord> Get([FromQuery] string title )
        {
            return _repo.GetAll(title);
        }
    }
    
}
