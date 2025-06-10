using System.Threading.Tasks;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicCollection.BL.Interfaces;
using MusicCollection.Models.DTO;
using MusicCollection.Models.Models;

namespace MusicCollection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly IMusicService _musicService;
        private readonly IMapper _mapper;
        private readonly ILogger<MusicController> _logger;

        public MusicController(IMusicService musicService, IMapper mapper, ILogger<MusicController> logger){
            _musicService = musicService;
            _mapper = mapper;
            _logger = logger;
        }

    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("GetAll")]
    public async Task<IActionResult> Get()
    {
        var result = await _musicService.GetAllSongs();

        if (result == null || result.Count == 0)
        {
            return NotFound("No songs found");
        }

        return Ok(result);
    }

    [HttpGet("GetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("Id can't be null or empty");
        }

        var result = await _musicService.GetSongById(id);

        if (result == null)
        {
            return NotFound($"Song with ID:{id} not found");
        }

        return Ok(result);
    }


    [HttpPost("Add")]
    public async Task<IActionResult> Add(Song song)
    {
        try
        {
            var songDto = _mapper.Map<SongDTO>(song);

            if (songDto == null)
            {
                return BadRequest("Can't convert song to song DTO");
            }

            await _musicService.AddSong(songDto);

            return Ok();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, $"Error adding song with");
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(string id)
    {
        await _musicService.DeleteSong(id);

        return Ok();
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update(string Id,Song song)
    {
        try
        {
            var SongDto = _mapper.Map<SongDTO>(song);

            if (SongDto == null)
            {
                return BadRequest("Can't convert song to song DTO");
            }

            await _musicService.UpdateSong(Id,SongDto);

            return Ok();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, $"Error adding song with");
            return BadRequest(ex.Message);
        }
    }
    
    }
}
