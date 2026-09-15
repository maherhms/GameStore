using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

/// <summary>
/// DTO used when updating an existing Game resource.
/// Contains validation rules to ensure incoming update data is valid.
/// </summary>
public record UpdateGameDto(

    // Updated name of the game. Required and limited to 50 characters.
    [Required]
    [StringLength(50)]
    string Name,

    // Updated genre. Required and limited to 20 characters.
    [Required]
    [StringLength(20)]
    string Genre,

    // Updated price. Must be between 1 and 100.
    [Range(1, 100)]
    decimal Price,

    // Updated release date. Optional. DateOnly avoids time-zone issues.
    DateOnly ReleaseDate
);
