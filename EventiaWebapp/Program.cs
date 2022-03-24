using EventiaWebapp.Data;
using EventiaWebapp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<Database>();
builder.Services.AddScoped<EventList>();

builder.Services.AddDbContext<EventiaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EventiaDb")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();


var app = builder.Build();
app.UseStaticFiles();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<EventiaDbContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    var database = scope.ServiceProvider
        .GetRequiredService<Database>();
    await database.Seed();
    
}

app.UseRouting();


app.MapControllerRoute(name: "default", pattern: "{controller=event}/{action=index}");

app.Run();
