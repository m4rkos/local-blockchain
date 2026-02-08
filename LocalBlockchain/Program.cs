using LocalBlockchain.service;
using LocalBlockchain.service.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
//builder.Services.AddOpenApi();

builder.Services.AddSingleton<BlockchainService>();
builder.Services.AddSingleton<Blockchain>();

var app = builder.Build();

//app.UseHttpsRedirection();
//app.UseAuthorization();

app.MapControllers();

app.Run();