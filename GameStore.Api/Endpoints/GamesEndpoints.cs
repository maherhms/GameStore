using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

/// <summary>
/// Defines all HTTP endpoints for the /games route.
/// Uses Minimal APIs to map routes directly to handlers.
/// </summary>
public static class GamesEndpoints
{
    // Name used for route generation (CreatedAtRoute)
    const string GetGameEndpointName = "GameGame";

    // In-memory list acting as a temporary data store.
    // Only for demo purposes — not persistent.
    // private static readonly List<GameSummaryDto> games =
    // [
    //     new (1, "Street Fighter 2", "Fighting", 19.99M, new DateOnly(1992,7,15)),
    //     new (2, "Final Fantasy VII Rebirth", "RPG", 69.99M, new DateOnly(2024,2,29)),
    //     new (3, "Astro Bot", "Platformer", 59.99M, new DateOnly(2024,9,6)),
    // ];

    /// <summary>
    /// Maps all /games endpoints to the WebApplication.
    /// </summary>
    public static void MapGamesEndpoints(this WebApplication app)
    {
        // Group all endpoints under /games
        var group = app.MapGroup("/games");

        // GET /games
        // Returns the full list of games.
        group.MapGet("/", async (GameStoreContext dbContext) => 
        await dbContext.Games
            .Include(game => game.Genre)
            .Select(game => new GameSummaryDto(
            game.Id,
            game.Name,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate
            // async list without waiting for response since we arent making 
            // any changes after retrievale
        )).AsNoTracking().ToListAsync());

        // GET /games/{id}
        // Returns a single game by ID.
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            var game = await dbContext.Games.FindAsync(id);

            return game is null ? Results.NotFound() : Results.Ok(
                new GameDetailsDto(
                    game.Id,
                    game.Name,
                    game.GenreId,
                    game.Price,
                    game.ReleaseDate
                )
            );
        }).WithName(GetGameEndpointName);

        // POST /games
        // Creates a new game from CreateGameDto.
        group.MapPost("/", async (CreateGameDto newGame ,GameStoreContext dbContext) =>
        {
            Game game = new()
            {
                Name = newGame.Name,
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };

            // tell ef core to track that a new game needs to be inserted into
            dbContext.Games.Add(game);
            // we actually save the track pending changes
            await dbContext.SaveChangesAsync();

            GameDetailsDto gameDto = new(
                // we can pass id because game id has been already generated previously on creating new Game
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate
            );
            // Returns 201 Created + Location header
            return Results.CreatedAtRoute( GetGameEndpointName, new { id = gameDto.Id },gameDto );
        });

        // PUT /games/{id}
        // Updates an existing game.
        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame , GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);
            if (existingGame is null)
            {
                return Results.NotFound();
            }

            // Replace the existing game with updated values
            existingGame.Name = updatedGame.Name;
            existingGame.GenreId = updatedGame.GenreId;
            existingGame.Price = updatedGame.Price;
            existingGame.ReleaseDate = updatedGame.ReleaseDate;

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // DELETE /games/{id}
        // Removes a game by ID.
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();
            return Results.NoContent();
        });
    }
}
