using Microsoft.EntityFrameworkCore;
using MsItem.Application;
using MsItem.Infrastructure;
using MsItem.Infrastructure.Extensions;
using MsItem.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();

// Migraciones y seed
using var scope = app.Services.CreateScope();
var logger = scope.ServiceProvider
    .GetRequiredService<ILoggerFactory>()
    .CreateLogger("MsItemStartup");

var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

logger.LogInformation("Aplicando migraciones...");
await context.Database.MigrateAsync();
logger.LogInformation("Migraciones completadas.");

await app.UseDefaultWorkItemsSeedAsync();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
