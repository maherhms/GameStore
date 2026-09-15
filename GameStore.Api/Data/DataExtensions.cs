using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

/// <summary>
/// Extension methods for configuring and initializing the GameStore database.
/// These methods plug into Program.cs to keep startup code clean.
/// </summary>
public static class DataExtensions
{
    /// <summary>
    /// Applies any pending EF Core migrations at application startup.
    /// Ensures the database schema is always up-to-date.
    /// </summary>
    public static void MigrateDb(this WebApplication app)
    {
        // Create a scoped service provider so we can resolve DbContext safely.
        using var scope = app.Services.CreateScope();

        // Resolve the GameStoreContext from DI.
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        // Apply migrations (creates DB if missing, updates schema if needed).
        dbContext.Database.Migrate();
    }

    /// <summary>
    /// Registers the GameStoreContext using SQLite and seeds initial data.
    /// Called inside Program.cs during application startup.
    /// </summary>
    public static void AddGameStoreDb(this WebApplicationBuilder builder)
    {
        // SQLite connection string (file-based database).
        var connString = "Data Source=GameStore.db";

        // Register EF Core + SQLite + seeding logic.
        builder.Services.AddSqlite<GameStoreContext>(
            connString,
            optionsAction: options => options.UseSeeding((context, _) =>
            {
                // Seed initial genres only if the table is empty.
                if (!context.Set<Genre>().Any())
                {
                    context.Set<Genre>().AddRange(
                        new Genre { Name = "Fighting" },
                        new Genre { Name = "RPG" },
                        new Genre { Name = "Platformer" },
                        new Genre { Name = "Racing" },
                        new Genre { Name = "Sports" }
                    );

                    // Persist seeded data.
                    context.SaveChanges();
                }
            })
        );
    }
}
