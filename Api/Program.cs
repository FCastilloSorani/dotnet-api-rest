using Api.Extensions;
using Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCorsPolicy();

builder.Services.AddSingleton<IMessageService, MessageService>();

var app = builder.Build();

app.UseCors(CorsServiceExtension.CorsPolicy);

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.MapControllers();

app.Run();