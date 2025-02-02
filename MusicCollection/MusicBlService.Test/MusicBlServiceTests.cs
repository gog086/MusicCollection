using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using MusicCollection.BL.Interfaces;
using MusicCollection.DL.Interfaces;
using MusicCollection.Models.DTO;
using MusicCollection.Models.Models;

namespace MusicBlService.Test;

public class MusicBlServiceTests
{
        private Mock<IMusicService> _musicServiceMock;
        private Mock<IPlatformRepository> _platformRepositoryMock;

        public MusicBlServiceTests()
        {
            _musicServiceMock = new Mock<IMusicService>();
            _platformRepositoryMock = new Mock<IPlatformRepository>();
        }

        private List<SongDTO> _song = new List<SongDTO>
        {
            new SongDTO()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Song 1",
                Platforms = ["Platform 1", "Platform 2"]
            },
            new SongDTO()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Song 2",
                Platforms = ["Platform 3", "Platform 4"]
            }
        }; 

        private List<PlatformDTO> _platforms = new List<PlatformDTO>
        {
            new PlatformDTO()
            {
                Id = "157af604-7a4b-4538-b6a9-fed41a41cf3a",
                Name = "Platform 1",
                URL = "https://www.youtube.com/watch?v=dQw4w9WgXcQ"
            },
            new PlatformDTO()
            {
                Id = "baac2b19-bbd2-468d-bd3b-5bd18aba98d7",
                Name = "Platform 2",
                URL = "https://www.youtube.com/watch?v=qR-H36Vgr00"
            },
            new PlatformDTO()
            {
                Id = "5c93ba13-e803-49c1-b465-d471607e97b3",
                Name = "Platform 3",
                URL = "https://www.youtube.com/watch?v=spR6QtWBc1o"
            },
            new PlatformDTO()
            {
                Id = "9badefdc-0714-4581-80ae-161cd0a5abbe",
                Name = "Platform 4",
                URL = "https://www.youtube.com/watch?v=n4Xp6g-_UUw"
            }
        };

        [Fact]
        public void GetDetailedSongs_Ok()
        {
            //setup
            var expectedCount = 2;

            _musicServiceMock
                .Setup(x => x.GetAllSongs())
                .Returns(_song);
            _platformRepositoryMock.Setup(x => 
                    x.GetPlatformById(It.IsAny<string>()))
                .Returns((string id) =>
                    _platforms.FirstOrDefault(x => x.Id == id));

            //inject
            var musicBlService = new MusicCollection.BL.Services.MusicBlService(
                _musicServiceMock.Object,
                _platformRepositoryMock.Object);

            //Act
            var result =
                musicBlService.GetDetailedSongs();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCount, result.Count);
        }

    }