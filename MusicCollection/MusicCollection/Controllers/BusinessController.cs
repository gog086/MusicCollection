using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicCollection.BL.Interfaces;
using MusicCollection.Models.DTO;

namespace MusicCollection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly IMusicBlService _musicService;
        private readonly IPlatformService _platformService;

        public BusinessController(IMusicBlService musicService, IPlatformService platformService){
            _musicService = musicService;
            _platformService = platformService;
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetAllSongsWithDetails")]
        public IActionResult GetAllSongsWithDetails()
        {
            var result = _musicService.GetDetailedSongs();

            if (result == null || result.Count == 0)
            {
                return NotFound("No songs found");
            }

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("AddPlatform")]
        public IActionResult AddPlatform([FromBody] PlatformDTO platform)
        {
            _platformService.AddPlatform(platform);

            return Ok();
        }
    }
}
