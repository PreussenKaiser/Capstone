using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Scheduler.Core.Models;

namespace Scheduler.Infrastructure.Persistence;

/// <summary>
/// Handles access to a relational database.
/// </summary>
public sealed class SchedulerContext : IdentityDbContext<IdentityUser>
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
	/// Instances of <see cref="Field"/> in the database.
	/// </summary>
	public DbSet<Field> Fields { get; set; }

	/// <summary>
	/// Instances of <see cref="Game"/> in the database.
	/// </summary>
	public DbSet<Game> Games { get; set; }

	/// <summary>
	/// Instances of <see cref="Team"/> in the database.
	/// </summary>
	public DbSet<Team> Teams { get; set; }

	/// <summary>
	/// Provides additional configuration for models.
	/// </summary>
	/// <param name="builder">The API to configure with.</param>
	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder
			.Entity<Game>()
			.HasNoKey();

		base.OnModelCreating(builder);
	}

	/// <summary>
	/// Executes a function against the database then saves the changes.
	/// Intended for queries that mutate.
	/// </summary>
	/// <param name="function">The function to execute.</param>
	/// <returns>Whether the task was completed or not.</returns>
	internal async Task ExecuteAsync(Func<Task> function)
	{
		await function();

		await this.SaveChangesAsync();
	}
}
