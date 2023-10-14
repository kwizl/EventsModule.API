using EventsModule.API.ConfigureServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure Services
builder.ConfigureIdentity();

builder.ConfigureMySQL();

builder.ConfigureAutoMapper();

builder.ConfigureBearerToken();

builder.ConfigureAuthentication();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
