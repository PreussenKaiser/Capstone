﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Scheduler.Core.Models;
using Scheduler.Infrastructure.Utility;

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
	/// Instances of <see cref="Recurrence"/> in the database.
	/// </summary>
	public DbSet<Recurrence> Recurrences { get; set; }

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
		builder.Entity<Event>(builder =>
		{
			builder.UseTptMappingStrategy();

			builder
				.HasOne(e => e.Recurrence)
				.WithOne(r => r.Event)
				.HasForeignKey<Recurrence>(r => r.Id);
		});

		builder.Entity<User>().HasData(SeedData.Users);
		builder.Entity<Role>().HasData(SeedData.Roles);
		builder.Entity<IdentityUserRole<Guid>>().HasData(SeedData.UserRoles);

		base.OnModelCreating(builder);
	}
}
