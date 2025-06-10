using Microsoft.AspNetCore.Mvc;

namespace SongGeneratorAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SongGeneratorController : ControllerBase
{
    [HttpGet(Name = "GetSong")]
    public Song Get()
    {
        Song song = new Song
        {
            Name = Guid.NewGuid().ToString(),
            Platforms = [Guid.NewGuid().ToString()],
            DateInserted = DateTime.Now,
        };
        return song;
    }  
}
