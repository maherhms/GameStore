using GameStore.Api.Data;
using GameStore.Api.Endpoints;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Adds automatic validation for DTOs (Required, Range, StringLength, etc.)
builder.Services.AddValidation();

// Registers EF Core + SQLite + seeding logic (from DataExtensions)
builder.AddGameStoreDb();

builder.Services.AddAuthentication().AddJwtBearer(Options =>
{
    Options.Authority="http://localhost:8080/realms/gamestore";
    Options.Audience="gamestore-api";
    Options.RequireHttpsMetadata = false;
});

builder.Services.AddAuthorizationBuilder();

// Builds the WebApplication (DI container, middleware pipeline, etc.)
var app = builder.Build();

// Maps all /games endpoints (GET, POST, PUT, DELETE)
app.MapGamesEndpoints();
app.MapGenresEndpoints();

// Applies EF Core migrations at startup (creates DB, updates schema)
app.MigrateDb();

// Starts the web server and begins listening for HTTP requests
app.Run();
