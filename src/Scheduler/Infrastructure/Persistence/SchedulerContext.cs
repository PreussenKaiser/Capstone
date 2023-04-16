using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Scheduler.Domain.Models;
using Scheduler.Domain.Utility;

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
	public required DbSet<Event> Events { get; init; }

	/// <summary>
	/// Instances of <see cref="Practice"/> in the database.
	/// </summary>
	public required DbSet<Practice> Practices { get; init; }

	/// <summary>
	/// Instances of <see cref="Game"/> in the database.
	/// </summary>
	public required DbSet<Game> Games { get; init; }

	/// <summary>
	/// Instances of <see cref="Recurrence"/> in the database.
	/// </summary>
	public required DbSet<Recurrence> Recurrences { get; init; }

	/// <summary>
	/// Instances of <see cref="Field"/> in the database.
	/// </summary>
	public required DbSet<Field> Fields { get; init; }

	/// <summary>
	/// Instances of <see cref="Team"/> in the database.
	/// </summary>
	public required DbSet<Team> Teams { get; init; }

	/// <summary>
	/// Instances of <see cref="League"/> in the database.
	/// </summary>
	public required DbSet<League> Leagues { get; init; }

	/// <summary>
	/// Provides additional configuration for models.
	/// </summary>
	/// <param name="builder">The API to configure with.</param>
	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.ApplyConfigurationsFromAssembly(typeof(SchedulerContext).Assembly);

		builder.Entity<User>().HasData(SeedData.Users);
		builder.Entity<Role>().HasData(SeedData.Roles);
		builder.Entity<IdentityUserRole<Guid>>().HasData(SeedData.UserRoles);
		builder.Entity<League>().HasData(SeedData.Leagues);

		base.OnModelCreating(builder);
	}
}
