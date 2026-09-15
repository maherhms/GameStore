using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();
builder.AddGameStoreDb();
var app = builder.Build();
app.MapGamesEndpoints();
app.MigrateDb();

app.Run();
