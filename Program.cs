using EventsModule.API.ConfigureServices;
using EventsModule.API.HostService;
using EventsModule.Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configure Services
builder.ConfigureMySQL();
builder.ConfigureAutoMapper();
builder.ConfigureDependencyInjection();
builder.ConfigureMediatR();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MigrateDatabase<EventsModuleMySQLContext>();
app.Run();
