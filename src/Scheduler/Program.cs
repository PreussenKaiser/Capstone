using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scheduler.Domain.Models;
using Scheduler.Domain.Repositories;
using Scheduler.Infrastructure.Persistence;
using Scheduler.Infrastructure.Repositories;

WebApplicationBuilder builder = WebApplication.CreateBuilder();

// Configure database
#if DEBUG
const string CONN = "Local";
#else
const string CONN = "Hosted";
#endif

string connectionString = builder.Configuration.GetConnectionString(CONN)
	?? throw new ArgumentException("Could not retrieve connection string.");

builder.Services
	.AddDbContext<SchedulerContext>(o => o
		.UseSqlServer(connectionString)
		.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking))
	.AddScoped<IScheduleRepository, ScheduleRepository>()
	.AddScoped<IFieldRepository, FieldRepository>()
	.AddScoped<ILeagueRepository, LeagueRepository>()
	.AddScoped<ITeamRepository, TeamRepository>();

// Configure identity
builder.Services
	.AddIdentity<User, Role>()
	.AddEntityFrameworkStores<SchedulerContext>()
	.AddDefaultTokenProviders();

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
	options.TokenLifespan = TimeSpan.FromHours(2));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error")
	   .UseHsts();
}

app.UseHttpsRedirection()
   .UseStaticFiles()
   .UseRouting()
   .UseAuthentication()
   .UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();