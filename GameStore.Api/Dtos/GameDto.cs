namespace GameStore.Api;

// A DTO is a contract between the client and the server since it represents
// a shared agreement about how data will be transferred and used

public record GameDto(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
