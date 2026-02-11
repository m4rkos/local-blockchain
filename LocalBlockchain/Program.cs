using LocalBlockchain.src.database;
using LocalBlockchain.src.repository;
using LocalBlockchain.src.service;
using LocalBlockchain.src.service.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<BlockchainRepository>();
builder.Services.AddSingleton<BlockchainService>();
builder.Services.AddSingleton<Blockchain>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builderVar =>
    {
        builderVar
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// SQLite Database
builder.Services.AddDbContext<BlockchainDbContext>(options =>
    options.UseSqlite("Data Source=blockchain.db"));

var app = builder.Build();

// Adds the /openapi/v1.json endpoint to the application
app.MapOpenApi();

//app.UseHttpsRedirection();
//app.UseAuthorization();
app.UseCors("AllowAll");

app.MapControllers();

app.Run();