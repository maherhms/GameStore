namespace GameStore.Api.Models;

/// <summary>
/// Domain model / Entity representing a Game inside the system.
/// This is the shape stored in the database and used internally,
/// not the shape exposed to clients (DTOs handle that).
/// </summary>
public class Game
{
    // Primary key. EF Core will treat 'Id' as the default PK.
    public int Id { get; set; }

    // Name of the game. 'required' ensures the property cannot be null.
    // This is a C# 11 feature, not a validation attribute.
    public required string Name { get; set; }

    // Navigation property to the Genre entity.
    // Nullable because a game might not have a genre assigned yet.
    public Genre? Genre { get; set; }

    // Foreign key for the Genre relationship.
    // EF Core uses this to link Game → Genre.
    public int GenreId { get; set; }

    // Price stored in the database. No validation attributes here.
    public decimal Price { get; set; }

    // Release date stored without time zone issues.
    public DateOnly ReleaseDate { get; set; }
}
