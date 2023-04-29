using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scheduler.Application.Logging;
using Scheduler.Application.Middleware;
using Scheduler.Application.Options;
using Scheduler.Domain.Models;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Services;
using Scheduler.Infrastructure.Persistence;
using Scheduler.Infrastructure.Repositories;
using Scheduler.Services;

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
	.AddScoped<IScheduleRepository>(
		p => new ScheduleRepositoryLogger(
				new ScheduleRepository(p.GetRequiredService<SchedulerContext>()),
				p.GetRequiredService<ILogger<IScheduleRepository>>()))
	.AddScoped<IFieldRepository>(
		p => new FieldRepositoryLogger(
				new FieldRepository(p.GetRequiredService<SchedulerContext>()),
				p.GetRequiredService<ILogger<IFieldRepository>>()))
	.AddScoped<ILeagueRepository>(
		p => new LeagueRepositoryLogger(
				new LeagueRepository(p.GetRequiredService<SchedulerContext>()),
				p.GetRequiredService<ILogger<ILeagueRepository>>()))
	.AddScoped<ITeamRepository>(
		p => new TeamRepositoryLogger(
				new TeamRepository(p.GetRequiredService<SchedulerContext>()),
				p.GetRequiredService<ILogger<ITeamRepository>>()));

builder.Services
	.AddHostedService<ScheduleCullingService>()
	.AddHostedService<LogCullingService>()
	.AddSingleton<IDateProvider, SystemDateProvider>();

builder.Services
	.AddIdentity<User, Role>()
	.AddEntityFrameworkStores<SchedulerContext>()
	.AddDefaultTokenProviders()
	.AddEntityFrameworkStores<SchedulerContext>()
	.AddDefaultTokenProviders();

builder.Services.AddScoped<User>();

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
	options.TokenLifespan = TimeSpan.FromHours(2));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services
	.AddOptions<CullingOptions>()
	.Bind(builder.Configuration.GetSection(CullingOptions.Culling))
	.ValidateDataAnnotations();

builder.Logging
	.ClearProviders()
	.AddTextLogging();
 
WebApplication app = builder.Build();

app.UseMiddleware<LoggingMiddleware>();

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