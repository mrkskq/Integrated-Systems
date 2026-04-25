using System.Text;
using EventsManagement.Repository;
using EventsManagement.Repository.Implementation;
using EventsManagement.Repository.Interface;
using EventsManagement.Service.Implementation;
using EventsManagement.Service.Interface;
using EventsManagement.Service.Jobs;
using EventsManagement.Web.Interceptor;
using EventsManagement.Web.Mapper;
using EvolveDb;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Quartz;

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
builder.Services.AddScoped<IFileUploadService, FileUploadService>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();


builder.Services.AddScoped<IVenueRepository, VenueRepository>();
builder.Services.AddScoped<ILegacyVenueRepository, LegacyVenueRepository>();
builder.Services.AddScoped<AuditInterceptor>();
builder.Services.AddScoped<ReservationMapper>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<VenueEtlService>();


// tie sho nasledvet od BackgroundService se so AddHostedService
builder.Services.AddHostedService<ReservationCleanupBackgroundService>();
builder.Services.AddHostedService<LegacyDbEtlBackgroundService>();


// za Quartz sho nasledvit od IJob e vaka
builder.Services.AddQuartzHostedService();

builder.Services.AddQuartz(options =>
{
    var jobKey = new JobKey("reservation-cleanup", "maintenance");
    options.AddJob<QuartzReservationCleanupJob>(o => o.WithIdentity(jobKey));

    options.AddTrigger(o =>
    {
        o.ForJob(jobKey).WithIdentity("reservation-cleanup-trigger")
            .WithCronSchedule("0 0/1 * * * ?")
            .WithDescription("Expires unpaid reservations");
    });
});



/*
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
*/

builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
    {
        options.UseSqlite(connectionString);
        options.AddInterceptors(sp.GetRequiredService<AuditInterceptor>());
    }
);

builder.Services.AddDbContext<LegacyApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration
            .GetConnectionString("LegacyVenueDb")));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();


/*
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddRazorPages();


var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]
                                 ?? throw new InvalidOperationException("Jwt:Key is missing from configuration."));

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero
        };
    });
*/

/*
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
*/


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