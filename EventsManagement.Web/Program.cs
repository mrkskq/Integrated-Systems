using EventsManagement.Repository;
using EventsManagement.Repository.Implementation;
using EventsManagement.Repository.Interface;
using EventsManagement.Service.Implementation;
using EventsManagement.Service.Interface;
using EventsManagement.Web.Interceptor;
using EvolveDb;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
//using EventsManagement.Web.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

/*
builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
    {
        options.UseSqlServer(connectionString);
        options.UseLazyLoadingProxies();
        sp.GetService<AuditInterceptor>();
    }
);
*/


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IVenueService, VenueService>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();


try
{
    using var cnx = new SqliteConnection(connectionString);

    var evolve = new Evolve(cnx, msg => Console.WriteLine(msg))
    {
        Locations = new[] { "Database/Migrations" },
        IsEraseDisabled = true,
        OutOfOrder = true
    };

    evolve.Migrate();
}
catch (Exception ex)
{
    Console.WriteLine("Migration failed");
    Console.WriteLine(ex);
    throw;
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
    .WithStaticAssets();

app.Run();