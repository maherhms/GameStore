using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

/// <summary>
/// DTO used when creating a new Game resource.
/// Contains validation attributes to ensure incoming data is valid
/// before it reaches the domain or database layer.
/// </summary>
public record class CreateGameDto(

    // The name of the game. Required and limited to 50 characters.
    [Required]
    [StringLength(50)]
    string Name,

    // The genre of the game. Required and limited to 20 characters.
    [Required]
    [StringLength(20)]
    string Genre,

    // Price must be between 1 and 100. Validation happens automatically.
    [Range(1, 100)]
    decimal Price,

    // Release date of the game. Optional. DateOnly avoids time-zone issues.
    DateOnly ReleaseDate
);
