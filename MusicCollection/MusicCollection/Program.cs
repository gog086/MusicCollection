using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using MusicCollection.BL;
using MusicCollection.DL.Configurations;
using MusicCollection.HealthChecks;
using MusicCollection.Models.DTO;
using MusicCollection.Models.Models;
using MusicCollection.Validators;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(theme:AnsiConsoleTheme.Code)
    .CreateLogger();

builder.Logging.AddSerilog(logger);
// Add services to the container.

builder.Services.Configure<MongoDbConfiguration>(
    builder.Configuration.GetSection("MongoDBSettings")
);


builder.Services
    .RegisterDataLayer()
    .RegisterBusinessLayer();

TypeAdapterConfig<Song, SongDTO>.NewConfig().TwoWays();
builder.Services.AddSingleton<IMapper, Mapper>();


builder.Services
    .AddValidatorsFromAssemblyContaining<AddSongRequestValidator>();
builder.Services.AddFluentValidationAutoValidation();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHealthChecks()
    .AddCheck<SampleHealthCheck>("Sample");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/Sample");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
