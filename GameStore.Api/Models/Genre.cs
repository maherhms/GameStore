namespace GameStore.Api.Models;

/// <summary>
/// Entity representing a game genre in the database.
/// EF Core maps this class to a 'Genres' table.
/// </summary>
public class Genre
{
    // Primary key for the Genre table.
    public int Id { get; set; }

    // Name of the genre. Required ensures no nulls.
    public required string Name { get; set; }
}