using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scheduler.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder();

// Configure database
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
MySqlServerVersion version = new(new Version(8, 0, 31));

ArgumentNullException.ThrowIfNull(connectionString);

builder.Services
	.AddDbContext<SchedulerContext>(o => o.UseMySql(connectionString, version))
	.AddDatabaseDeveloperPageExceptionFilter();

// Configure identity
builder.Services
	.AddDefaultIdentity<IdentityUser>(o => o.SignIn.RequireConfirmedAccount = true)
	.AddEntityFrameworkStores<SchedulerContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

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
