namespace GameStore.Api.Dtos;

/// <summary>
/// DTO returned to clients when retrieving game data.
/// This represents the *public shape* of a Game resource.
/// </summary>
public record GameDetailsDto(

    // Unique identifier for the game. Assigned by the system/database.
    int Id,

    // Name of the game. Already validated at creation time.
    string Name,

    // Genre of the game. Simple string categorization.
    int GenreId,

    // Price of the game. Decimal is used for currency-safe precision.
    decimal Price,

    // Release date. Using DateOnly avoids time zone issues.
    DateOnly ReleaseDate
);
