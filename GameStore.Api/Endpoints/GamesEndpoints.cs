using GameStore.Api.Dtos;

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
    private static readonly List<GameDto> games =
    [
        new (1, "Street Fighter 2", "Fighting", 19.99M, new DateOnly(1992,7,15)),
        new (2, "Final Fantasy VII Rebirth", "RPG", 69.99M, new DateOnly(2024,2,29)),
        new (3, "Astro Bot", "Platformer", 59.99M, new DateOnly(2024,9,6)),
    ];

    /// <summary>
    /// Maps all /games endpoints to the WebApplication.
    /// </summary>
    public static void MapGamesEndpoints(this WebApplication app)
    {
        // Group all endpoints under /games
        var group = app.MapGroup("/games");

        // GET /games
        // Returns the full list of games.
        group.MapGet("/", () => games);

        // GET /games/{id}
        // Returns a single game by ID.
        group.MapGet("/{id}", (int id) =>
        {
            var game = games.Find(game => game.Id == id);
            return game is null ? Results.NotFound() : Results.Ok(game);
        }).WithName(GetGameEndpointName);

        // POST /games
        // Creates a new game from CreateGameDto.
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new (
                games.Count + 1,          // Auto-generate ID
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);

            // Returns 201 Created + Location header
            return Results.CreatedAtRoute(
                GetGameEndpointName,
                new { id = game.Id },
                game
            );
        });

        // PUT /games/{id}
        // Updates an existing game.
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);
            if (index == -1)
            {
                return Results.NotFound();
            }

            // Replace the existing game with updated values
            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        });

        // DELETE /games/{id}
        // Removes a game by ID.
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });
    }
}
