using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduler.Domain.Models;
using Scheduler.Domain.Utility;

namespace Scheduler.Infrastructure.Persistence.EntityConfigurations;

/// <summary>
/// Contains configuration for <see cref="League"/>.
/// </summary>
public sealed class LeagueConfiguration : IEntityTypeConfiguration<League>
{
	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<League> builder)
	{
		builder.HasKey(l => l.Id);

		builder
			.Property(l => l.Name)
			.IsRequired()
			.HasMaxLength(32);

		builder
			.Metadata
			.FindNavigation(nameof(League.Teams))
			?.SetPropertyAccessMode(PropertyAccessMode.Field);

		builder.HasData(SeedData.Leagues);
	}
}
