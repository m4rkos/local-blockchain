using LocalBlockchain.service;
using LocalBlockchain.service.Models;

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

// Adds the /openapi/v1.json endpoint to the application
app.MapOpenApi();

//app.UseHttpsRedirection();
//app.UseAuthorization();
app.UseCors("AllowAll");

app.MapControllers();

app.Run();