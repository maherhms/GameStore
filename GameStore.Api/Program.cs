using GameStore.Api;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games = [
    new (
        1, 
        "Street Fighter 2", "Fighting" ,
        19.99M ,
        new DateOnly(1992,7,15)
        ),
    new (
        2, 
        "Final Fantasy VII Rebirth",
        "RPG" ,
        69.99M ,
        new DateOnly(2024,2,29)
        ),
    new (
        3, 
        "Astro Bot",
        "Platformer" , 
        59.99M , 
        new DateOnly(2024,9,6)
        ),
];

// GET /games
app.MapGet("/games", () => games);
// GET /games/id
app.MapGet("games/{id}" , (int id) => games.Find(game => game.Id == id));

app.Run();
