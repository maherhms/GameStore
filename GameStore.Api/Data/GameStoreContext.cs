using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

/// <summary>
/// EF Core DbContext representing the database session.
/// It defines which entity classes become database tables
/// and manages querying, saving, and tracking of data.
/// </summary>
public class GameStoreContext(DbContextOptions<GameStoreContext> options)
    : DbContext(options)
{
    // Represents the 'Games' table in the database.
    // EF Core will map the Game entity to this table.
    public DbSet<Game> Games => Set<Game>();

    // Represents the 'Genres' table in the database.
    // EF Core will map the Genre entity to this table.
    public DbSet<Genre> Genres => Set<Genre>();
}
