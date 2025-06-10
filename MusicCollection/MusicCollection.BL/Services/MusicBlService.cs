using MusicCollection.BL.Interfaces;
using MusicCollection.DL.Interfaces;
using MusicCollection.Models.Views;

namespace MusicCollection.BL.Services;

public class MusicBlService : IMusicBlService
{
        private readonly IMusicService _musicService;
        private readonly IPlatformRepository _platformRepository;

        public MusicBlService(IMusicService musicService, IPlatformRepository platformRepository)
        {
            _musicService = musicService;
            _platformRepository = platformRepository;
        }

        public async Task<List<SongView>> GetDetailedSongs()
        {
            var result = new List<SongView>();

            var songs = await _musicService.GetAllSongs();

            foreach (var song in songs)
            {
                var songView = new SongView
                {
                    SongId = song.Id,
                    SongName = song.Name,
                    Platforms = await _platformRepository.GetPlatformsByName(song.Platforms)
                };

                result.Add(songView);
            }

            return result;
        }
}
