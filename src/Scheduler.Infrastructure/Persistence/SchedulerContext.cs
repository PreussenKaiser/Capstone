using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Scheduler.Core.Models;

namespace Scheduler.Infrastructure.Persistence;

/// <summary>
/// Handles access to a relational database.
/// </summary>
public sealed class SchedulerContext : IdentityDbContext<User, Role, Guid>
{
	/// <summary>
	/// Initializes the <see cref="SchedulerContext"/> class.
	/// </summary>
	/// <param name="options">Database configuration.</param>
	public SchedulerContext(DbContextOptions<SchedulerContext> options)
		: base(options)
	{
	}

	/// <summary>
	/// Instances of <see cref="Event"/> in the database.
	/// </summary>
	public DbSet<Event> Events { get; set; }

	/// <summary>
	/// Instances of <see cref="Practice"/> in the database.
	/// </summary>
	public DbSet<Practice> Practices { get; set; }

	/// <summary>
	/// Instances of <see cref="Game"/> in the database.
	/// </summary>
	public DbSet<Game> Games { get; set; }

	/// <summary>
	/// Instances of <see cref="Field"/> in the database.
	/// </summary>
	public DbSet<Field> Fields { get; set; }

	/// <summary>
	/// Instances of <see cref="Team"/> in the database.
	/// </summary>
	public DbSet<Team> Teams { get; set; }

	/// <summary>
	/// Instances of <see cref="League"/> in the database.
	/// </summary>
	public DbSet<League> Leagues { get; set; }

	/// <summary>
	/// Provides additional configuration for models.
	/// </summary>
	/// <param name="builder">The API to configure with.</param>
	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder
			.Entity<Event>()
			.UseTptMappingStrategy();

		builder.Entity<Role>().HasData(new Role
		{
			Id = new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
			Name = "Admin",
			NormalizedName = "Admin",
			ConcurrencyStamp = Guid.NewGuid().ToString()
		});

		builder.Entity<User>().HasData(new User
		{
			Id = new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
			UserName = "teamnull@gmail.com",
			NormalizedUserName = "TEAMNULL@GMAIL.COM",
			FirstName = "Team",
			LastName = "Null",
			Email = "teamnull@gmail.com",
			PasswordHash = new PasswordHasher<User>().HashPassword(null!, "T3am-Null-Rul3z"),
			SecurityStamp = Guid.NewGuid().ToString("D")
		});

		builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
		{
			RoleId = new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
			UserId = new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c")
		});

		base.OnModelCreating(builder);
	}
}
