using Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCorsPolicy();

var app = builder.Build();

app.UseCors(CorsServiceExtension.CorsPolicy);

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.MapGet("/", () => "Hello world from a .NET API!");

app.Run();