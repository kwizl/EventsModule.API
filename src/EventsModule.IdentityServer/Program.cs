using EventsModule.IdentityServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
    .AddInMemoryClients(Config.Clients)
    .AddInMemoryApiScopes(Config.ApiScope)
    .AddDeveloperSigningCredential();
builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseRouting(); 
app.UseIdentityServer();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
