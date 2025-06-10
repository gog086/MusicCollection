using System;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using MusicCollection.DL.Interfaces;
using MusicCollection.Models.DTO;

namespace MusicCollection.BL.Services;

public class RandomSongAPIService : BackgroundService
{
    private readonly IMusicRepository _musicRepository;
    private readonly HttpClient _httpClient = new HttpClient();
    
    public RandomSongAPIService(IMusicRepository musicRepository)
    {
        _musicRepository = musicRepository;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var response = await _httpClient.GetAsync("http://127.0.0.1:5101/SongGenerator");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync(stoppingToken);
                var song = JsonSerializer.Deserialize<SongDTO>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                await _musicRepository.AddSong(song);
            }
            else
            {
                Console.WriteLine($"Failed to get new random song. Status: {response.StatusCode}");
            }

            await Task.Delay(18000, stoppingToken); 
        }
    }
}
