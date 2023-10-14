using EventsModule.API.ConfigureServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

/*
Configure Services
*/
// Configure Identity User, Roles and UserRoles
builder.ConfigureIdentity();
// Configure MySQL
builder.ConfigureMySQL();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
