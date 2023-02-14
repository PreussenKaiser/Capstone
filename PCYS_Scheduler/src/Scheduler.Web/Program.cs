using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scheduler.Core.Models;
using Scheduler.Core.Models.Identity;
using Scheduler.Infrastructure.Persistence;

WebApplicationBuilder builder = WebApplication.CreateBuilder();

// Configure database
string? connectionString = builder.Configuration.GetConnectionString("Local");

ArgumentNullException.ThrowIfNull(connectionString);

builder.Services
	.AddDbContext<SchedulerContext>(o => o.UseSqlServer(connectionString))
	.AddDatabaseDeveloperPageExceptionFilter();

// Configure identity
builder.Services
	.AddIdentity<User, Role>()
	.AddEntityFrameworkStores<SchedulerContext>();

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
