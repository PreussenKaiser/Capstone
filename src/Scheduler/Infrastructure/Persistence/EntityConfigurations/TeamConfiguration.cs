using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduler.Domain.Models;

namespace Scheduler.Infrastructure.Persistence.EntityConfigurations;

/// <summary>
/// Contains configuration for <see cref="Team"/>.
/// </summary>
public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<Team> builder)
	{
		builder.HasKey(t => t.Id);

		builder
			.Property(t => t.Name)
			.IsRequired()
			.HasMaxLength(32);

		builder
			.Property(l => l.UserId)
			.IsRequired(false);
	}
}
