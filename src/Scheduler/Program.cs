using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scheduler.Application.Logging;
using Scheduler.Application.Middleware;
using Scheduler.Application.Options;
using Scheduler.Application.Services;
using Scheduler.Domain.Models;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Services;
using Scheduler.Infrastructure.Persistence;
using Scheduler.Infrastructure.Repositories;

WebApplicationBuilder builder = WebApplication.CreateBuilder();

// Configure database
string connectionString = builder.Configuration.GetConnectionString("Default")
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

string timeZone = builder.Configuration
	.GetSection("TimeZone")
	.Value
		?? "Central Standard Time";

builder.Services
	.AddHostedService<ScheduleCullingService>()
	.AddHostedService<LogCullingService>()
	.AddSingleton<IDateProvider, SystemDateProvider>(p => new(timeZone))
	.AddSingleton<IEmailSender, SmtpEmailSender>();

builder.Services
	.AddIdentity<User, Role>(opt =>
	{
		opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
		opt.Lockout.MaxFailedAccessAttempts = 10;
	})
	.AddEntityFrameworkStores<SchedulerContext>()
	.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
	options.ExpireTimeSpan = TimeSpan.FromDays(1);
});

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
	options.TokenLifespan = TimeSpan.FromHours(2));

if (builder.Environment.IsDevelopment())
{
	builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}

builder.Services.AddControllersWithViews();

builder.Logging
	.ClearProviders()
	.AddTextLogging();

builder.Services
	.AddOptions<CullingOptions>()
	.Bind(builder.Configuration.GetSection(CullingOptions.Culling))
	.ValidateDataAnnotations();

builder.Services
	.AddOptions<SmtpOptions>()
	.Bind(builder.Configuration.GetSection(SmtpOptions.Smtp))
	.ValidateDataAnnotations();

builder.Services
	.AddOptions<EmailOptions>()
	.Bind(builder.Configuration.GetSection(EmailOptions.Email))
	.ValidateDataAnnotations();

WebApplication app = builder.Build();

app.UseMiddleware<LoggingMiddleware>();

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage()
	   .UseMigrationsEndPoint();
}
else if (app.Environment.IsProduction())
{
	app.UseExceptionHandler("/Error/500")
	   .UseStatusCodePagesWithRedirects("/Error/{0}")
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

app.Run();