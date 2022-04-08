using EventiaWebapp.Data;
using EventiaWebapp.Models;
using EventiaWebapp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<Database>();
builder.Services.AddScoped<EventList>();

builder.Services.AddDbContext<EventiaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EventiaDb")));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>().AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<EventiaDbContext>();


var app = builder.Build();

app.UseStaticFiles();

using (var scope = app.Services.CreateScope())
{

    var database = scope.ServiceProvider
        .GetRequiredService<Database>();

    await database.CreateAndSeed();

}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=event}/{action=index}");

app.MapRazorPages();
app.Run();
